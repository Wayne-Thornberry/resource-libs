using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Classic.Client.Engine.CScripting;
using Proline.Resource.Framework;
using System;
using System.Collections.Generic;

namespace Proline.Resource.Engine.Client
{
    public class CoreContext : ComponentContext
    {
        private ScriptManager _scriptManager;

        public CoreContext()
        {
            EventManager.AddEventListenerV2("StartScriptHandler", new Action<string, object[]>(OnStartScript));
        } 
     
        private void OnStartScript(string arg1, params object[] arg2)
        {
            _log.Debug(arg1 + " Script Start Request");
            _scriptManager.StartScript(arg1, arg2);
        }

        public override void OnLoad()
        {
            var scriptLibrary = new ScriptLibrary();
            scriptLibrary.ProcessAssembly("Proline.Game.Scripts");
            _scriptManager = new ScriptManager(scriptLibrary);
        }

        public override void OnStart()
        {
            _scriptManager.StartScript("Main");
        }
    }
}
