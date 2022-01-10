using Proline.Classic.Client.Engine.CScripting;
using Proline.Component.Framework.Client.Access;
using Proline.Resource.Client.Framework;
using Proline.Resource.Component.Events;
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
            scriptLibrary.ProcessAssembly("Proline.Resource.LevelScripts");
            _scriptManager = new ScriptManager(scriptLibrary);
        }

        public override void OnStart()
        {
            _scriptManager.StartScript("Main");
        }
    }
}
