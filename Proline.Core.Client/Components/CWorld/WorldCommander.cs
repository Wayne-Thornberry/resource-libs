using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CWorld
{
    public class WorldCommander : ComponentCommands
    {

        [EngineCommand("DoSomething")]
        public void DoSomething()
        {

            Debugger.LogDebug("It worked!");
        }
    }
}
