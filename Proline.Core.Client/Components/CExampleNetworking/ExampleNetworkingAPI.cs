using Proline.Freemode;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CExampleNetworking
{
    public class ExampleNetworkingAPI : ComponentAPI 
    { 
        [ComponentAPI]
        public void Test(string x, string y, string z)
        {

            Debugger.LogDebug(x);
            Debugger.LogDebug(y);
            Debugger.LogDebug(z);
        }
    }
}
