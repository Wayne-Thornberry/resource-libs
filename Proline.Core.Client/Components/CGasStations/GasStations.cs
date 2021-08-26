

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

    public class GasStationsHandler : ComponentHandler
    { 

        public override void OnInitialized()
        {
            base.OnInitialized();
        }

        public override void OnLoad()
        { 
        }

        public override void OnStart()
        { 
            // var data = ResourceFile.Load(// API.GetCurrentResourceName(), "data/gasstations.json"); 
            //var _x = JsonConvert.DeserializeObject<GasStation[]>(data);
        }
    }
}
