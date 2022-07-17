using CitizenFX.Core;
using Proline.ClassicOnline.Resource.Config;
using Proline.Resource;
using Proline.Resource.Configuration;
using Proline.Resource.Framework;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.Resource
{
    public class ResourceMainScript : ResourceScript
    {
        private Dictionary<string, ComponentContainer> _modules;
        public ResourceMainScript()
        { 
            _modules = new Dictionary<string, ComponentContainer>();
        }

        public override async Task OnLoad()
        {
            LoadResourceDepencies();
            var config = ModuleConfigSection.ModuleConfig;
            OutputToConsole($"Loading modules from assemblies...");
            if (config == null)
                throw new Exception("Modules failed to load, configuration failed");
            if (config != null)
            {
                foreach (ModuleInstanceElement item in config.Modules)
                {
                    var container = LoadModule(item.Name);
                    _modules.Add(container.Name, container);
                    container.RegisterCommands();

                    // Init_Core
                    // - Finds all scripts that are marked InitializeCore
                    // - Execute Core Initializations
                    container.ExecuteScript(ComponentContainer.INITCORESCRIPTNAME);
                }
            }
        }

        public override async Task OnStart()
        {
            // Init_Session
            // - Find all scripts that are marked InitializeSession
            // - Execute Session Intializations 
            foreach (var moduleName in _modules.Keys)
            {
                if (_modules.ContainsKey(moduleName))
                {
                    var module = _modules[moduleName];
                    module.ExecuteScript(ComponentContainer.INITSESSIONSCRIPTNAME); 
                    module.HasStarted = true;
                }
            }
        }

        public override async Task OnUpdate()
        {
            foreach (var module in _modules.Values)
            {
                module.Run();
            }
        }

        private void LoadResourceDepencies()
        {
            Console.WriteLine("Loading Resources...");
            foreach (var item in Configuration.GetSection<string[]>("Resources"))
            {
                Assembly.Load(item);
            }
            Console.WriteLine("Loaded Resources");
        }


        public ComponentContainer LoadModule(string moduleName)
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
            var container = new ComponentContainer(assembly);
            container.Load();
            OutputToConsole($"Succesfully Loaded {container.Name}"); 
            return container;
        }

         
        private void OutputToConsole(string data)
        {
            Console.WriteLine(data);
        }

    }
}
