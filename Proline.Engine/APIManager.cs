using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class APIManager
    {
        private static APIManager _instance;
        private Dictionary<string, ComponentAPI> _apis;
        private Dictionary<string, EngineComponent> _apisAndComponents;

        public APIManager()
        {
            _apis = new Dictionary<string, ComponentAPI>();
            _apisAndComponents = new Dictionary<string, EngineComponent>();
        }


        internal ComponentAPI GetAPI(string apiName)
        {
            if (_apis.ContainsKey(apiName))
                return _apis[apiName];
            return null;
        }

        internal static APIManager GetInstance()
        {
            if (_instance == null)
                _instance = new APIManager();
            return _instance;
        }

        internal void RegisterAPI(string apiName)
        {
            _apis.Add(apiName, null);
        }

        internal void RegisterAPI(object source, MethodInfo item, string apiName)
        { 
            _apis.Add(apiName, new ComponentAPI(source, item));
        }

        internal void RegisterAPI(ComponentAPI componentAP)
        {
            Debugger.LogDebug("Registered " + componentAP.Type + " " + componentAP.Name);
            //_apisAndComponents.Add(apiName, component);
            _apis.Add(componentAP.Name, componentAP);
        }

        internal void UnregisterAPI(ComponentAPI apiName)
        {
            _apis.Remove(apiName.Name);
        }

        internal IEnumerable<ComponentAPI> GetAPIs()
        {
            return _apis.Values;
        }
    }
}
