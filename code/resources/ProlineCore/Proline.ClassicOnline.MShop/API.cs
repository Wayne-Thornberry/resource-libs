using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.MShop.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MShop
{
    internal static partial class API
    {
        public static void BuyVehicle(string vehicleName)
        {
            try
            {
                if(CharacterGlobals.Character != null)
                {
                    var manager = CatalougeManager.GetInstance();
                    var catalouge = manager.GetCatalouge("VehicleCatalouge");
                    if (catalouge == null)
                        throw new Exception("Catalouge not found"); 
                    var vci = (VehicleCatalougeItem)catalouge.GetItem(vehicleName);

                    if (vci == null)
                        throw new Exception("Catalouge Item not found");

                    if (CharacterGlobals.Character.BankBalance > vci.Price)
                    {
                        var currentVehicle = CharacterGlobals.Character.PersonalVehicle;
                        if (currentVehicle != null)
                        {
                            foreach (var item in currentVehicle.AttachedBlips)
                            {
                                item.Delete();
                            }
                            currentVehicle.IsPersistent = false;
                            currentVehicle.Delete();
                        }


                        CharacterGlobals.Character.BankBalance -= vci.Price;  
                        var task = World.CreateVehicle(new Model(vci.Model), World.GetNextPositionOnStreet(Game.PlayerPed.Position));
                        task.ContinueWith(e =>
                        {
                            var createdVehicle = e.Result;
                            CharacterGlobals.Character.PersonalVehicle = new GCharacter.Data.PlayerPersonalVehicle(createdVehicle.Handle);

                            var id = "PlayerVehicle";
                            MData.API.CreateDataFile();
                            MData.API.AddDataFileValue("VehicleHash", currentVehicle.Model.NativeValue);
                            MData.API.AddDataFileValue("VehiclePosition", JsonConvert.SerializeObject(createdVehicle.Position));
                            createdVehicle.IsPersistent = true;
                            if (createdVehicle.AttachedBlips.Length == 0)
                                createdVehicle.AttachBlip();
                            MData.API.SaveDataFile(id);

                        });
                    }
                }
               
            }
            catch (Exception e)
            {

                MDebug.MDebugAPI.LogError(e);
            }
        }
    }
}
