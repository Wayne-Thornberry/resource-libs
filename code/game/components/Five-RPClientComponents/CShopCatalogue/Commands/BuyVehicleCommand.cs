using Newtonsoft.Json;
using Proline.Resource.Framework;
using Proline.Resource.IO;

namespace CShopCatalogue.Commands
{
    public class BuyVehicleCommand : ResourceCommand
    {
        public BuyVehicleCommand() : base("BuyVehicle")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length > 0)
            {
                var vehicle = args[0].ToString();
                var api = new CShopCatalogueAPI();
                api.BuyVehicle(vehicle);
            }

        }
    }
}
