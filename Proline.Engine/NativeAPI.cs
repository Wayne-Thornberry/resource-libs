using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class NativeAPI
    {
        public static object CallNativeAPI<T>(ulong hash, params object[] inputParameters)
        {
            var ca = EngineService.GetInstance();
            return ca.CallFunction<T>(hash, inputParameters);
        }
        public static void CallNativeAPI(ulong hash, params object[] inputParameters)
        {
            var ca = EngineService.GetInstance();
            ca.CallFunction(hash, inputParameters);
        }
    }
}
