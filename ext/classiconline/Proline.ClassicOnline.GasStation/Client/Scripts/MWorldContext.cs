using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Modularization.Core;

namespace Proline.ClassicOnline.MWorld
{
    public class MWorldContext : ModuleScript
    {


        public override async Task OnExecute()
        {
            //EnableControllers();
            // var data = ResourceFile.Load(// API.GetCurrentResourceName(), "data/gasstations.json"); 
            //var _x = JsonConvert.DeserializeObject<GasStation[]>(data);
        }
         

        private List<Blip> _blips = new List<Blip>();
        private GasStation[] _x = new GasStation[0];
         
    }
}
