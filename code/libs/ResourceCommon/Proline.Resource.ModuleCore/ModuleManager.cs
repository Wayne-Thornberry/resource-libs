using Newtonsoft.Json;
using Proline.Resource.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore
{
    public class ModuleManager 
    {
        private static ModuleManager _instance;
        private Dictionary<string, Module> _modules;

        private ModuleManager()
        {
            _modules = new Dictionary<string, Module>();
        }

        public T GetModuleData<T>(string name, string key)
        {
            return JsonConvert.DeserializeObject<T>(GetModuleData(name, key).ToString());
        }

        public bool IsAssemblyModule(Assembly assembly)
        {
            return _modules.ContainsKey(assembly.GetName().Name);
        }

        public object GetModuleData(string name, string key)
        {
            //Resource.Console.Console.WriteLine(_log.Debug($"Retriving module data from {name} key {key}"));
            if (_modules.ContainsKey(name))
            {
                //Resource.Console.Console.WriteLine(_log.Debug($"Module exists, retriving {key}"));
                var mod = _modules[name];

                if (mod.Data != null)
                {
                    if (mod.Data.ContainsKey(key))
                    {
                        //Resource.Console.Console.WriteLine(_log.Debug($"Key exists, retriving..."));
                        return mod.Data[key];
                    }
                    else
                    {
                        foreach (var item in mod.Data.Keys)
                        {
                            //Resource.Console.Console.WriteLine(_log.Debug($"Data Key: {item}"));
                        }
                        //Resource.Console.Console.WriteLine(_log.Debug($"Key does not exist, are you sure the key is correct?"));
                    }
                }
                else
                {
                    //Resource.Console.Console.WriteLine(_log.Debug($"No module data found"));
                }
            }
            else
            {
                //Resource.Console.Console.WriteLine(_log.Debug($"module does not exist, are you sure the module is correct?"));
            }
            return null;
        }

        public void StartModule(string module)
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

        public void StartAllModules()
        { 
            foreach (var ass in _modules.Keys)
            {
                StartModule(ass);
            }
        }

        public void RegisterModule(string name, Module module)
        {
            _modules.Add(name, module);
        }

        public static ModuleManager GetInstance()
        {
            if (_instance == null)
                _instance = new ModuleManager();
            return _instance;
        }
    }
}
