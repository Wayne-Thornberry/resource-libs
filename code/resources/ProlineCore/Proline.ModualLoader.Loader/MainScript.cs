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
        private List<Task> _modualTasks;
        private Dictionary<string, ComponentContainer> _modules;
        public ResourceMainScript()
        { 
            _modules = new Dictionary<string, ComponentContainer>();
            _modualTasks = new List<Task>();
        }

        public override async Task OnLoad()
        {
            try
            {
                Console.WriteLine("Loading Resources...");
                foreach (var item in Configuration.GetSection<string[]>("Resources"))
                {
                    Assembly.Load(item);
                }
                Console.WriteLine("Loaded Resources");
                var config = ModuleConfigSection.ModuleConfig;
                Console.WriteLine($"Loading modules from assemblies...");
                if (config == null)
                    throw new Exception("Modules failed to load, configuration failed");
                foreach (ModuleInstanceElement item in config.Modules)
                {
                    var container = CreateComponent(item.Name);
                    container.RegisterCommands();
                    _modules.Add(container.Name, container);

                    Console.WriteLine(string.Format("Invoking {0} {1}", container.Name, ComponentContainer.INITCORESCRIPTNAME));

                    // Init_Core
                    // - Finds all scripts that are marked InitializeCore
                    // - Execute Core Initializations
                    try
                    {
                        container.ExecuteScript(ComponentContainer.INITCORESCRIPTNAME);
                    }
                    catch (ScriptDoesNotExistException e)
                    {

                    }catch(Exception e)
                    {
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override async Task OnStart()
        {
            // Init_Session
            // - Find all scripts that are marked InitializeSession
            // - Execute Session Intializations
            try
            { 
                foreach (var container in _modules.Values)
                {
                    if (container.HasStarted) continue;
                    Console.WriteLine(string.Format("Invoking {0} {1}", container.Name, ComponentContainer.INITSESSIONSCRIPTNAME));
                    container.Start();
                    var task = container.Run();
                    _modualTasks.Add(task);
                    container.Enable();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override async Task OnUpdate()
        {
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 

        public ComponentContainer CreateComponent(string moduleName)
        {
            ModuleInstanceElement id = GetModualInstanceElement(moduleName); 
            if (id == null)
                return null;
            var assemblyString = id.Assembly;
            Console.WriteLine($"Loading {assemblyString}");
            var container = ComponentContainer.Load(assemblyString);
            Console.WriteLine($"Succesfully Loaded {container.Name}");
            return container;
        }

        private ModuleInstanceElement GetModualInstanceElement(string moduleName)
        {
            var config = ModuleConfigSection.ModuleConfig; 
            foreach (ModuleInstanceElement item in config.Modules)
            {
                if (item.Name.Equals(moduleName))
                { 
                    return item;
                }
            } 
            return null;
        }
    }
}
