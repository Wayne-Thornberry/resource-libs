using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Server.Components.CExampleNetworking
{
    public class ExampleNetworkingComponent : AbstractComponent
    {
        [ComponentAPI]
        public void LogDebug(string data)
        {
            Debugger.LogDebug(data);
        }
        [ComponentAPI]
        public void LogError(string data)
        {
            Debugger.LogError(data);
        }
        [ComponentAPI]
        public void LogWarn(string data)
        {
            Debugger.LogWarn(data);
        }

        [ComponentAPI]
        public int TestNetworkMethod(long x, long y, long z)
        {

            Debugger.LogDebug(x);
            Debugger.LogDebug(y);
            Debugger.LogDebug(z);
            return 1;
        }
    }
}
