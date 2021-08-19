using Proline.Engine.Client;
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
           var service =  new EngineService(program);
            service.Initialize();
            service.StartAllComponents();
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
            Console.ReadKey();
        }

        private static void X(string v, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void AddTick(Func<Task> task)
        {
            _ticks.Add(task);
        }

        public async Task Delay(int ms)
        {
            await Task.Delay(ms);
        }

        public string GetCurrentResourceName()
        {
            return "Proline.Engine.Client";
        }

        public string LoadResourceFile(string resourceName, string filePath)
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath));
        }

        public void RemoveTick(Func<Task> task)
        {
            _ticks.Remove(task);
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
