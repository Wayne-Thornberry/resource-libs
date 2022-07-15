using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MShop.Internal
{
    internal class VehicleCatalougeItem : CatalougeItem
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public long Price { get; set; }
    }

    internal class VehicleCatalouge : Catalouge
    {
        public VehicleCatalougeItem[] Vehicles { get; set; }


        internal override CatalougeItem GetItem(string vehicleName)
        {
            var list = new List<VehicleCatalougeItem>(Vehicles);
            return list.FirstOrDefault(e => e.Name.Equals(vehicleName));
        }
    }
}
