using CitizenFX.Core.Native;
using Proline.Resource.ModuleCore;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting
{
    public class MScriptingContext : ModuleContext
    {
        private Log _log => new Log();
        private LWScriptManager _lwScriptManager;

        public override void OnStart()
        {
            var name = Modules.GetCurrentModuleName();
            _log.Debug(name);
            var lsAssembly = Modules.GetModuleData<string[]>(name, "LevelScriptAssemblies");
            _lwScriptManager = LWScriptManager.GetInstance();

            if (lsAssembly != null)
            {
                _log.Debug($"Loading level scripts from {lsAssembly.Count()} assemblies");
                foreach (var item in lsAssembly)
                {
                    _lwScriptManager.ProcessAssembly(item);

                }
            }


            MScriptingAPI.StartNewScript("Main");
        }
    }
}
