using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.Modularization.Core.Config;
using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

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


        public static void StartModule(string moduleName)
        {
            var mm = GetInstance();
            if (mm._modules.ContainsKey(moduleName))
            {
                var module = mm._modules[moduleName];
                if(module.Scripts != null)
                {
                    module.StartupScript = module.Scripts.FirstOrDefault(e => e.GetType().Name.Equals("Startup"));
                    if (module.StartupScript != null)
                        module.StartupScript.Execute();
                }

                foreach (var item in mm._modules[moduleName].Commands)
                {
                    item.RegisterCommand();
                } ;
                module.HasStarted = true;
            }
        }

        public static bool HasAllModulesStarted()
        {
            var mm = GetInstance();
            foreach (var item in mm._modules)
            {
                if (!item.Value.IsStarted)
                {
                    Console.WriteLine(String.Format("{0} Module has not started yet", item.Value.Name)); 
                    return false;
                }
            }
            return true;
        } 

        public static void ProcessModules()
        {
            var mm = GetInstance();
            foreach (var item in mm._modules.Values)
            {
                if (item.IsStarted)
                {  
                    foreach (var script in item.Scripts)
                    {
                        script.Execute();
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
            //BaseScript.RegisterScript(module);
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
            var commandTypes = assembly.GetTypes().Where(e => e.BaseType == typeof(ResourceCommand)).ToArray();
            OutputToConsole($"Getting object from assembly {assembly.FullName}");
            var scripts = new List<ModuleScript>();
            var commands = new List<ResourceCommand>();
            foreach (var item in scriptTypes)
            {
                scripts.Add((ModuleScript)Activator.CreateInstance(item)); 
            }

            foreach (var item in commandTypes)
            {
                commands.Add((ResourceCommand)Activator.CreateInstance(item));
            }
            OutputToConsole($"Inserted {assembly.GetName().Name} into memory");

            var container = new ModuleContainer();
            container.Name = assembly.GetName();
            container.Scripts = scripts;
            container.Assembly = assembly;
            container.Commands = commands;
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
