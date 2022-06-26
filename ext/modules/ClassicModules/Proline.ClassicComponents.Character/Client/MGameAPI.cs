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
                MDataAPI.CreateFile();
                MDataAPI.AddFileValue("VehicleModelHash", vehicle.Model.Hash);
                MDataAPI.AddFileValue("VehiclePosition", vehicle.Position);
                MDataAPI.AddFileValue("VehicleHeading", vehicle.Heading);
                MDataAPI.AddFileValue("VehicleHealth", vehicle.Health);

            }
        }
    }
}
