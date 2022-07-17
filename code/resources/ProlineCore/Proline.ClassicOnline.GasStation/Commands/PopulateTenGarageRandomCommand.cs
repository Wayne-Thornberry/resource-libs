using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proline.Resource.Framework;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MWorld.Commands
{
    public class PopulateTenGarageRandomCommand : ResourceCommand
    {
        public PopulateTenGarageRandomCommand() : base("PopulateTenGarageRandom")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            DoStuff();
        }

        private static async Task DoStuff()
        {
            for (int i = 0; i < 10; i++)
            {
                //Array values = Enum.GetValues(typeof(VehicleHash));
                //Random random = new Random();
                //VehicleHash randomBar = (VehicleHash)values.GetValue(random.Next(values.Length));
                var vehicle = await World.CreateVehicle(new Model(VehicleHash.Buffalo3), Game.PlayerPed.Position);
                WorldAPI.PlaceVehicleInGarageSlot("10CarGarage", i, vehicle);
            }
        }
    }
}
