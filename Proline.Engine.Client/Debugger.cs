using Proline.Engine.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.Engine.Data;

namespace Proline.Engine
{
    public static class Debugger
    {

        public static void LogDebug(object data)
        {
            IDebugger.LogDebug(new DebugConsole(), data); 
        }
        public static void LogWarn(object data)
        { 
            IDebugger.LogDebug(new DebugConsole(), data); 
        }
        public static void LogError(object data)
        {
            IDebugger.LogDebug(new DebugConsole(), data);  
        }
    }
}
