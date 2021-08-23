using CitizenFX.Core;
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CGasStations
{
    public class GasStationsAPI : ComponentAPI
    {
        private List<Blip> _blips = new List<Blip>();
        private GasStation[] _x = new GasStation[0];

        [ComponentAPI]
        public void AttachBlipsToGasStations()
        {
            foreach (var item in _x)
            {
                var vector = new Vector3(item.X, item.Y, item.Z);
                Debugger.LogDebug(item.Name);
                _blips.Add(World.CreateBlip(vector));
            }
        }
    }
}
