using Proline.ModuleFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ModuleFramework.Tester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ModuleManager.LoadModules();
            ModuleManager.StartAllModules();
            ModuleScript instance = ModuleManager.GetModule("Proline.ModuleFramework.TestModule");
            if (instance == null)
                return;
            List<Task> taskManger = new List<Task>();
            var task = new Task(async () => {
                while (true)
                {
                    await instance.OnTick();
                }
            });
            taskManger.Add(task);
            task.Start();
            ModuleManager.StartAllModules();
            Thread.Sleep(5000);
            instance.Disable();
            Console.ReadLine();
        }
    }
}
