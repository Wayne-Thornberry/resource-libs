using Proline.Resource.Configuration;
using Proline.Resource.Logging;
using Proline.Resource.ModuleCore.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore
{
    internal class EModuleProcessor : ModuleProcessor
    {
        private static Log _log = new Log();

        public override void OutputToConsole(string data)
        {
            Resource.Console.EConsole.WriteLine(_log.Debug(data));
        }

        public override ModuleConfiguration GetModuleConfiguration()
        {
            if (Config == null)
                Config = ConfigManager.GetResourceConfigSection<ModuleConfiguration>("ModuleConfiguration");
            return Config;
        }

        public override ModuleConfig GetModuleConfig(string moduleName)
        {
            var moduleConfiguration = GetModuleConfiguration();
            var moduleConfig = moduleConfiguration.GetConfigSection<ModuleConfig>(moduleName);// ConfigManager.GetResourceConfigSection<ModuleConfig>(moduleName);
            return moduleConfig;
        }
    }
}
