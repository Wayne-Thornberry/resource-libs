using CitizenFX.Core;
using Proline.ClassicOnline.MData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MGame
{
    public static class PCAPIs
    {
        public static void SaveCurrentCar()
        {
            if (Game.PlayerPed.CurrentVehicle != null)
            {
                var vehicle = Game.PlayerPed.CurrentVehicle;
                ResourceFile.CreateNewFile();
                ResourceFile.AddValue("VehicleModelHash", vehicle.Model.Hash);
                ResourceFile.AddValue("VehiclePosition", vehicle.Position);
                ResourceFile.AddValue("VehicleHeading", vehicle.Heading);
                ResourceFile.AddValue("VehicleHealth", vehicle.Health);

            }
        }
    }
}
