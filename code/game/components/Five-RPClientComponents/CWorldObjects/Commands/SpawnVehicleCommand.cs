using CitizenFX.Core;
using Proline.Resource.Framework;
using System;
using Console = Proline.Resource.Console;

namespace CWorldObjects.Commands
{
    public class SpawnVehicleCommand : ResourceCommand
    {
        public SpawnVehicleCommand() : base("SpawnVehicle")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    VehicleHash randomBar;
                    if (Enum.TryParse(args[0].ToString(), true, out randomBar))
                    {
                        World.CreateVehicle(new Model(randomBar), World.GetNextPositionOnStreet(Game.PlayerPed.Position));
                    };
                }
                catch (Exception)
                {
                    Console.WriteLine("Vehicle could not be found");
                }
            }
        }
    }
}
