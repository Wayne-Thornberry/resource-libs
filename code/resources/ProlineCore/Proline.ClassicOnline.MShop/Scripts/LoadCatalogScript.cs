using Newtonsoft.Json;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MShop.Internal;
using Proline.Modularization.Core;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MShop.Scripts
{
    internal class LoadCatalogScript : ModuleScript
    {
        public override async Task OnExecute()
        {
            var data = ResourceFile.Load("data/catalogue/catalogue-vehicles.json");
            MDebugAPI.LogDebug(data);
            CatalougeManager.GetInstance().Register("VehicleCatalouge", JsonConvert.DeserializeObject<VehicleCatalouge>(data.Load()));
        }
     }
}
