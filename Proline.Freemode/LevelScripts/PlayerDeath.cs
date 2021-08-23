using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.Engine;
using Proline.Engine;

namespace Proline.Engine.LevelScripts
{
    public class PlayerDeath : GameScript
    {
        public enum SpawnType
        {
            LAST_LOCATION,
            HOPSITAL,
            NEAREST_SIDEWALK,
            NEAREST_SAFEHOUSE
        }

        private int _deathStage;
        private float _timer;

        public PlayerDeath()
        {
            WastedTime = 3000;
            EnableTransition = true;
            EnableCost = true;
            EnableScript = true;
            EnableFasterRespawn = true;
            DistanceRange = 15f;
            EnableInvincibility = false;
            InvincibilityDuration = 3000;
            EnableDelayedRespawn = false;
            DelayedRespawnTime = 5000;
            DeathCost = 200;
            FadeHoldTime = 2000;
            RespawnType = SpawnType.NEAREST_SIDEWALK;
            TransitionTime = 500;

            CitizenFX.Core.Native.API.RegisterCommand("KillSelf", new Action(KillSelf), false);
            CitizenFX.Core.Native.API.RegisterCommand("ReviveSelf", new Action(ReviveSelf), false);
        }

        public int WastedTime { get; set; }
        public bool EnableDelayedRespawn { get; set; }
        public int DelayedRespawnTime { get; set; }
        public int TransitionTime { get; set; }
        public bool EnableTransition { get; set; }
        public bool EnableFasterRespawn { get; set; }
        public float DistanceRange { get; set; }
        public int InvincibilityDuration { get; set; }
        public bool EnableCost { get; set; }
        public int FadeHoldTime { get; set; }
        public bool EnableInvincibility { get; set; }
        public bool EnableScript { get; set; }
        public int DeathCost { get; set; }
        public SpawnType RespawnType { get; set; }

        private void PlayerDied()
        {
            SetDeathStage(1);
        }

        private void ReviveSelf()
        {
            Game.PlayerPed.Position = Game.PlayerPed.Position;
            Debugger.LogDebug("reviving");
            CitizenFX.Core.Native.API.NetworkRespawnCoords(Game.PlayerPed.Handle, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y,
                Game.PlayerPed.Position.Z, false, false);
            CitizenFX.Core.Native.API.ResurrectPed(Game.PlayerPed.Handle);
            Game.PlayerPed.Health = 100;
        }

        public override async Task Execute(params object[] args)
        {
            while (_deathStage != -1)
            {  
                if (_deathStage == 0 && Game.PlayerPed.IsDead) PlayerDied();

                switch (_deathStage)
                {
                    case 0:

                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        if (_timer > 0)
                            _timer -= Game.LastFrameTime;
                        else
                            SetDeathStage(_deathStage + 1);
                        Game.DisableAllControlsThisFrame(0);
                        break;
                }
                //Debugger.LogDebug(Stage);
                //Debugger.LogDebug(_timer);
                await BaseScript.Delay(0);
            }
        }

        private void SetDeathStage(int stage)
        {
            _deathStage = stage;
            switch (_deathStage)
            {
                case 0:
                    Game.EnableAllControlsThisFrame(0);
                    break;
                case 1:
                    if (EnableDelayedRespawn)
                        _timer = ConvertMsToFloat(DelayedRespawnTime);
                    else
                        _timer = 0;
                    break;
                case 2:
                    _timer = ConvertMsToFloat(WastedTime);
                    PlayWastedEffect();
                    break;
                case 3:
                    if (EnableTransition)
                    {
                        _timer = ConvertMsToFloat(TransitionTime);
                        Screen.Fading.FadeOut(TransitionTime);
                    }

                    break;
                case 4:
                    _timer = ConvertMsToFloat(FadeHoldTime);
                    RespawnPlayer(GetPlayerRespawnLocation());
                    if (EnableInvincibility)
                        Game.PlayerPed.IsInvincible = true;
                    break;
                case 5:
                    if (EnableTransition)
                    {
                        _timer = ConvertMsToFloat(TransitionTime);
                        Screen.Fading.FadeIn(TransitionTime);
                    }

                    StopWastedEffect();
                    break;
                case 6:
                    if (EnableInvincibility)
                        _timer = ConvertMsToFloat(InvincibilityDuration);
                    else
                        SetDeathStage(0);
                    //if (EnableCost) Cash.RemoveBankCash(DeathCost);
                    //BaseScript.TriggerEvent("PlayerRespawned");
                    break;
                case 7:
                    SetDeathStage(0);
                    Game.PlayerPed.IsInvincible = false;
                    break;
            }
        }

        private void RespawnPlayer(Vector3 position)
        {
            if (Game.PlayerPed.IsInVehicle())
                Game.PlayerPed.Position = Game.PlayerPed.Position;
            Game.PlayerPed.Resurrect();
            Game.PlayerPed.ResetVisibleDamage();
            Game.PlayerPed.Position =
                new Vector3(position.X, position.Y, World.GetGroundHeight(position));
            Game.PlaySound("WEAPON_PURCHASE", "HUD_AMMO_SHOP_SOUNDSET");
        }

        private Vector3 GetPlayerRespawnLocation()
        {
            switch (RespawnType)
            {
                case SpawnType.NEAREST_SIDEWALK:
                    return World.GetNextPositionOnSidewalk(new Vector3(
                        Game.PlayerPed.Position.X + DistanceRange,
                        Game.PlayerPed.Position.Y + DistanceRange,
                        World.GetGroundHeight(new Vector2(Game.PlayerPed.Position.X + DistanceRange,
                            Game.PlayerPed.Position.Y + DistanceRange))));
                //case RespawnType.HOPSITAL:
                //  return World.GetClosest(Game.PlayerPed.Position, GameworldController.Locations.HospitalSpawns)
                //    .Position;
                default:
                    return Game.PlayerPed.Position;
            }
        }

        private void StopWastedEffect()
        {
            Screen.Effects.Stop(ScreenEffect.DeathFailMpIn);
            GameplayCamera.StopShaking();
        }

        private void PlayWastedEffect()
        {
            Screen.Effects.Start(ScreenEffect.DeathFailMpIn);
            GameplayCamera.Shake(CameraShake.DeathFail, 1f);
        }

        private void KillSelf()
        {
            Game.PlayerPed.Kill();
        }


        public float ConvertMsToFloat(int time)
        {
            return (float)(time * 0.001);
        }
    }
}