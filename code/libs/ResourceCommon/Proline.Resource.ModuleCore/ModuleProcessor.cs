using Proline.Resource.Console;
using Proline.Resource.ModuleCore.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore
{
    public abstract class ModuleProcessor : IConsoleOutput
    {
        protected Dictionary<string, Module> Modules { get; set; }
        protected ModuleConfiguration Config { get; set; }

        public ModuleProcessor()
        {
            Modules = new Dictionary<string, Module>();
        }

        public abstract ModuleConfig GetModuleConfig(string moduleName);
        public abstract ModuleConfiguration GetModuleConfiguration();

        public void SaveModule(string moduleName, Module module)
        {
            var mm = ModuleManager.GetInstance();
            mm.RegisterModule(moduleName, module);
        }

        public virtual void OutputToConsole(string data) { }

        public Module LoadModule(string moduleName)
        {
            var config = GetModuleConfig(moduleName);
            var assemblyString = config.Assembly;
            OutputToConsole($"Loading {assemblyString}");
            var assembly = Assembly.Load(assemblyString); 
            OutputToConsole($"Succesfully Loaded {assembly.FullName}");
            var type = assembly.GetTypes().FirstOrDefault(e => e.BaseType == typeof(ModuleContext));
            OutputToConsole($"Getting object from assembly {assembly.FullName}");
            ModuleContext instance = (ModuleContext)Activator.CreateInstance(type);
            if (type == null)
            {
                OutputToConsole($"No modual start found in {assembly.FullName}");
            }

            var basescripts = assembly.GetTypes().Where(e => e.BaseType == typeof(IModuleScript));
            var bsInstances = new List<IModuleScript>();

            foreach (var item in basescripts)
            {
                bsInstances.Add((IModuleScript)Activator.CreateInstance(item));
            }

            var module = new Module()
            {
                BaseScripts = bsInstances,
                Name = assembly.GetName(),
                Data = config.Data,
                Context = instance,
            };
            OutputToConsole($"Inseted {assembly.GetName().Name} into memory");
            instance.OnLoad();
            SaveModule(assembly.GetName().Name, module);
            return module;
        }



        public void LoadModules()
        {
            var moduleConfiguration = GetModuleConfiguration();
            OutputToConsole($"Loading modules from assemblies...");
            if (moduleConfiguration == null)
                throw new Exception("Modules failed to load, configuration failed");
            if (moduleConfiguration.DoesConfigSectionExist("Modules"))
            {
                foreach (var item in moduleConfiguration.Modules)
                {
                    LoadModule(item);
                }
            }
        }
    }
}
