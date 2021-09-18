using System.Collections.Generic;
using CitizenFX.Core;
using Proline.Classic.Data;
using Proline.Engine.Componentry;

namespace Proline.Classic.Components
{

    public class CGasStations : ClientComponent
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

    }
}
