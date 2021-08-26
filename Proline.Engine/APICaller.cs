
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class APICaller
    { 
        public static async Task<object> CallAPIAsync(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null)
                return null;
            return await api.InvokeAsync(inputParameters);
        }

        public static async Task<T> CallAPIAsync<T>(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null)
                return default;
            return await api.InvokeAsync<T>(inputParameters); 
        }

        public static object CallAPI(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null) return null;
            return api.Invoke(inputParameters);
        }

        public static T CallAPI<T>(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null) return default;
            return (T) Convert.ChangeType(api.Invoke(inputParameters), typeof(T));
        }
    }
}
