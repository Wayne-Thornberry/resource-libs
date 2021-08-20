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
        private Dictionary<string, APIKeyPair> _apis;

        public APIManager()
        {
            _apis = new Dictionary<string, APIKeyPair>();
        }


        internal APIKeyPair GetAPI(string apiName)
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
            _apis.Add(apiName, new APIKeyPair(source, item));
        }
    }
}
