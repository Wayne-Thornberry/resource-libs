using CitizenFX.Core.Native;
using Proline.Resource.ModuleCore;
using Proline.Resource.Framework;
using Proline.Resource.Logging;
using Proline.ResourceConfiguration;
using Proline.ResourceLoader.Main.Configuration;

namespace Proline.ResourceLoader.Main
{
    public class Resource : ResourceContext
    {
        private static Log _log = new Log();
        public static void Main(string[] args)
        {
            var config = ConfigManager.GetConfig<COConfiguration>(API.GetCurrentResourceName());
            Modules.LoadModules(config.ModuleConfigs);
            Modules.StartAllModules();
        }
    }
}
