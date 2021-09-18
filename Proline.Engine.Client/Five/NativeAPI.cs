


using CitizenFX.Core.Native;

namespace Proline.Engine.Five
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
