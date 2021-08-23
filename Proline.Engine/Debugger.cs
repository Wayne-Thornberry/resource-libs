using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class Debugger
    {
        public static void LogError(object data, bool replicate = false)
        {
            if (!EngineConfiguration.IsDebugEnabled) return;
            var log = new Log();
            var entry = log.LogError(data);
            F8Console.WriteLine(entry);
            if (replicate && EngineConfiguration.EnvType == 0 && EngineStatus.IsEngineInitialized)
                EngineAccess.ExecuteEngineMethodServer("LogError", "[Client] " + data.ToString());
            EngineAccess.TriggerEngineEvent("logError", entry);
        }

        public static void LogWarn(object data, bool replicate = false)
        {
            if (!EngineConfiguration.IsDebugEnabled) return;
            var log = new Log();
            var entry = log.LogWarn(data);
            F8Console.WriteLine(entry);
            if (replicate && EngineConfiguration.EnvType == 0 && EngineStatus.IsEngineInitialized)
                EngineAccess.ExecuteEngineMethodServer("LogWarn", "[Client] " + data.ToString());
            EngineAccess.TriggerEngineEvent("logWarn", entry);
        }

        public static void LogDebug(IEngineTracker trackedObject, object data)
        {
            LogDebug($"[{trackedObject.Type}:{trackedObject.Name}] " + data);
        }

        public static void LogDebug(object data, bool replicate = false)
        { 
            if (!EngineConfiguration.IsDebugEnabled) return;
            var log = new Log();
            var entry = log.LogDebug(data);
            F8Console.WriteLine(entry);
            if (replicate && EngineConfiguration.EnvType == 0 && EngineStatus.IsEngineInitialized)
                EngineAccess.ExecuteEngineMethodServer("LogDebug", "[Client] " + data.ToString());
            EngineAccess.TriggerEngineEvent("logDebug", entry);
        }

    }
}
