using CitizenFX.Core;
using Proline.Modularization.Core;
using Proline.Resource;
using Proline.Resource.Configuration;
using Proline.Resource.Framework;
using Proline.Resource.Logging;
using System.Reflection;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace ProlineServer
{
    public class ResourceMainScript : ResourceScript
    { 
        public override async Task OnLoad()
        { 
            LoadResources();
            ModuleManager.LoadModules();
        }

        public override async Task OnStart()
        { 
            // Init_Core
            // - Finds all scripts that are marked InitializeCore
            // - Execute Core Initializations
            // Init_Session
            // - Find all scripts that are marked InitializeSession
            // - Execute Session Intializations


            ModuleManager.StartAllModules();
            while (!ModuleManager.HasAllModulesStarted())
            {
                await Delay(0);
            }
        }

        public override async Task OnUpdate()
        { 
            ModuleManager.ProcessModules();  
        }

        private void LoadResources()
        {
            Console.WriteLine("Loading Resources...");
            foreach (var item in Configuration.GetSection<string[]>("Resources"))
            {
                Assembly.Load(item);
            }
            Console.WriteLine("Loaded Resources");
        }
    }
}
