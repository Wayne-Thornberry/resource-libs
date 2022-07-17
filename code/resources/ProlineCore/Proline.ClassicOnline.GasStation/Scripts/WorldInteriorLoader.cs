using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MWorld.Internal;
using Proline.ClassicOnline.MWord;
using Proline.ClassicOnline.MWorld.Client.Data;
using Proline.Modularization.Core;
using Proline.Resource.IO;

namespace Proline.ClassicOnline.MWorld.Client.Scripts
{
    public class WorldInteriorLoader 
    {


        public async Task Execute()
        {
            var twocarjson = ResourceFile.Load("data/world/garages/2cargarage.json");
            var sixcarjson = ResourceFile.Load("data/world/garages/6cargarage.json");
            var tencarjson = ResourceFile.Load("data/world/garages/10cargarage.json");

            var instance = InteriorManager.GetInstance();
            var twoCarInterior = JsonConvert.DeserializeObject<GarageInterior>(twocarjson.Load());
            var sixCarInterior = JsonConvert.DeserializeObject<GarageInterior>(sixcarjson.Load());
            var tenCarInterior = JsonConvert.DeserializeObject<GarageInterior>(tencarjson.Load());

            instance.Register("2CarGarage", twoCarInterior);
            instance.Register("6CarGarage", sixCarInterior);
            instance.Register("10CarGarage", tenCarInterior);
        }


    }
}
