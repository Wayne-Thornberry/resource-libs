using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.MDebug;

namespace Proline.ClassicOnline.SClassic
{
    public class PlayerDeath
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
            DelayedRespawn = false;
            DelayedRespawnTime = 5000;
            DeathCost = 200;
            FadeHoldTime = 2000;
            RespawnType = SpawnType.NEAREST_SIDEWALK;
            TransitionTime = 500;

            //API.RegisterCommand("KillSelf", new Action(KillSelf), false);
            //API.RegisterCommand("ReviveSelf", new Action(ReviveSelf), false);
        }

        public int WastedTime { get; set; }
        public bool DelayedRespawn { get; set; }
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

        public async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_deathStage > 0)
                    Game.DisableAllControlsThisFrame(0);

                if (_timer > 0)
                {
                    _timer -= Game.LastFrameTime;
                    await BaseScript.Delay(0);
                    continue;
                }

                switch (_deathStage)
                {
                    case 0:
                        if (Game.PlayerPed.IsDead)
                            _deathStage++;
                        break;
                    case 1:
                        _timer = ConvertMsToFloat(DelayedRespawnTime);
                        PlayWastedEffect();
                        _deathStage++;
                        break;
                    case 2:
                        _timer = ConvertMsToFloat(WastedTime);
                        _deathStage++;
                        break;
                    case 3:
                        if (EnableTransition)
                        {
                            _timer = ConvertMsToFloat(TransitionTime);
                            Screen.Fading.FadeOut(TransitionTime);
                        }
                        _deathStage++;
                        break;
                    case 4:
                        _timer = ConvertMsToFloat(FadeHoldTime);
                        RespawnPlayer(GetPlayerRespawnLocation());
                        if (EnableInvincibility)
                            Game.PlayerPed.IsInvincible = true;
                        _deathStage++;
                        break;
                    case 5:
                        if (EnableTransition)
                        {
                            _timer = ConvertMsToFloat(TransitionTime);
                            Screen.Fading.FadeIn(TransitionTime);
                        }
                        StopWastedEffect();
                        _deathStage++;
                        break;
                    case 6:
                        Game.Player.WantedLevel = 0;
                        Game.PlayerPed.IsInvincible = false;
                        _deathStage = 0;
                        break;
                }
                //MDebugAPI.LogDebug(Stage);
                //MDebugAPI.LogDebug(_timer);
                await BaseScript.Delay(0);
            }
        }


        private void ReviveSelf()
        {
            Game.PlayerPed.Position = Game.PlayerPed.Position;
            MDebugAPI.LogDebug("reviving");
            API.NetworkRespawnCoords(Game.PlayerPed.Handle, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y,
                Game.PlayerPed.Position.Z, false, false);
            API.ResurrectPed(Game.PlayerPed.Handle);
            Game.PlayerPed.Health = 100;
        }

        private void RespawnPlayer(Vector3 position)
        {
            if (Game.PlayerPed.IsInVehicle())
                Game.PlayerPed.Position = Game.PlayerPed.Position;
            Game.PlayerPed.Resurrect();
            Game.PlayerPed.ResetVisibleDamage();
            Game.PlayerPed.Position =
                new Vector3(position.X, position.Y, World.GetGroundHeight(position));
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
            stat.SetValue(stat.GetValue() - 500);
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

        public float ConvertMsToFloat(int time)
        {
            return (float)(time * 0.001);
        }
    }
}