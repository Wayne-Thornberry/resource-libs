using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Core.Client;
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CExampleComponent
{
    public class ExampleHandler : ComponentHandler
    {
        public override void OnComponentInitialized()
        {
            base.OnComponentInitialized();
        }

        public override void OnComponentLoad()
        {
            base.OnComponentLoad();
        }

        public override void OnComponentStart()
        {
            //EngineAccess.ExecuteComponentAPI(this, "ExampleControl", new object[] { "X", "Y", "X" });
            Persistence.Set("EnableSomething", true);
            Debugger.LogDebug("ExampleLog", true);
            
            base.OnComponentStart();
        }

        public override void OnComponentStop()
        {
            base.OnComponentStop();
        }
    }
}
