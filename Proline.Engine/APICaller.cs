
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class APICaller
    {
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
