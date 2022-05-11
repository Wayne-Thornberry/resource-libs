using CitizenFX.Core.Native;
using Proline.Resource.ModuleCore;
using Proline.Resource.Framework;
using Proline.Resource.Logging; 
using System.Reflection;
using System.Collections.Generic;
using Proline.Resource.Configuration;

namespace Proline.ResourceLoader.Main
{
    public class Resource : ResourceContext
    {
        private static Log _log = new Log();
        public static void Main(string[] args)
        { 
            var sections = ConfigManager.GetResourceConfigSection<string[]>("sections");
            Modules.LoadModules();
            foreach (var item in sections)
            { 
                if (item.Equals("Resources"))
                {
                    var resources = ConfigManager.GetResourceConfigSection<string[]>(item);
                    foreach (var resource in resources)
                    {
                        Assembly.Load(resource);
                    }
                }
            }

            Modules.StartAllModules();
        }
    }
}
