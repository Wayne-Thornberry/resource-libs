using Proline.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CExampleComponent
{
    public class ExampleCommander : ComponentCommands
    {
        [EngineCommand("X")]
        public void ExampleCommand()
        {

        }
    }
}
