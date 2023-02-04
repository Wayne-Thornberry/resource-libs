using CDataStream;
using CGameLogic;
using CitizenFX.Core;
using Newtonsoft.Json;


using Proline.Resource.Framework;
using System;

namespace CNetConnection.Commands
{
    public class BuyRandomVehicleCommand : ResourceCommand
    {
        public BuyRandomVehicleCommand() : base("BuyRandomVehicle")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {

            var api = new CGameLogicAPI();
            if (api.GetCharacterBankBalance() > 250)
            {
                if (api.HasCharacter())
                {
                    if (api.GetPersonalVehicle() != null)
                    {
                        api.DeletePersonalVehicle();
                    }


                    api.SetCharacterBankBalance(250);
                    Array values = Enum.GetValues(typeof(VehicleHash));
                    Random random = new Random();
                    VehicleHash randomBar = (VehicleHash)values.GetValue(random.Next(values.Length));
                    var task = World.CreateVehicle(new Model(randomBar), World.GetNextPositionOnStreet(Game.PlayerPed.Position));
                    task.ContinueWith(e =>
                    {
                        var vehicle = e.Result;
                        api.SetCharacterPersonalVehicle(vehicle.Handle);

                        var id = "PlayerVehicle";
                        var dataAPI = new CDataStreamAPI();
                        dataAPI.CreateDataFile();
                        dataAPI.AddDataFileValue("VehicleHash", vehicle.Model.Hash);
                        dataAPI.AddDataFileValue("VehiclePosition", JsonConvert.SerializeObject(vehicle.Position));
                        vehicle.IsPersistent = true;
                        if (vehicle.AttachedBlips.Length == 0)
                            vehicle.AttachBlip();
                        dataAPI.SaveDataFile(id);

                    });
                }


            }
        }
    }
}
