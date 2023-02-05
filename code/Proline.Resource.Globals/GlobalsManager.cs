using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Globals
{
    public class GlobalsManager
    {
        private static GlobalsManager _instance;
        public StateBag NetworkedProperties { get; set; }
        public Dictionary<string, object> GlobalProperties { get; set; }

        public GlobalsManager()
        {
            GlobalProperties = new Dictionary<string, object>();
        }

        public static GlobalsManager GetInstance()
        {
            if (_instance == null)
                _instance = new GlobalsManager();
            return _instance;
        }

        public void SetGlobal(string globalName, object value, bool isNetworked)
        {
            if (isNetworked)
            {
                NetworkedProperties[globalName] = value;
            }
            else if (GlobalProperties.ContainsKey(globalName))
            {
                GlobalProperties[globalName] = value;
            } else
            { 
                GlobalProperties.Add(globalName, value);
            }
        }

        public object GetGlobal(string globalName, bool isNetworked)
        {
            if (isNetworked)
                return NetworkedProperties[globalName];
            if(GlobalProperties.ContainsKey(globalName))
                return GlobalProperties[globalName];
            return default;
        }

        public T GetGlobal<T>(string globalName, bool isNetworked)
        {
            if (isNetworked)
                return (T) NetworkedProperties[globalName];
            if (GlobalProperties.ContainsKey(globalName))
                return (T) GlobalProperties[globalName];
            return default;
        }
    }
}
