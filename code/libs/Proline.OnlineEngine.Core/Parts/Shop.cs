using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static async Task<string> GetVehicleNames()
        {
            var api = new CShopCatalogue.CShopCatalogueAPI();
            var result = await api.ListVehicles();
            return result; 
        }

        public static void PutVehicleIn(string name)
        {
            var api = new CShopCatalogue.CShopCatalogueAPI();
            api.PutVehicle(name); 
        }

        public static void DeleteVehicle(string name)
        {
            var api = new CShopCatalogue.CShopCatalogueAPI();
            api.DeleteVehicle(name);
        }
    }
}
