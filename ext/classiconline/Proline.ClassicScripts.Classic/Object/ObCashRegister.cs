using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MScripting;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Object
{
    public class ObCashRegister
    {

        public ObCashRegister()
        {
        }

        public Prop LocalEntity { get; private set; }

        public async Task Execute(object[] args, CancellationToken token)
        {

            var handle = (int)args[0];
            LocalEntity = new Prop(handle);
            while (!token.IsCancellationRequested)
            {
                if (LocalEntity.Exists())
                {
                    if (LocalEntity.Model == API.GetHashKey("prop_till_01_dam"))
                    {
                        MDebugAPI.LogDebug(((uint)LocalEntity.Model.Hash).ToString());
                        MDebugAPI.LogDebug(((uint)LocalEntity.Model.Hash).ToString());
                    }

                    if (!LocalEntity.HasBeenDamagedByAnyWeapon())
                    {
                        Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ To do something");
                        if (Game.IsControlJustPressed(0, Control.Context))
                        {
                            API.CreateModelSwap(LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z,
                                1f,
                                (uint)API.GetHashKey("prop_till_01"), (uint)API.GetHashKey("prop_till_01_dam"), true);
                            await World.CreateAmbientPickup(PickupType.MoneyDepBag,
                                API.GetSafePickupCoords(LocalEntity.Position.X, LocalEntity.Position.Y,
                                    LocalEntity.Position.Z, 0,
                                    0), new Model("prop_money_bag_01"), 0);
                            API.RemoveModelSwap(LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z,
                                1f,
                                (uint)API.GetHashKey("prop_till_01"), (uint)API.GetHashKey("prop_till_01_dam"), true);

                            MScriptingAPI.MarkScriptAsNoLongerNeeded(this);
                            MScriptingAPI.TerminateScriptInstance(this);
                        }
                    }
                    else
                    {
                        await World.CreateAmbientPickup(PickupType.MoneyDepBag,
                            API.GetSafePickupCoords(LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z,
                                0, 0),
                            new Model("prop_money_bag_01"), 0);

                        MScriptingAPI.MarkScriptAsNoLongerNeeded(this);
                        MScriptingAPI.TerminateScriptInstance(this);
                    }
                }
                else
                {
                    MScriptingAPI.MarkScriptAsNoLongerNeeded(this);
                    MScriptingAPI.TerminateScriptInstance(this);
                }
                await BaseScript.Delay(0);
            }
          
        }
    }
}