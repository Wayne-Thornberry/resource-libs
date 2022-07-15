using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GCharacter;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class BuyRandomVehicleCommand : ModuleCommand
    {
        public BuyRandomVehicleCommand() : base("BuyRandomVehicle")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");

            if (stat2.GetValue() > 250)
            {
                if(CharacterGlobals.Character != null)
                {
                    if(CharacterGlobals.Character.PersonalVehicle != null)
                    {
                        foreach (var item in CharacterGlobals.Character.PersonalVehicle.AttachedBlips)
                        {
                            item.Delete();
                        }
                        CharacterGlobals.Character.PersonalVehicle.IsPersistent = false;
                        CharacterGlobals.Character.PersonalVehicle.Delete();
                    }


                    stat2.SetValue(stat2.GetValue() - 250);
                    Array values = Enum.GetValues(typeof(VehicleHash));
                    Random random = new Random();
                    VehicleHash randomBar = (VehicleHash)values.GetValue(random.Next(values.Length));
                    var task = World.CreateVehicle(new Model(randomBar), World.GetNextPositionOnStreet(Game.PlayerPed.Position));
                    task.ContinueWith(e =>
                    {
                        var vehicle = e.Result;
                        CharacterGlobals.Character.PersonalVehicle = new GCharacter.Data.PlayerPersonalVehicle(vehicle.Handle);

                        var id = "PlayerVehicle";
                        MData.API.CreateDataFile();
                        MData.API.AddDataFileValue("VehicleHash", vehicle.Model.NativeValue);
                        MData.API.AddDataFileValue("VehiclePosition", JsonConvert.SerializeObject(vehicle.Position));
                        vehicle.IsPersistent = true;
                        if (vehicle.AttachedBlips.Length == 0)
                            vehicle.AttachBlip();
                        MData.API.SaveDataFile(id);

                    });
                }

                
            }
        }
    }
}
