using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.LevelScripts
{
    public class VehicleFuel 
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
        public int Stage { get; private set; }

        public async Task Execute(object[] args, CancellationToken token)
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