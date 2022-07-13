using CitizenFX.Core;
using Proline.ClassicOnline.MData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MGame
{
    public static class MGameAPI
    {
        public static void SaveCurrentCar()
        {
            if (Game.PlayerPed.CurrentVehicle != null)
            {
                var vehicle = Game.PlayerPed.CurrentVehicle;
                API.CreateDataFile();
                API.AddDataFileValue("VehicleModelHash", vehicle.Model.Hash);
                API.AddDataFileValue("VehiclePosition", vehicle.Position);
                API.AddDataFileValue("VehicleHeading", vehicle.Heading);
                API.AddDataFileValue("VehicleHealth", vehicle.Health);

            }
        }
    }
}
