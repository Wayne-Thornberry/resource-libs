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
        private Dictionary<int, APIInvoker> _apis; 

        public APIManager()
        {
            _apis = new Dictionary<int, APIInvoker>();
        }


        internal APIInvoker GetAPI(int apiName)
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

        internal void RegisterAPI(APIInvoker componentAP)
        {
            try
            { 
                Debugger.LogDebug("Registered " + componentAP.ToString());
                //_apisAndComponents.Add(apiName, component);
                _apis.Add(componentAP.GetHashCode(), componentAP);
            }
            catch (ArgumentException e)
            {
                Debugger.LogDebug("Could not add API, same API already exists");
            }
        }

        internal void UnregisterAPI(APIInvoker apiName)
        {
            _apis.Remove(apiName.GetHashCode());
        }

        internal IEnumerable<APIInvoker> GetAPIs()
        {
            return _apis.Values;
        }
    }
}
