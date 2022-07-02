using Newtonsoft.Json;
using Proline.Modularization.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Modularization.Core
{
    public class ModuleManager
    {
        private static ModuleManager _instance;

        private static ModuleManager GetInstance()
        {
            if (_instance == null)
                _instance = new ModuleManager();
            return _instance;
        }

        private Dictionary<string, ModuleContainer> _modules;

        private ModuleManager()
        {
            _modules = new Dictionary<string, ModuleContainer>();
        }

        public static ModuleContainer GetModule(string v)
        {
            var mm = GetInstance();
            if (mm._modules.ContainsKey(v))
                return mm._modules[v];
            return null;

        }

        public static bool IsAssemblyModule(Assembly assembly)
        {
            var mm = GetInstance();
            return mm._modules.ContainsKey(assembly.GetName().Name);
        }


        public static void StartModule(string module)
        {
            var mm = GetInstance();
            if (mm._modules.ContainsKey(module))
            {
               // mm._modules[module].Enable();
            }
        }

        public static void ProcessModules()
        {
            var mm = GetInstance();
            foreach (var item in mm._modules.Values)
            {
                for (int i = 0; i < item.TaskManager.Count; i++)
                {
                    var task = item.TaskManager[i];
                    if(task.IsCompleted || task.IsFaulted || task.IsCanceled)
                        item.TaskManager.Remove(task);
                }
                 
                foreach (var script in item.Scripts)
                {
                    if (!script.IsFinished)
                    { 
                        var task = script.Execute();
                        item.TaskManager.Add(task);
                    }
                }
            }
        }

        public static void StartAllModules()
        {
            var mm = GetInstance();
            foreach (var ass in mm._modules.Keys)
            {
                StartModule(ass);
            }
        }

        public static void RegisterModule(string name, ModuleContainer module)
        {
            var mm = GetInstance();
            mm._modules.Add(name, module);
        }

        public static ModuleContainer LoadModule(string moduleName)
        {
            var config = ModuleConfigSection.ModuleConfig;
            ModuleInstanceElement id = null;
            foreach (ModuleInstanceElement item in config.Modules)
            {
                if (item.Name.Equals(moduleName))
                {
                    id = item;
                    break;
                }
            }

            if (id == null)
                return null;
            var assemblyString = id.Assembly;
            OutputToConsole($"Loading {assemblyString}");
            var assembly = Assembly.Load(assemblyString);
            OutputToConsole($"Succesfully Loaded {assembly.FullName}");
            var scriptTypes = assembly.GetTypes().Where(e => e.BaseType == typeof(ModuleScript)).ToArray();
            OutputToConsole($"Getting object from assembly {assembly.FullName}");
            var scripts = new List<ModuleScript>();
            foreach (var item in scriptTypes)
            {
                scripts.Add((ModuleScript)Activator.CreateInstance(item)); 
            } 
            OutputToConsole($"Inserted {assembly.GetName().Name} into memory");

            var container = new ModuleContainer();
            container.Name = assembly.GetName();
            container.Scripts = scripts;
            container.Assembly = assembly;
            RegisterModule(assembly.GetName().Name, container);
            return container;
        }


        public static void LoadModules()
        {
            var x = ModuleConfigSection.ModuleConfig;
            OutputToConsole($"Loading modules from assemblies...");
            if (x == null)
                throw new Exception("Modules failed to load, configuration failed");
            if (x != null)
            {
                foreach (ModuleInstanceElement item in x.Modules)
                {
                    LoadModule(item.Name);
                }
            }
        }

        public static string GetCurrentModuleName()
        {
            var callingAsembly = Assembly.GetCallingAssembly();
            if (IsAssemblyModule(callingAsembly))
                return callingAsembly.GetName().Name;
            else
                return "";
        }
        private static void OutputToConsole(string data)
        {
            Console.WriteLine(data);
        }

    }
}
