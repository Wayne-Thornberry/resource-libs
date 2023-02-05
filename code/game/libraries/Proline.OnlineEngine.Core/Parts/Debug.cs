using CDebugActions;
using System;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {

        public static void LogDebug(object obj, bool outputToServer = false)
        {
            var api = new CDebugActionsAPI();
            api.LogDebug(obj, outputToServer);
        }
        public static void LogError(object obj, bool outputToServer = false)
        {
            var api = new CDebugActionsAPI();
            api.LogError(obj, outputToServer);
        }
        public static void LogInfo(object obj, bool outputToServer = false)
        {
            var api = new CDebugActionsAPI();
            api.LogInfo(obj, outputToServer);
        }
        public static void LogWarn(object obj, bool outputToServer = false)
        {
            var api = new CDebugActionsAPI();
            api.LogWarn(obj, outputToServer);
        }

    }
}
