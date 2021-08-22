using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Components.CExample
{
    public class ExampleComponentAPI : ComponentAPI
    {

        [Client]
        [ComponentCommand("X")]
        public void ExampleCommand()
        {

        }

        [Server]
        [ComponentCommand("X")]
        public void ExampleCommand2()
        {

        }

        [Client]
        [ComponentAPI]
        public int ExampleAPI()
        {
            return 1;
        }

        [Client]
        [ComponentAPI]
        public bool UnlockNeareastVehicle(int x, int y)
        {
            //Debugger.LogDebug(this, "It seems to have worked");
            y = 100;
            return true;
        }

        [Client]
        [ComponentAPI]
        public bool UnlockNeareastVehicle(int x, out int y)
        {
            //Debugger.LogDebug(this, "It seems to have worked");
            y = 100;
            return true;
        }

        [Server]
        [ComponentAPI]
        public int ExampleAPI2()
        {
            return 1;
        }
    }
}
