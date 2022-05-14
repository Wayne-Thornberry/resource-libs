using CitizenFX.Core.Native;
using Proline.Resource.ModuleCore;
using Proline.Resource.Framework;
using Proline.Resource.Logging;
using System.Reflection;
using System.Collections.Generic;
using Proline.Resource.Configuration;
using Proline.Resource.Console;

namespace ProlineServer
{
    public class Resource : ResourceContext
    {
        private static Log _log = new Log();
        public static void Main(string[] args)
        {
            LoadResources();
            Modules.LoadModules();
            Modules.StartAllModules();
        }

        private static void LoadResources()
        {
            EConsole.WriteLine("Loading Resources...");
            foreach (var item in ConfigManager.GetResourceConfigSection<string[]>("Resources"))
            {
                Assembly.Load(item);
            }
            EConsole.WriteLine("Loaded Resources");
        }
    }
}
