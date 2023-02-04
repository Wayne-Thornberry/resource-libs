using CitizenFX.Core;
using Proline.Resource.Framework;
using System.Threading.Tasks;

namespace CWorldObjects.Commands
{
    public class SpawnMoneyBagCommand : ResourceCommand
    {
        public SpawnMoneyBagCommand() : base("SpawnMoneyBag")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            DoThing();
        }

        private static async Task DoThing()
        {
            var pickup = await World.CreateAmbientPickup(PickupType.MoneyDepBag, Game.PlayerPed.Position + Game.PlayerPed.ForwardVector * 2, new Model("xm_prop_x17_bag_01b"), 10000);
        }
    }
}
