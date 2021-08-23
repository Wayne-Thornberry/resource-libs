using Proline.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CExampleComponent
{
    public class ExampleCommander : ComponentCommander
    {
        [ComponentCommand("X")]
        public void ExampleCommand()
        {

        }
    }
}
