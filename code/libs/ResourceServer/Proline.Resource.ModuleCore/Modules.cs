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
        public static Module LoadModule(string moduleName)
        { 
            var processor = new EModuleProcessor();
            return processor.LoadModule(moduleName);
        }

        public static void StartAllModules()
        {
            var mm = ModuleManager.GetInstance();
            mm.StartAllModules();
        }

        public static void LoadModules()
        {
            var processor = new EModuleProcessor();
            processor.LoadModules();
        }

        public static T GetModuleData<T>(string name, string key)
        {
            var mm = ModuleManager.GetInstance();
            return mm.GetModuleData<T>(name, key);
        }


        public static object GetModuleData(string name, string key)
        {
            var mm = ModuleManager.GetInstance(); 
            return mm.GetModuleData(name,key);
        }

        public static string GetCurrentModuleName()
        {
            var callingAsembly = Assembly.GetCallingAssembly();
            var mm = ModuleManager.GetInstance();
            if(mm.IsAssemblyModule(callingAsembly))
                return callingAsembly.GetName().Name;
            else
                return ""; 
        }

        public static void StartModule(string module)
        {
            var mm = ModuleManager.GetInstance();
            mm.StartModule(module);
        }
    }
}
