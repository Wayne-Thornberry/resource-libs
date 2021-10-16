using CitizenFX.Core;
using Proline.Resource.Component.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CExample
{
    public class ExampleHandler : ComponentHandler
    {
        public ExampleHandler() : base()
        {

        }

        public override async Task OnLoad()
        {
            LogDebug("OnLoad"); 
        }

        public override async Task OnInitialized()
        {
            LogDebug("OnInitialized");
        }

        public override async Task OnTick()
        {
            await CallServerAPI(-769563032);
            await CallServerAPI(-488262820, 1, 2, 3);
            var x = await CallServerAPI(2127208193, 5, 6, 3);
            LogDebug("Result of call " + x); 
        }

        [ComponentAPI]
        public void ExampleClientAPI()
        {

        }
    }
}
