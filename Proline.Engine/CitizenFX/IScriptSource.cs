using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public interface IScriptSource
    {
        void RemoveTick(Func<Task> task);
        void AddTick(Func<Task> task);
        void WriteLine(object data);
        void Write(object data);
        string GetCurrentResourceName();
        object GetGlobal(string key);
        string LoadResourceFile(string resourceName, string filePath);
        void TriggerServerEvent(string eventName, params object[] args);
        void SetGlobal(string key, object data, bool replicated);
        void TriggerClientEvent(string eventName, params object[] args);
        void TriggerClientEvent(int playerId, string eventName, params object[] args);
        void TriggerEvent(string eventName, params object[] data);
        Task Delay(int ms);
      //  object CallNativeAPI(string apiName, params object[] inputParameters);
        object CallFunction<T>(ulong hash, object[] inputParameters);
        void CallFunction(ulong hash, object[] inputParameters);
    }
}