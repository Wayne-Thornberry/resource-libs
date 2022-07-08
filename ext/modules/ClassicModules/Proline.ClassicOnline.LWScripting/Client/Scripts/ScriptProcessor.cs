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

        public ScriptProcessor() : base(true)
        {

        }

        public override async Task OnExecute()
        {
            var sm = LWScriptManager.GetInstance();  
            var scripts = sm.GetRunningScripts().ToArray(); 
            foreach (var scriptName in scripts)
            {
                var tasks = sm.GetScriptTasks(scriptName).ToArray();
                var terminatedTasks = tasks.Where(e => e.IsCompleted).ToArray();
                foreach (var item in tasks)
                {
                    sm.TerminateTask(scriptName, item);
                }
            }
        }

    }
}
