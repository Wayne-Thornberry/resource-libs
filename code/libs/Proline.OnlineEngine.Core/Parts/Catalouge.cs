
using CShopCatalogue;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {

        public static void BuyVehicle(string vehicleName)
        {
            var api = new CShopCatalogueAPI();
            api.BuyVehicle(vehicleName);
        }
    }
}
