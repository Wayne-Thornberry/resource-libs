
using Proline.Example.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.Engine.ConsoleApp
{
    class Program : IScriptSource
    {
        private static List<Func<Task>> _ticks;

        public Program()
        {
            _ticks = new List<Func<Task>>();
        }

        static void Main(string[] args)
        {
            var program = new Program();
            //program.Test();
           var service =  new EngineService(program);
            service.Initialize("ConsoleApp", "0", "true");
            service.StartAllComponents();
            service.StartStartupScripts();
            var thread = new Thread(e=> {
                while (true)
                {
                    if (_ticks != null) continue;
                    foreach (var item in _ticks)
                    {
                        item.Invoke();
                    }
                }
            });
            thread.Start();


            var task = Task.Run(async ()  =>  {
                while (true)
                {
                    await service.Tick();
                }
            });



            Console.ReadKey();
        }

        public void AddTick(Func<Task> task)
        {
            _ticks.Add(task);
        }

        public object CallFunction<T>(ulong hash, object[] inputParameters)
        {
            throw new NotImplementedException();
        }

        public void  CallFunction(ulong hash, object[] inputParameters)
        {
            throw new NotImplementedException();
        }

        public object CallNativeAPI(string apiName, params object[] inputParameters)
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
