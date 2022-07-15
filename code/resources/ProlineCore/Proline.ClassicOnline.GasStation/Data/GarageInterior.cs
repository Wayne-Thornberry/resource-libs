using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.MWord
{
    internal abstract class Interior
    {

    }

    internal class GarageInterior : Interior
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public List<GarageSlot> VehicleSlots { get; set; }
    }
}