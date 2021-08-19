using System;
using System.Collections.Generic;

namespace Proline.Engine
{
    internal class ScriptCache
    {
        private static ScriptCache _instance;
        private Dictionary<string, Type> _scriptTypes;

        ScriptCache()
        {
            _scriptTypes = new Dictionary<string, Type>();
        }

        internal static ScriptCache GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptCache();
            return _instance;
        }

        internal bool DoesScriptExist(string scriptName)
        {
            if (_scriptTypes == null) return false;
            return _scriptTypes.ContainsKey(scriptName.ToLower());
        }

        internal Type GetScriptType(string scriptName)
        {
            return _scriptTypes[scriptName.ToLower()];
        }

        internal void CacheScriptType(Type t)
        {
            if (_scriptTypes == null || t == null) return;
            _scriptTypes.Add(t.Name.ToLower(), t);
        }

        internal int GetScriptCount()
        {
            if (_scriptTypes == null) return 0;
            return _scriptTypes.Count;
        }
    }
}
