using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Script
{
    public partial class EngineScript : BaseScript
    {
        [Command("ExecuteScript")]
        public void ExecuteScript(string[] args)
        {
            EngineAccess.StartNewScript(args[0]);
        }

        [Command("RequestScriptStop")]
        public void RequestScriptStop(string[] args)
        {
            EngineAccess.RequestScriptStop(args[0]);
        }
    }
}
