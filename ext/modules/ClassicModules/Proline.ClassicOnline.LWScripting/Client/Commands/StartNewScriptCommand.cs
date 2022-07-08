using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Commands
{
    public class StartNewScriptCommand : ModuleCommand
    {
        public StartNewScriptCommand() : base("StartNewScript")
        {
        }

        protected override Delegate GetCommandHandler()
        {
            return new Action<string>(OnStartNewScript);
        }

        private void OnStartNewScript(string scriptName)
        {
            MScriptingAPI.StartNewScript(scriptName);
        }
    }
}
