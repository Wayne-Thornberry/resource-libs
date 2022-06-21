using Newtonsoft.Json;
using Proline.ModuleFramework.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ModuleFramework.Core
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

        private Dictionary<string, ModuleScript> _modules;

        private ModuleManager()
        {
            _modules = new Dictionary<string, ModuleScript>();
        }

        public static ModuleScript GetModule(string v)
        {
            var mm = ModuleManager.GetInstance();
            if (mm._modules.ContainsKey(v))
                return mm._modules[v];
            return null;

        }

        public static bool IsAssemblyModule(Assembly assembly)
        {
            var mm = ModuleManager.GetInstance();
            return mm._modules.ContainsKey(assembly.GetName().Name);
        }


        public static void StartModule(string module)
        {
            var mm = ModuleManager.GetInstance(); 
            if (mm._modules.ContainsKey(module))
            {
                mm._modules[module].Enable();
            } 
        }

        public static void StartAllModules()
        {
            var mm = ModuleManager.GetInstance();
            foreach (var ass in mm._modules.Keys)
            {
                StartModule(ass);
            }
        }

        public static void RegisterModule(string name, ModuleScript module)
        {
            var mm = ModuleManager.GetInstance();
            mm._modules.Add(name, module);
        }

        public static ModuleScript LoadModule(string moduleName)
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
            var types = assembly.GetTypes().Where(e => e.BaseType == typeof(ModuleScript)).ToArray();
            if(types.Length > 1)
            {
                throw new Exception("Detected more than 1 module script in the following assembly {0} Only 1 module script can exist per module");
            }
            OutputToConsole($"Getting object from assembly {assembly.FullName}");
            var type = types[0];
            ModuleScript instance = (ModuleScript)Activator.CreateInstance(type, assembly);
            if (type == null)
            {
                OutputToConsole($"No modual start found in {assembly.FullName}");
            }
            OutputToConsole($"Inserted {assembly.GetName().Name} into memory");
            // instance.OnLoad();
            RegisterModule(assembly.GetName().Name, instance);
            return instance;
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
