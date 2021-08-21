
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
        private Dictionary<string, ScriptPackage> _scriptPackages;
        private Dictionary<string, ScriptWrapper> _scriptWrappers;

        private ScriptManager()
        {
            _scriptWrappers = new Dictionary<string, ScriptWrapper>();
            _scriptPackages = new Dictionary<string, ScriptPackage>();  
        }

        internal static ScriptManager GetInstance()
        { 
            if (_instance == null)
                _instance = new ScriptManager();
            return _instance;
        } 
         
        internal void RegisterScript(string scriptName, ScriptPackage sp)
        {

            try
            {
                _scriptPackages.Add(scriptName, sp);
                _scriptWrappers.Add(scriptName, sp.GetScriptWrapper(scriptName));
            }
            catch (ArgumentException e)
            {

            }
            catch (Exception e)
            {
                throw;
            } 
        }

        internal int GetRegisteredScriptCount()
        {
            return _scriptPackages.Count;
        }

        internal void UnRegisterScript(string scriptName)
        {

            try
            {
                _scriptPackages.Remove(scriptName);
                _scriptWrappers.Remove(scriptName);
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
