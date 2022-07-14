using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core.Scaleforms;

namespace Proline.ClassicOnline.SClassic.Old
{
    public class RespawnScript
    {
        public enum SpawnType
        {
            LAST_LOCATION,
            HOPSITAL,
            NEAREST_SIDEWALK,
            NEAREST_SAFEHOUSE
        }

        public RespawnScript()
        {
            WastedDisplay = new MPBigMessageFreemode();
            WastedTime = 3000;
            EnableTransition = true;
            EnableCost = true;
            EnableScript = true;
            EnableFasterRespawn = true;
            DistanceRange = 15f;
            EnableInvincibility = true;
            InvincibilityDuration = 3000;
            DelayedRespawn = false;
            DelayedRespawnTime = 5000;
            FadeHoldTime = 2000;
            RespawnType = SpawnType.NEAREST_SIDEWALK;
            TransitionTime = 500;
        }

        public void Update()
        {
            //if (ScriptStage == 0 && Game.PlayerPed.IsDead)
            //{
            //    ScriptStage = 1;
            //}

            //switch (ScriptStage)
            //{
            //    case 1:
            //        if (ElapsedTime == 0)
            //        {
            //            if (EnableBaseScript.DelayedRespawn)
            //                CountTimer = Tools.ConvertMsToFloat(BaseScript.DelayedRespawnTime);
            //            WastedDisplay.ShowShardCenteredMPMessage("WASTED", "",
            //                (int)HudColor.HUD_COLOUR_RED, (int)HudColor.HUD_COLOUR_BLACK);
            //        }

            //        if (ElapsedTime > 500)
            //        {
            //            ScriptStage = 2;
            //            ElapsedTime = 0;
            //        }
            //        else
            //        {
            //            ElapsedTime++;
            //        }
            //        break;
            //    case 2:
            //        CountTimer = Tools.ConvertMsToFloat(WastedTime);
            //        PlayWastedEffect();
            //        break;
            //    case 3:
            //        if (EnableTransition)
            //        {
            //            CountTimer = Tools.ConvertMsToFloat(TransitionTime);
            //            Screen.Fading.FadeOut(TransitionTime);
            //        }
            //        break;
            //    case 4:
            //        CountTimer = Tools.ConvertMsToFloat(FadeHoldTime);
            //        RespawnPlayer(GetPlayerRespawnLocation());
            //        if (EnableInvincibility)
            //            Game.PlayerPed.IsInvincible = true;
            //        break;
            //    case 5:
            //        if (EnableTransition)
            //        {
            //            CountTimer = Tools.ConvertMsToFloat(TransitionTime);
            //            Screen.Fading.FadeIn(TransitionTime);
            //        }
            //        StopWastedEffect();
            //        break;
            //    case 6:
            //        if (EnableInvincibility)
            //        {
            //            CountTimer = Tools.ConvertMsToFloat(InvincibilityDuration);
            //        }
            //        else
            //        {
            //            ScriptStage = 0;
            //        }

            //        break;
            //    case 7:
            //        ScriptStage = 0;
            //        Game.PlayerPed.IsInvincible = false;
            //        break;
            //}

            //if (ScriptStage == 5) Game.DisableAllControlsThisFrame(0);

            //if (!(CountTimer > 0)) return;
            //CountTimer -= Game.LastFrameTime;
            //if (CountTimer <= 0)
            //{
            //    ScriptStage = ScriptStage + 1;
            //}

        }

        public void OnGUI()
        {
            if (WastedDisplay.IsLoaded)
            {
                WastedDisplay.Render2D();
            }
            else
            {
                WastedDisplay.Load();
            }
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
        public float CountTimer { get; set; }
        public SpawnType RespawnType { get; set; }
        public MPBigMessageFreemode WastedDisplay { get; set; }
        public int ScriptStage { get; private set; }
        public int ElapsedTime { get; private set; }

        private void RespawnPlayer(Vector3 position)
        {
            Game.PlayerPed.Resurrect();
            Game.PlayerPed.ResetVisibleDamage();
            Game.PlayerPed.Position = new Vector3(position.X, position.Y, World.GetGroundHeight(position));
        }

        private Vector3 GetPlayerRespawnLocation()
        {
            switch (RespawnType)
            {
                case SpawnType.NEAREST_SIDEWALK:
                    return World.GetNextPositionOnSidewalk(new Vector3(Game.PlayerPed.Position.X + DistanceRange,
                        Game.PlayerPed.Position.Y + DistanceRange,
                        World.GetGroundHeight(new Vector2(Game.PlayerPed.Position.X + DistanceRange,
                            Game.PlayerPed.Position.Y + DistanceRange))));
                //case RespawnType.HOPSITAL:
                //  return World.GetClosest(Game.PlayerPed.Position, GameworldController.Locations.HospitalSpawns)
                //    .Position;
                case SpawnType.LAST_LOCATION:
                    return Game.PlayerPed.Position;
                case SpawnType.NEAREST_SAFEHOUSE:
                    return Game.PlayerPed.Position;
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
            Game.PlaySound("MP_Flash", "WastedSounds");
            Screen.Effects.Start(ScreenEffect.DeathFailMpIn);
            GameplayCamera.Shake(CameraShake.DeathFail, 1f);
        }

        public async Task Execute(object[] args, CancellationToken token)
        {

        }
    }
}