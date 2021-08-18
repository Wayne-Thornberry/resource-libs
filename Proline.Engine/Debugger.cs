using Proline.Engine.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Client
{
    public static class Debugger
    {
        public static void LogError(object data)
        {
            if (!EngineConfiguration.IsDebugEnabled()) return;
            var log = new Log();
            log.LogError(data);
        }

        public static void LogWarn(object data)
        {
            if (!EngineConfiguration.IsDebugEnabled()) return;
            var log = new Log();
            log.LogWarn(data);
        }

        public static void LogDebug(IEngineTracker trackedObject, object data)
        {
            if (!EngineConfiguration.IsDebugEnabled()) return;
            var log = new Log();
            log.LogDebug($"[{trackedObject.Type}:{trackedObject.Name}] " + data);
        }

        public static void LogDebug(object data)
        { 
            if (!EngineConfiguration.IsDebugEnabled()) return;
            var log = new Log();
            log.LogDebug(data);
        }
    }
}
