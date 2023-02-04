using CDataStream;
using CDebugActions;
using CGameLogic;
using CitizenFX.Core;
using CShopCatalogue.Internal;
using Newtonsoft.Json;
using System; 
using Console = Proline.Resource.Console;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proline.Resource.Eventing;
using Proline.ResourceLibs.ServerEventCaller;

namespace CShopCatalogue
{
    public class CShopCatalogueAPI : ICShopCatalogueAPI
    {
        public async Task<string> ListVehicles()
        {
            try
            {
                var result = "";
                var x = new EventCaller("ListItemsRequestHandler");
                x.OnEventCallback((args) =>
                {
                    var xy = args[0].ToString();
                    var list = JsonConvert.DeserializeObject<List<ShopItem>>(xy);
                    var names = list.Select(y => y.Name).ToArray();
                    result = string.Join(",", names);
                });
                x.Invoke();
                await x.WaitForCallback(); 
                return result; 
            }
            catch (Exception e)
            {
                var api = new CDebugActionsAPI();
                api.LogError(e);
            }
            return "";
        }

        public async Task PutVehicle(string name)
        {
            try
            {
                var events = new EventCaller("PutItemRequestHandler");
                events.OnEventCallback((args) =>
                {

                });
                events.Invoke(name);
                await events.WaitForCallback(); 
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task DeleteVehicle(string name)
        {
            try
            {
                var events = new EventCaller("DeleteItemRequestHandler");
                events.OnEventCallback((args) =>
                {

                });
                events.Invoke(name);
                await events.WaitForCallback();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void BuyVehicle(string vehicleName)
        {
            var api = new CDebugActionsAPI();
            var gameApi = new CGameLogicAPI();
            try
            {
                if (gameApi.HasCharacter())
                {
                    var manager = CatalougeManager.GetInstance();
                    var catalouge = manager.GetCatalouge("VehicleCatalouge");
                    if (catalouge == null)
                        throw new Exception("Catalouge not found");
                    var vci = (VehicleCatalougeItem)catalouge.GetItem(vehicleName);

                    if (vci == null)
                        throw new Exception("Catalouge Item not found");
                    if (gameApi.HasBankBalance(vci.Price))
                    {
                        var currentVehicle = gameApi.GetPersonalVehicle();
                        if (currentVehicle != null)
                        {
                            foreach (var item in currentVehicle.AttachedBlips)
                            {
                                item.Delete();
                            }
                            currentVehicle.IsPersistent = false;
                            currentVehicle.Delete();
                        }


                        var task = World.CreateVehicle(new Model(vci.Model), World.GetNextPositionOnStreet(Game.PlayerPed.Position));
                        task.ContinueWith(e =>
                        {
                            var createdVehicle = e.Result;
                            gameApi.SetCharacterPersonalVehicle(createdVehicle.Handle);
                            var dataAPI = new CDataStreamAPI();
                            var id = "PlayerVehicle";
                            dataAPI.CreateDataFile();
                            dataAPI.AddDataFileValue("VehicleHash", createdVehicle.Model.Hash);
                            dataAPI.AddDataFileValue("VehiclePosition", JsonConvert.SerializeObject(createdVehicle.Position));
                            createdVehicle.IsPersistent = true;
                            if (createdVehicle.AttachedBlips.Length == 0)
                                createdVehicle.AttachBlip();
                            dataAPI.SaveDataFile(id);
                            gameApi.SubtractValueFromBankBalance(vci.Price);
                        });

                    }
                }

            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }
    }
}
