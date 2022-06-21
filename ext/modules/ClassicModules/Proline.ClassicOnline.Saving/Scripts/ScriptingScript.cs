using CitizenFX.Core;
using Proline.DBAccess.Proxies;
using Proline.Resource.Console;
using Proline.Resource.ModuleCore;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Scripts
{
    public class ScriptingScript : ModuleScript
    {

        public ScriptingScript() : base()
        {
            EventHandlers.Add("ScriptStartedHandler", new Action<Player,string>(OnScriptStart));
            EventHandlers.Add("ScriptTerminatedHandler", new Action<Player,string>(OnScriptTerminated));
        }

        private void OnScriptStart([FromSource] Player player, string scriptName)
        {
            EConsole.WriteLine(scriptName);
        }

        private void OnScriptTerminated([FromSource] Player player, string scriptName)
        {
            EConsole.WriteLine(scriptName);
        }

        public override async Task OnStart()
        {

        }

        public override async Task OnUpdate()
        {

        }
    }
}
