using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CWorld
{
    public class WorldCommander : ComponentCommander
    {

        [ComponentCommand("DoSomething")]
        public void DoSomething()
        {

            Debugger.LogDebug("It worked!");
        }
    }
}
