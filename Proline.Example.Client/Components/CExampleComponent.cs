using Proline.Engine.Client;
using Proline.Engine.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Client.Components
{
    public class CExampleComponent : SimpleComponent
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
            base.OnComponentStart();
        }

        public override void OnComponentStop()
        {
            base.OnComponentStop();
        }

        [ComponentCommand("X")]
        public void ExampleCommand()
        {

        }

        [ControllerControl]
        public void ExampleControl(string x, string y, string z)
        {
            Debugger.LogDebug(x);
            Debugger.LogDebug(y);
            Debugger.LogDebug(z);
        }

        [ComponentAPI]
        public int ExampleAPI()
        {
            return 1;
        }
    }
}
