
extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;

using System.Threading.Tasks;
using Proline.Engine;

namespace Proline.Freemode.LevelScripts
{
    public class VehicleFuel : LevelScript
    {
        public VehicleFuel()
        {
            UseRpm = true;
            ConsumptionRate = 5;
            DetectionLevel = 6;
            DeadZone = 0.3f;
        }

        public bool UseRpm { get; set; }
        public float ConsumptionRate { get; set; }
        public float DetectionLevel { get; set; }
        public float DeadZone { get; set; }

        public override async Task Execute(params object[] args)
        {
            while (Stage != -1)
            {
                if (!Game.PlayerPed.IsInVehicle()) return;
                if (!(Game.PlayerPed.CurrentVehicle.CurrentRPM > DeadZone ||
                      Game.PlayerPed.CurrentVehicle.CurrentRPM < -DeadZone)) return;
                Game.PlayerPed.CurrentVehicle.FuelLevel -= Game.PlayerPed.CurrentVehicle.CurrentRPM / ConsumptionRate * Game.LastFrameTime;
                if (Game.PlayerPed.CurrentVehicle.FuelLevel < DetectionLevel) Game.PlayerPed.CurrentVehicle.FuelLevel = 0f;
                await BaseScript.Delay(0);
            }
        }
    }
}