using Proline.ClassicOnline.MScripting.Config;
using Proline.ClassicOnline.MScripting.Internal;
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

        public static void InvokeEvent(string username)
        {  
            _event.Invoke(username); 
        }

        protected override object OnEventTriggered(params object[] args)
        { 
            var lsAssembly = ScriptingConfigSection.ModuleConfig;
            Console.WriteLine("Retrived config section");
            var _lwScriptManager = ScriptTypeLibrary.GetInstance();

            if (lsAssembly != null)
            {
                Console.WriteLine($"Loading level scripts. from {lsAssembly.LevelScriptAssemblies.Count()} assemblies");
                foreach (var item in lsAssembly.LevelScriptAssemblies)
                {
                    _lwScriptManager.ProcessAssembly(item); 
                }
                ScriptTypeLibrary.HasLoadedScripts = true;
            }


            MScriptingAPI.StartNewScript("Main");
            return null;
        }
    }
}
