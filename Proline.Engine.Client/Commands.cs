
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
            //EngineObject.StartNewScript(args[0]);
        }

        [Command("RequestScriptStop")]
        public void RequestScriptStop(string[] args)
        {
            //EngineObject.RequestScriptStop(args[0]);
        }
    }
}
