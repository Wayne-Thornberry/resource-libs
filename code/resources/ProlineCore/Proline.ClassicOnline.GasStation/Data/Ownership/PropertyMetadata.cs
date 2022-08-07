using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.MWord
{
    internal class PropertyMetadata
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public bool HasGarage { get; set; }
        public string Garage { get; set; }
        public string Apartment { get; set; }
        public string Layout { get; set; }
        public int VehicleCap { get; set; }
        public string Id { get; set; }
        public string Building { get; set; }
    } 
     
}