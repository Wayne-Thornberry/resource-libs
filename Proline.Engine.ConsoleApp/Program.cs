 
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
        private EngineService _service;
        private List<Func<Task>> _ticks;

        public Program()
        { 
            _service = new EngineService(this);
            _ticks = new List<Func<Task>>();
        }

        private async Task Start()
        { 
            await _service.Start("ConsoleApp", "0", "true");
            _service.DumpLog();
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.Start().Wait();
            var thread = Task.Run(program.NewMethod);
            Console.ReadKey();
        }

        private async Task NewMethod()
        {
            while (true)
            {
                if (_ticks == null) continue;
                foreach (var item in _ticks)
                {
                        await item.Invoke();
                }
            };
        }

        public void AddTick(Func<Task> task)
        {
            _ticks.Add(task);
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
