using CitizenFX.Core;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWord
{
    public class GarageSlot
    {
        public int Index { get; set; }
        public Vector3 Position { get; set; }
        public float Heading { get; set; }
    }

    internal class GarageLayout
    { 
        public List<GarageSlot> VehicleSlots { get; set; }
    } 
}