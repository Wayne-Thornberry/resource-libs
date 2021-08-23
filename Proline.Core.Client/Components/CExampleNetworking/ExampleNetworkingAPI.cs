using Proline.Core.Client;
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CExampleNetworking
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
