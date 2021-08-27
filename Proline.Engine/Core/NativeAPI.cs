extern alias Server;
extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class NativeAPI
    {
        public static T CallNativeAPI<T>(Hash hash, params InputArgument[] inputParameters)
        {
            
            return Function.Call<T>(hash, inputParameters);
        }
        public static void CallNativeAPI(Hash hash, params InputArgument[] inputParameters)
        {
             Function.Call(hash, inputParameters);
        }

    }
}
