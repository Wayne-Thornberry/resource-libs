
extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;

using Newtonsoft.Json;
using Proline.Freemode;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CGasStations
{

    public class GasStationsComponent : EngineComponent
    { 

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnLoad()
        { 
        }

        protected override void OnStart()
        { 
            // var data = ResourceFile.Load(// API.GetCurrentResourceName(), "data/gasstations.json"); 
            //var _x = JsonConvert.DeserializeObject<GasStation[]>(data);
        }
        private List<Blip> _blips = new List<Blip>();
        private GasStation[] _x = new GasStation[0];

        [ComponentAPI]
        public void AttachBlipsToGasStations()
        {
            foreach (var item in _x)
            {
                var vector = new Vector3(item.X, item.Y, item.Z);
                LogDebug(item.Name);
                _blips.Add(World.CreateBlip(vector));
            }
        }
    }
}
