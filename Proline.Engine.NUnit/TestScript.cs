using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.Engine.NUnit
{
    internal class TestScript : IScriptSource
    {
        private static List<Func<Task>> _ticks;

        public TestScript()
        {
            _ticks = new List<Func<Task>>(); 
        } 

        private static void X(string v, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void AddTick(Func<Task> task)
        {
            _ticks.Add(task);
        }

        public object CallFunction<T>(ulong hash, object[] inputParameters)
        {
            throw new NotImplementedException();
        }

        public void CallFunction(ulong hash, object[] inputParameters)
        {
            throw new NotImplementedException();
        }

        public void CallNativeAPI(string apiName, params object[] inputParameters)
        {
            throw new NotImplementedException();
        }

        public async Task Delay(int ms)
        {
            await Task.Delay(ms);
        }

        public string GetCurrentResourceName()
        {
            return "Proline.Engine.Client";
        }

        public object GetGlobal(string key)
        {
            throw new NotImplementedException();
        }

        public string LoadResourceFile(string resourceName, string filePath)
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath));
        }

        public void RemoveTick(Func<Task> task)
        {
            _ticks.Remove(task);
        }

        public void SetGlobal(string key, object data, bool replicated)
        {
            throw new NotImplementedException();
        }

        public void TriggerClientEvent(string eventName, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void TriggerClientEvent(int playerId, string eventName, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent(string eventName, params object[] data)
        {
            throw new NotImplementedException();
        }

        public void TriggerServerEvent(string eventName, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Write(object data)
        {
            Console.Write(data);
        }

        public void WriteLine(object data)
        {
            Console.WriteLine(data);
        }
    }
}
