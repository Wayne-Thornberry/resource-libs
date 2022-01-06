using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.CScripting.Framework;

namespace Proline.Classic.Scripts
{
    public class VehicleFuel : ScriptInstance
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
                if (!CitizenFX.Core.Game.PlayerPed.IsInVehicle()) return;
                if (!(CitizenFX.Core.Game.PlayerPed.CurrentVehicle.CurrentRPM > DeadZone ||
                      CitizenFX.Core.Game.PlayerPed.CurrentVehicle.CurrentRPM < -DeadZone)) return;
                CitizenFX.Core.Game.PlayerPed.CurrentVehicle.FuelLevel -= CitizenFX.Core.Game.PlayerPed.CurrentVehicle.CurrentRPM / ConsumptionRate * CitizenFX.Core.Game.LastFrameTime;
                if (CitizenFX.Core.Game.PlayerPed.CurrentVehicle.FuelLevel < DetectionLevel) CitizenFX.Core.Game.PlayerPed.CurrentVehicle.FuelLevel = 0f;
                await BaseScript.Delay(0);
            }
        }
    }
}