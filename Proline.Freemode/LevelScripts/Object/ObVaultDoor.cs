using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.Engine;
using Proline.Engine;

namespace Proline.Engine.LevelScripts.Object
{
    public class ObVaultDoor : GameScript
    {
        public ObVaultDoor()
        {
        }

        public Entity LocalEntity { get; set; }

        public override async Task Execute(params object[] args)
        {
            LocalEntity = new Prop((int)args[0]);

            //API.DoorControl((uint)LocalEntity.Model.Hash, LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z, true,
            //    1f, 1f, 1f);

            while (true)
            {
                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to open the vault door");
                DoesEntityExist((int)args[0], out bool exists);
                if (!exists)
                    break;
                if (Game.IsControlJustPressed(0, Control.Context))
                {
                    await Test();
                    break;
                }
                await BaseScript.Delay(0);
            }

            //if (LocalEntity.Exists())
            //{
            //    if (IsEntityWithinActivationRange())
            //    {
            //        Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to open the vault door");
            //        if (Game.IsControlJustPressed(0, Control.Context))
            //        {
            //            await Test();
            //            TerminateScript();
            //        }
            //    }
            //}
            //else
            //{
            //    TerminateScript();
            //}
        }

        public async Task Test()
        {
            API.CreateModelSwap(LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z, 1f,
                (uint)LocalEntity.Model.Hash, (uint)API.GetHashKey("hei_prop_heist_sec_door"), true);
            await BaseScript.Delay(5000);
            API.RequestAnimDict("anim@heists@fleeca_bank@bank_vault_door");
            while (!API.HasAnimDictLoaded("anim@heists@fleeca_bank@bank_vault_door"))
                await BaseScript.Delay(0);
            API.PlayEntityAnim(LocalEntity.Handle, "bank_vault_door_opens", "anim@heists@fleeca_bank@bank_vault_door", 4f,
                false, true, false, 0f, 8);
            API.ForceEntityAiAndAnimationUpdate(LocalEntity.Handle);
            API.RequestScriptAudioBank("vault_door", false);
            API.PlaySoundFromCoord(-1, "vault_unlock", Game.PlayerPed.Position.X,
                Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z,
                "dlc_heist_fleeca_bank_door_sounds", false, 0, false);
            await BaseScript.Delay(500);
            Debugger.LogDebug("Checking if the animation is playing\n");
            while (API.IsEntityPlayingAnim(LocalEntity.Handle, "anim@heists@fleeca_bank@bank_vault_door",
                "bank_vault_door_opens", 3))
            {
                Debugger.LogDebug("Animation is playing\n");
                if (API.GetEntityAnimCurrentTime(LocalEntity.Handle, "anim@heists@fleeca_bank@bank_vault_door",
                        "bank_vault_door_opens") >= 1f)
                {
                    var test = API.GetEntityRotation(LocalEntity.Handle, 2) + new Vector3(0, 0, -80);
                    API.FreezeEntityPosition(LocalEntity.Handle, true);
                    API.StopEntityAnim(LocalEntity.Handle, "bank_vault_door_opens",
                        "anim@heists@fleeca_bank@bank_vault_door", -1000f);
                    API.SetEntityRotation(LocalEntity.Handle, test.X, test.Y, test.Z, 2, true);
                    API.ForceEntityAiAndAnimationUpdate(LocalEntity.Handle);
                    API.SetModelAsNoLongerNeeded((uint)API.GetHashKey("hei_prop_heist_sec_door"));
                    Debugger.LogDebug("Finished");
                }

                await BaseScript.Delay(0);
            }
        }
    }
}