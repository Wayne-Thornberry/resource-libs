

using Proline.Freemode;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CExampleComponent
{
    public class ExampleHandler : ComponentHandler
    {
        public override void OnInitialized()
        {
            base.OnInitialized();
        }

        public override void OnLoad()
        {
            base.OnLoad();
        }

        public override void OnStart()
        {
            //EngineAccess.ExecuteComponentAPI(this, "ExampleControl", new object[] { "X", "Y", "X" });
            Persistence.Set("EnableSomething", true);
            Debugger.LogDebug("ExampleLog", true);
            
            base.OnStart();
        }

        public override void OnStop()
        {
            base.OnStop();
        }
    }
}
