using System.Threading;
using System.Threading.Tasks;
using Proline.EngineFramework.Scripting;

namespace Proline.ExampleClient.Scripts.Object
{
    public class ObCashRegister : DemandScript
    { 

        public ObCashRegister(string name) : base(name)
        {
        }

        public override async Task Execute(object[] args, CancellationToken token)
        {

            //if (LocalEntity.Exists())
            //{
            //    if (LocalEntity.Model == API.GetHashKey("prop_till_01_dam"))
            //    {
            //        LogDebug(((uint) LocalEntity.Model.Hash).ToString());
            //        LogDebug(((uint) LocalEntity.Model.Hash).ToString());
            //    }

            //    if (!LocalEntity.HasBeenDamagedByAnyWeapon())
            //    {
            //        if (IsEntityWithinActivationRange())
            //        {
            //            Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ To do something");
            //            if (Game.IsControlJustPressed(0, Control.Context))
            //            {
            //                API.CreateModelSwap(LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z,
            //                    1f,
            //                    (uint) API.GetHashKey("prop_till_01"), (uint) API.GetHashKey("prop_till_01_dam"), true);
            //                await World.CreateAmbientPickup(PickupType.MoneyDepBag,
            //                    API.GetSafePickupCoords(LocalEntity.Position.X, LocalEntity.Position.Y,
            //                        LocalEntity.Position.Z, 0,
            //                        0), new Model("prop_money_bag_01"), 0);
            //                API.RemoveModelSwap(LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z,
            //                    1f,
            //                    (uint) API.GetHashKey("prop_till_01"), (uint) API.GetHashKey("prop_till_01_dam"), true);
            //                TerminateScript();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        await World.CreateAmbientPickup(PickupType.MoneyDepBag,
            //            API.GetSafePickupCoords(LocalEntity.Position.X, LocalEntity.Position.Y, LocalEntity.Position.Z,
            //                0, 0),
            //            new Model("prop_money_bag_01"), 0);
            //        TerminateScript();
            //    }
            //}
            //else
            //{
            //    TerminateScript();
            //}
        }
    }
}