
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Client.Components.CExample
{
    public class ExampleComponent : AbstractComponent
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

        [ComponentAPI]
        public int ExampleAPI()
        {
            return 1;
        }


        [ComponentAPI]
        public bool UnlockNeareastVehicle(int x, out int y)
        {
            Debugger.LogDebug(this, "It seems to have worked");
            y = 100;
            return true;
        }
    }
}
