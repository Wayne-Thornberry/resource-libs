using System;

namespace Proline.ClassicOnline.MShop.Internal
{

    internal abstract class Catalouge
    {
        public string Version { get; set; }

        internal virtual CatalougeItem GetItem(string vehicleName) { return null; }
    }
}