using CitizenFX.Core.Native;
using Proline.ClassicOnline.MScripting.Config;
using Proline.Modularization.Core;
using Proline.Resource.Configuration;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting
{
    public class ScriptProcessor : ModuleScript
    {
        private Log _log => new Log();
        private LWScriptManager _lwScriptManager; 

        public override async Task OnExecute()
        {
            var name = ModuleManager.GetCurrentModuleName();
            var lsAssembly = ScriptingConfigSection.ModuleConfig;
            Console.WriteLine("Retrived config section");
            _lwScriptManager = LWScriptManager.GetInstance();

            if (lsAssembly != null)
            {
                Console.WriteLine(_log.Debug($"Loading level scripts from {lsAssembly.LevelScriptAssemblies.Count()} assemblies"));
                foreach (var item in lsAssembly.LevelScriptAssemblies)
                {
                    _lwScriptManager.ProcessAssembly(item);

                }
            }


            MScriptingAPI.StartNewScript("Main");
        }
    }
}
