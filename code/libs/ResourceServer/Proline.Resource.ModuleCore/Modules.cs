using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Proline.Resource.Logging;
using Proline.Resource.ModuleCore.Configuration;
using Proline.Resource.Configuration;

namespace Proline.Resource.ModuleCore
{
    public static class Modules
    {
        private static Log _log = new Log();
        private static Dictionary<string, Module> _modules;

        public static Module LoadModule(string moduleName)
        {
            if (_modules == null)
                _modules = new Dictionary<string, Module>();

            var moduleConfiguration = ConfigManager.GetResourceConfigSection<ModualConfiguration>("ModualConfiguration");

            var moduleConfig = moduleConfiguration.GetConfigSection<ModuleConfig>(moduleName);// ConfigManager.GetResourceConfigSection<ModuleConfig>(moduleName);

            var assembly = moduleConfig.Assembly;

            Resource.Console.Console.WriteLine(_log.Debug($"Loading {assembly}"));
            var ass = Assembly.Load(assembly);

            // ResourceConsole.Console.WriteLine(_log.Debug($"Getting object from assembly {ass.FullName}"));
            var type = ass.GetTypes().FirstOrDefault(e => e.BaseType == typeof(ModuleContext));

            ModuleContext instance = (ModuleContext)Activator.CreateInstance(type);
            if (type == null)
            {
                // ResourceConsole.Console.WriteLine(_log.Debug($"No modual start found in {ass.FullName}"));
            }

            Resource.Console.Console.WriteLine(_log.Debug($"Succesfully Loaded {assembly}"));
            var basescripts = ass.GetTypes().Where(e => e.BaseType == typeof(ModuleScript));
            var bsInstances = new List<ModuleScript>();

            foreach (var item in basescripts)
            {
                bsInstances.Add((ModuleScript) Activator.CreateInstance(item));
            }

            var module = new Module()
            {
                BaseScripts = bsInstances,
                Name = ass.GetName(),
                Data = moduleConfig.Data,
                Context = instance,
            };
            // ResourceConsole.Console.WriteLine(_log.Debug($"Inseted {ass.GetName().Name} into memory"));
            instance.OnLoad();
            _modules.Add(ass.GetName().Name, module);
            return module;
        }

        public static void StartAllModules()
        {
            // ResourceConsole.Console.WriteLine(_log.Debug($"Retriving starting object from modules..."));
            foreach (var ass in _modules.Keys)
            {
                StartModule(ass);
            }
        }

        public static void LoadModules()
        {
            var moduleConfiguration = ConfigManager.GetResourceConfigSection<ModualConfiguration>("ModualConfiguration");
            Resource.Console.Console.WriteLine(_log.Debug($"Loading modules from assemblies..."));
            foreach (var item in moduleConfiguration.Modules)
            {
                LoadModule(item);
            }
        }

        public static T GetModuleData<T>(string name, string key)
        {
            return JsonConvert.DeserializeObject<T>(GetModuleData(name, key).ToString());
        }


        public static object GetModuleData(string name, string key)
        {
            Resource.Console.Console.WriteLine(_log.Debug($"Retriving module data from {name} key {key}"));
            if (_modules.ContainsKey(name))
            {
                Resource.Console.Console.WriteLine(_log.Debug($"Module exists, retriving {key}"));
                var mod = _modules[name];

                if (mod.Data != null)
                {
                    if (mod.Data.ContainsKey(key))
                    {
                        Resource.Console.Console.WriteLine(_log.Debug($"Key exists, retriving..."));
                        return mod.Data[key];
                    }
                    else
                    {
                        foreach (var item in mod.Data.Keys)
                        {
                            Resource.Console.Console.WriteLine(_log.Debug($"Data Key: {item}"));
                        }
                        Resource.Console.Console.WriteLine(_log.Debug($"Key does not exist, are you sure the key is correct?"));
                    }
                }
                else
                {
                    Resource.Console.Console.WriteLine(_log.Debug($"No module data found"));
                }
            }
            else
            {
                Resource.Console.Console.WriteLine(_log.Debug($"module does not exist, are you sure the module is correct?"));
            }
            return null;
        }

        public static string GetCurrentModuleName()
        {
            var callingAsembly = Assembly.GetCallingAssembly();
            if (_modules.ContainsKey(callingAsembly.GetName().Name))
                return _modules[callingAsembly.GetName().Name].Name.Name;
            else
                return "";
        }

        public static void StartModule(string module)
        {
            if (_modules.ContainsKey(module))
            {
                _modules[module].Context.OnStart();
                //foreach (var item in _modules[module].BaseScripts)
                //{
                //    item.OnTick();
                //}
            }
            //else
            // ResourceConsole.Console.WriteLine(_log.Debug($"Cannot start module {module} as module does not exist")); 
        }
    }
}
