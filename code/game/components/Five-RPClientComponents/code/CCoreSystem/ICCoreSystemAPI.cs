using System.Threading.Tasks;

namespace CCoreSystem
{
    public interface ICCoreSystemAPI
    {
        Task Delay(int ms);
        void TriggerScriptEvent(string eventName, params object[] args);
        bool GetEventExists(object scriptInstance, string eventName);
        object[] GetEventData(object scriptInstance, string eventName);
        int StartNewScript(string scriptName, params object[] args);
        int GetInstanceCountOfScript(string scriptName);
        void MarkScriptAsNoLongerNeeded(object callingClass);
        void MarkScriptAsNoLongerNeeded(string scriptName);
        void TerminateScript(string scriptName);
    }
}
