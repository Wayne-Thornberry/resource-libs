using Proline.ClassicOnline.MScripting.Config;
using Proline.Modularization.Core;
using Proline.Resource.Eventing;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting
{
    internal partial class PlayerReadyEvent : LoudEvent
    {
        private Log _log => new Log();

        public static void InvokeEvent(string username)
        {  
            _event.Invoke(username); 
        }

        protected override object OnEventTriggered(params object[] args)
        {
            var name = ModuleManager.GetCurrentModuleName();
            var lsAssembly = ScriptingConfigSection.ModuleConfig;
            Console.WriteLine("Retrived config section");
            var _lwScriptManager = LWScriptManager.GetInstance();

            if (lsAssembly != null)
            {
                Console.WriteLine(_log.Debug($"Loading level scripts. from {lsAssembly.LevelScriptAssemblies.Count()} assemblies"));
                foreach (var item in lsAssembly.LevelScriptAssemblies)
                {
                    _lwScriptManager.ProcessAssembly(item);

                }
            }


            MScriptingAPI.StartNewScript("Main");
            return null;
        }
    }
}
