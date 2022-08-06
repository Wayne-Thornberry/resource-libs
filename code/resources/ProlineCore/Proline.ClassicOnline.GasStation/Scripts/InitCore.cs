using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MWorld.Internal;
using Proline.ClassicOnline.MWord;
using Proline.ClassicOnline.MWorld.Client.Data;

using Proline.Resource.IO;

namespace Proline.ClassicOnline.MWorld.Client.Scripts
{
    public class InitCore 
    {


        public async Task Execute()
        {
            var apartmentData = ResourceFile.Load("data/world/interiors/apartments.json");
            var garageData = ResourceFile.Load("data/world/interiors/garages.json");
             

            var instance = InteriorManager.GetInstance();
            var apartmentInteriors = JsonConvert.DeserializeObject<List<Interior>>(apartmentData.Load());
            var garageInteriors = JsonConvert.DeserializeObject<List<Interior>>(garageData.Load()); 
             
            foreach (var item in apartmentInteriors)
            { 
                instance.Register(item.Id, item);
            }

            foreach (var item in garageInteriors)
            {
                instance.Register(item.Id, item);
            }


            var dpHeightsData = ResourceFile.Load("data/world/apartments/apt_dpheights.json");
            var pierLowData = ResourceFile.Load("data/world/garages/gar_pier_low.json");

            var data = dpHeightsData.Load();
            var data2 = pierLowData.Load();
            //MDebug.MDebugAPI.LogDebug(data);
            //MDebug.MDebugAPI.LogDebug(data2);

            try
            {

                var dpHeights = JsonConvert.DeserializeObject<ApartmentBuilding>(data);
                var pierLow = JsonConvert.DeserializeObject<GarageBuilding>(data2);

                MDebug.MDebugAPI.LogDebug($"IDS MODCHECK???");
                MDebug.MDebugAPI.LogDebug($"IDS MODCHECK???");

                var pm = PropertyManager.GetInstance();
                pm.Register(dpHeights.Id, dpHeights);
                pm.Register(pierLow.Id, pierLow);
            }
            catch (System.Exception e) 
            {
                MDebug.MDebugAPI.LogDebug(e);
            }

        }


    }
}
