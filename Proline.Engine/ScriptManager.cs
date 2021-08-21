
using Newtonsoft.Json;
using Proline.Engine.Data;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class ScriptManager
    {
        private static ScriptManager _instance;  
        private Dictionary<string, EngineScript> _scriptWrappers;

        private ScriptManager()
        {
            _scriptWrappers = new Dictionary<string, EngineScript>(); 
        }

        internal static ScriptManager GetInstance()
        { 
            if (_instance == null)
                _instance = new ScriptManager();
            return _instance;
        }  

        internal void RegisterScript(EngineScript scriptName)
        {
            Debugger.LogDebug("Registered " + scriptName.Type + " " + scriptName.Name);
            _scriptWrappers.Add(scriptName.Name, scriptName);
        }

        internal int GetScriptCount()
        {
            return _scriptWrappers.Count;
        }

        internal void UnregisterScript(EngineScript scriptName)
        {
            _scriptWrappers.Remove(scriptName.Name); 
        }

        internal EngineScript GetScriptWrapper(string scriptName)
        {
            return _scriptWrappers[scriptName];
        }
    }
}
