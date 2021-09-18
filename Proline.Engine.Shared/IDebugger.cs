using Proline.Engine.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.Engine.Data;

namespace Proline.Engine
{
    public static class IDebugger
    {
        private static Log _log = new Log();
        public static void LogDebug(IDebugConsole console, object data)
        {
            if (!EngineConfiguration.IsDebugEnabled) return; 
            var d = _log.LogDebug(data); 
            console.WriteLine(d, true);
            //if (EngineConfiguration.IsClient)
            //    ExecuteEngineMethodServer(EngineConstraints.Log, 2, data.ToString());
        }
        public static void LogWarn(IDebugConsole console, object data)
        {
            if (!EngineConfiguration.IsDebugEnabled) return; 
            var d = _log.LogWarn(data);
            console.WriteLine(d, true);
            //if (EngineConfiguration.IsClient)
            //    ExecuteEngineMethodServer(EngineConstraints.Log, 2, data.ToString());
        }
        public static void LogError(IDebugConsole console, object data)
        {
            if (!EngineConfiguration.IsDebugEnabled) return; 
            var d = _log.LogError(data);
            console.WriteLine(d, true);
            //if (EngineConfiguration.IsClient)
            //    ExecuteEngineMethodServer(EngineConstraints.Log, 2, data.ToString());
        }
    }
}
