using CCoreSystem;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static void TriggerScriptEvent(string eventName, params object[] args)
        {
            var coreAPI = new CCoreSystemAPI();
            coreAPI.TriggerScriptEvent(eventName, args);
        }

        public static bool GetEventExists(object scriptInstance, string eventName)
        {
            var coreAPI = new CCoreSystemAPI();
            return coreAPI.GetEventExists(scriptInstance, eventName);
        }

        public static object[] GetEventData(object scriptInstance, string eventName)
        {
            var coreAPI = new CCoreSystemAPI();
            return coreAPI.GetEventData(scriptInstance, eventName);
        }
    }
}
