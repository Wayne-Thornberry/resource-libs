using CitizenFX.Core;
using Proline.Modularization.Core;
using Proline.Resource;
using Proline.Resource.Configuration;
using Proline.Resource.Framework;
using Proline.Resource.Logging;
using System.Reflection;
using System.Threading.Tasks;

namespace ProlineServer
{
    public class ResourceMainScript : ResourceScript
    {
        private static Log _log = new Log(); 
         

        public override async Task OnLoad()
        { 
            LoadResources();
            ModuleManager.LoadModules();
        }

        public override async Task OnStart()
        { 
            ModuleManager.StartAllModules();
        }

        public override async Task OnUpdate()
        {
            ModuleManager.ProcessModules();
            await BaseScript.Delay(10000);
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
