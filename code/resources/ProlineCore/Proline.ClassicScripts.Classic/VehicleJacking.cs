using System;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.SClassic
{
    public class VehicleJacking
    {
        public VehicleJacking()
        {
            ChanceOfFleeing = 0.1f;
            ChanceOfLocking = 0.5f;
            EnableFleeing = true;
            EnableLocking = true;
            RandomGenerator = new Random();
            CurrentChance = 0.0;
        }

        public int ScriptState { get; set; }
        public float ChanceOfFleeing { get; set; }
        public float ChanceOfLocking { get; set; }
        public bool EnableFleeing { get; set; }
        public Random RandomGenerator { get; set; }
        public bool EnableLocking { get; set; }
        public Vehicle Target { get; set; }
        public double CurrentChance { get; set; }
        public int Stage { get; private set; }

        public async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                switch (Stage)
                {
                    case 0:
                        if (Game.PlayerPed.VehicleTryingToEnter != null)
                        {
                            Target = Game.PlayerPed.VehicleTryingToEnter;
                            if (Target.LockStatus == VehicleLockStatus.Unlocked)
                                ScriptState = 1;
                        }
                        break;
                    case 1:
                        if (EnableLocking)
                        {
                            CurrentChance = RandomGenerator.NextDouble();
                            Target.LockStatus = CurrentChance < ChanceOfLocking
                                ? VehicleLockStatus.Locked
                                : VehicleLockStatus.None;
                        }

                        if (Target.Driver != null && EnableFleeing)
                        {
                            CurrentChance = RandomGenerator.NextDouble();
                            if (CurrentChance < ChanceOfFleeing && Target.Driver.IsPlayer)
                            {
                                Target.Driver.Task.FleeFrom(Game.PlayerPed);
                                Target.LockStatus = VehicleLockStatus.Unlocked;
                            }
                        }

                        ScriptState++;
                        break;
                    case 2:
                        if (Game.PlayerPed.VehicleTryingToEnter == null)
                        {
                            Target = null;
                            ScriptState = 0;
                        }
                        break;
                }
                await BaseScript.Delay(0);
            }
        }
    }
}