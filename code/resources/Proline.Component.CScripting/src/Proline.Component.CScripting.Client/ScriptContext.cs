
using Proline.Component.Framework.Client.Access;
using Proline.Component.Framework.Client.Global;
using Proline.Resource.Client.Eventing;
using Proline.Resource.Client.Framework;
using Proline.Resource.Component.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Client.Engine.CScripting
{
    public class ScriptContext : ComponentContext
    {
        private ScriptManager _scriptManager;

        public ScriptContext()
        {
            EventHandlers.Add("StartScriptHandler", new Action<string, List<object>>(OnStartScript));
            ExportManager.CreateExport("StartScript", new Action<string, object[]>(StartScript));
        }

        private void StartScript(string x, params object[] args)
        {
            _scriptManager.StartScript(x, args);
        }

        private void OnStartScript(string arg1, List<object> arg2)
        { 
            _scriptManager.StartScript(arg1, arg2.ToArray());
        }

        public override void OnLoad()
        { 
            EventManager.AddEventListener("DoSomething", new Action<int>(T));
            EventManager.AddEventListener("DoSomething", new Action<int>(X));

            Globals.AddGlobal("Testing", 1);

            var ente =  new CEvent("CScripting", "DoSomething");
            ente.Invoke(1);

            var scriptLibrary = new ScriptLibrary();
            scriptLibrary.ProcessAssembly("Proline.Classic.LevelScripts");
            _scriptManager = new ScriptManager(scriptLibrary);
        }

        public override void OnStart()
        { 
            _scriptManager.StartScript("Main");
        }


        private void X(int x)
        {
            _log.Debug( x + " X played out");
        }

        private void T(int x)
        { 
            _log.Debug(x + " T played out");
        }
    }
}
