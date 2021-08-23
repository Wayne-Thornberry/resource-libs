using Proline.Engine;
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

        [Server]
        [ComponentAPI]
        public int ExampleAPI2()
        {
            return 1;
        }
    }
}
