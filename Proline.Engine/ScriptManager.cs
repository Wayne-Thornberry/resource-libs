
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
        private Dictionary<string, ScriptWrapper> _scriptWrappers;

        private ScriptManager()
        {
            _scriptWrappers = new Dictionary<string, ScriptWrapper>(); 
        }

        internal static ScriptManager GetInstance()
        { 
            if (_instance == null)
                _instance = new ScriptManager();
            return _instance;
        }  

        internal void RegisterScript(ScriptWrapper scriptName)
        {
            _scriptWrappers.Add(scriptName.Name, scriptName);
        }

        internal int GetScriptCount()
        {
            return _scriptWrappers.Count;
        }

        internal void UnregisterScript(ScriptWrapper scriptName)
        {

            try
            { 
                _scriptWrappers.Remove(scriptName.Name);
            }
            catch (ArgumentException e)
            {

            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal ScriptWrapper GetScriptWrapper(string scriptName)
        {
            return _scriptWrappers[scriptName];
        }
    }
}
