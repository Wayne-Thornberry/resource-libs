using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Proline.Engine.Client
{
    public interface IScriptSource
    {
        void RemoveTick(Func<Task> task);
        void AddTick(Func<Task> task);
        void WriteLine(object data);
        void Write(object data);
        string GetCurrentResourceName();
        string LoadResourceFile(string resourceName, string filePath);
        void TriggerServerEvent(string eventName, params object[] args);
        void TriggerClientEvent(string eventName, params object[] args);
        void TriggerClientEvent(int playerId, string eventName, params object[] args);
        void TriggerEvent(string eventName, params object[] data);
        Task Delay(int ms);
    }
}