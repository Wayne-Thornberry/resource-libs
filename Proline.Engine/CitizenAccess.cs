
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class CitizenAccess : IScriptSource
    {
        private IScriptSource _source;
        internal CitizenAccess(IScriptSource source)
        {
            _source = source;
        }

        public void RemoveTick(Func<Task> task)
        {
            _source.RemoveTick(task);
        }

        public void AddTick(Func<Task> task)
        {
            _source.AddTick(task);
        }

        public void WriteLine(object data)
        {
            _source.WriteLine(data);
        }

        public void Write(object data)
        {
            _source.Write(data);
        }

        public string GetCurrentResourceName()
        {
            return _source.GetCurrentResourceName();
        }

        public object GetGlobal(string key)
        {
            return _source.GetGlobal(key);
        }

        public string LoadResourceFile(string resourceName, string filePath)
        {
            return _source.LoadResourceFile(resourceName, filePath);
        }

        public void TriggerServerEvent(string eventName, params object[] args)
        {
            _source.TriggerServerEvent(eventName, args);
        }

        public void SetGlobal(string key, object data, bool replicated)
        {
            _source.SetGlobal(key, data, replicated);
        }

        public void TriggerClientEvent(string eventName, params object[] args)
        {
            _source.TriggerClientEvent(eventName, args);
        }

        public void TriggerClientEvent(int playerId, string eventName, params object[] args)
        {
            _source.TriggerClientEvent(playerId, eventName, args);
        }

        public void TriggerEvent(string eventName, params object[] data)
        {
            _source.TriggerEvent(eventName, data);
        }

        public async Task Delay(int ms)
        {
            await _source.Delay(ms);
        }

        public object CallFunction<T>(ulong hash, object[] inputParameters)
        {
            return _source.CallFunction<T>(hash, inputParameters);
        }

        public void CallFunction(ulong hash, object[] inputParameters)
        {
            _source.CallFunction(hash, inputParameters);
        }
    }
}
