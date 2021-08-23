using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Online.Script
{
    public partial class EngineScript : BaseScript, IScriptSource
    {
        public void AddTick(Func<Task> task)
        {
            Tick += task;
        }

        public object CallFunction<T>(ulong hash, object[] inputParameters)
        {
            throw new NotImplementedException();
        }

        public void CallFunction(ulong hash, object[] inputParameters)
        {
            throw new NotImplementedException();
        }

        public async Task Delay(int ms)
        {
            await Delay(ms);
        }

        public string GetCurrentResourceName()
        {
            return API.GetCurrentResourceName();
        }

        public object GetGlobal(string key)
        {
            throw new NotImplementedException();
        }

        public string LoadResourceFile(string resourceName, string filePath)
        {
            return API.LoadResourceFile(resourceName, filePath);
        }

        public void RemoveTick(Func<Task> task)
        {
            Tick -= task;
        }

        public void SetGlobal(string key, object data, bool replicated)
        {
            throw new NotImplementedException();
        }

        public void TriggerClientEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerClientEvent(eventName, args);
        }

        public void TriggerClientEvent(int playerId, string eventName, params object[] args)
        {
            BaseScript.TriggerClientEvent(Players[playerId], eventName, args);
        }

        public void TriggerEvent(string eventName, params object[] data)
        {
            BaseScript.TriggerEvent(eventName, data);
        }

        public void TriggerServerEvent(string eventName, params object[] args)
        {

        }

        public void Write(object data)
        {
            Debug.Write(data.ToString());
        }

        public void WriteLine(object data)
        {
            Debug.WriteLine(data.ToString());
            string x = "";
            if(File.Exists("Log"))
              x = File.ReadAllText("Log");
            File.WriteAllText("Log", x + "\n" + data.ToString());
        }
    }
}
