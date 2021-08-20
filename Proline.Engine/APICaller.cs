
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class APICaller
    {
        public static async Task<T> CallAPIAsync<T>(string apiName, params object[] inputParameters)
        {
            try
            { 
                var cm = APIManager.GetInstance();
                var api = cm.GetAPI(apiName);
                if (api == null)
                {
                    return await EngineAccess.ExecuteEngineMethodServer<T>(apiName, inputParameters);
                }
                else
                {
                    return (T)Convert.ChangeType(api.Invoke(inputParameters), typeof(T));
                }
            }
            catch (Exception e)
            {
                Debugger.LogError(e.ToString(), true);
                throw;
            }
        }

        public static async Task<object> CallAPIAsync(string apiName, params object[] inputParameters)
        {
            var cm = APIManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null)
                return await EngineAccess.ExecuteEngineMethodServer<object>(apiName, inputParameters);
            else
                return api.Invoke(inputParameters);
        }

        public static object CallAPI(string apiName, params object[] inputParameters)
        {
            var cm = APIManager.GetInstance();
            var api = cm.GetAPI(apiName);
            return api.Invoke(inputParameters);
        }

        public static object CallNativeAPI<T>(ulong hash, params object[] inputParameters)
        { 
            return CitizenAccess.GetInstance().CallFunction<T>(hash, inputParameters);
        }
        public static void CallNativeAPI(ulong hash, params object[] inputParameters)
        { 
             CitizenAccess.GetInstance().CallFunction(hash, inputParameters);
        }
    }
}
