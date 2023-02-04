using CitizenFX.Core;
using Proline.Resource.Framework;
using System.Threading.Tasks;

namespace CWorldObjects.Commands
{
    public class PopulateSixGarageRandomCommand : ResourceCommand
    {
        public PopulateSixGarageRandomCommand() : base("PopulateSixGarageRandom")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            DoStuff();
        }

        private static async Task DoStuff()
        {
            for (int i = 0; i < 6; i++)
            {
                //Array values = Enum.GetValues(typeof(VehicleHash));
                //Random random = new Random();
                //VehicleHash randomBar = (VehicleHash)values.GetValue(random.Next(values.Length));
                var vehicle = await World.CreateVehicle(new Model(VehicleHash.Buffalo3), Game.PlayerPed.Position);
                var api = new CWorldObjectsAPI();
                api.PlaceVehicleInGarageSlot("6CarGarage", i, vehicle);
            }
        }
    }
}
