using System.Collections.Generic;

namespace CWorldObjects.Data.Ownership
{

    internal class BuildingInteriorLink
    {
        public string Id { get; set; }
        public string Interior { get; set; }
        public string Building { get; set; }
        public Dictionary<string, string> ExteriorEntrances { get; set; }
        public Dictionary<string, string> InteriorExits { get; set; }
    }
}