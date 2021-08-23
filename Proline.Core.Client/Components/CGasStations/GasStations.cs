using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Core.Client;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CGasStations
{

    public class GasStationsHandler : ComponentHandler
    { 

        public override void OnComponentInitialized()
        {
            base.OnComponentInitialized();
        }

        public override void OnComponentLoad()
        { 
        }

        public override void OnComponentStart()
        { 
             var data = ResourceFile.Load(API.GetCurrentResourceName(), "data/gasstations.json"); 
            var _x = JsonConvert.DeserializeObject<GasStation[]>(data);
        }
    }
}
