using System.Collections.Generic;
using CitizenFX.Core; 
using Proline.Resource.Client.Framework;

namespace Proline.Classic.Engine.Components.CGasStation
{

    public class GasStationsContext : ComponentContext
    {


        public override void OnStart()
        {
            // var data = ResourceFile.Load(// API.GetCurrentResourceName(), "data/gasstations.json"); 
            //var _x = JsonConvert.DeserializeObject<GasStation[]>(data);
        }

        public override void OnLoad()
        {
        }

        private List<Blip> _blips = new List<Blip>();
        private GasStation[] _x = new GasStation[0];

    }
}
