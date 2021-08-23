using System.Collections.Generic;

namespace Proline.Core.Client.Components.CGlobals
{
    public class GlobalsManager
    {
        private static GlobalsManager _instance;
        private Dictionary<string, object> keyValuePairs;
        private Dictionary<string, bool> isLocalGlobals;

        public GlobalsManager()
        {
            keyValuePairs = new Dictionary<string, object>();
            isLocalGlobals = new Dictionary<string, bool>();
        }

        public void AddGlobal(string globalName, object value, bool isLocal = false)
        { 
            keyValuePairs.Add(globalName, value);
            isLocalGlobals.Add(globalName, isLocal);
        }

        public void RemoveGlobal(string globalName)
        {
            keyValuePairs.Remove(globalName);
            isLocalGlobals.Remove(globalName);
        }

        public void UpdateGlobal(string globalName, object value, bool isLocal = false)
        {
            keyValuePairs[globalName] = value;
            isLocalGlobals[globalName] = isLocal;
        }

        public object GetGlobal(string globalName)
        {
            return keyValuePairs[globalName];
        }

        public Dictionary<string, object> ListGlobals()
        {
            return keyValuePairs;
        }

        public void ClearGlobals()
        {
            keyValuePairs.Clear();
            isLocalGlobals.Clear();
        }

        public static GlobalsManager GetInstance()
        {
            if (_instance == null)
                _instance = new GlobalsManager();
            return _instance;
        }
    }
}
