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
                MDataAPI.CreateNewFile();
                MDataAPI.AddValue("VehicleModelHash", vehicle.Model.Hash);
                MDataAPI.AddValue("VehiclePosition", vehicle.Position);
                MDataAPI.AddValue("VehicleHeading", vehicle.Heading);
                MDataAPI.AddValue("VehicleHealth", vehicle.Health);

            }
        }
    }
}
