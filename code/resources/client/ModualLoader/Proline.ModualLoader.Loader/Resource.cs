using CitizenFX.Core.Native;
using Proline.Resource.ModuleCore;
using Proline.Resource.Framework;
using Proline.Resource.Logging;
using Proline.ResourceConfiguration;
using Proline.ResourceLoader.Main.Configuration;
using System.Reflection;

namespace Proline.ResourceLoader.Main
{
    public class Resource : ResourceContext
    {
        private static Log _log = new Log();
        public static void Main(string[] args)
        {
            var config = ConfigManager.GetConfig<COConfiguration>(API.GetCurrentResourceName());

            foreach (var item in config.Resources)
            {
                Assembly.Load(item);
            }

            Modules.LoadModules(config.ModuleConfigs);
            Modules.StartAllModules();
        }
    }
}
