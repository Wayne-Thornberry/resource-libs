using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.MWord
{

    internal class Interior
    {
        public string Id { get; set; }
        public List<Vector3> Entrances { get; set; }
        public List<Vector3> Exits { get; set; }
    }

    internal abstract class PropertyInterior
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Interior { get; set; }
    }

    internal class ApartmentInterior : PropertyInterior
    {

    }

    internal class GarageInterior : PropertyInterior
    {
        public List<GarageSlot> VehicleSlots { get; set; }
    }
}