

using Newtonsoft.Json;
using Proline.Framework;
using Proline.Engine.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class EngineAccess
    {
        private static int _timeout = 1000;

        internal static async Task<NetworkResponse> ExecuteComponentAPI(SimpleComponent component, string methodName, params object[] args)
        { 
            return await ExecuteServerMethod(component.Name, methodName, args);
        }

        internal static async Task<NetworkResponse> ExecuteServerMethod(string componentName, string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, componentName, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;

            var log = new Log();
            var jsonArgs = JsonConvert.SerializeObject(args);
            var requestParams = new EventRequestParams()
            {
                GUID = guid,
                ComponentName = componentName,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            Debugger.LogDebug(data);
            CitizenAccess.GetInstance().TriggerServerEvent(NetworkManager.NetworkRequestListenerHandle, guid, componentName, methodName, JsonConvert.SerializeObject(args));
            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        internal static async Task<NetworkResponse> ExecuteMethod(string componentName, string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, componentName, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;
             
            var jsonArgs = JsonConvert.SerializeObject(args);
            Debugger.LogDebug(jsonArgs);
            var requestParams =  new EventRequestParams()
            {
                GUID = guid,
                ComponentName = componentName,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            Debugger.LogDebug(data);
            CitizenAccess.GetInstance().TriggerEvent(NetworkManager.NetworkRequestListenerHandle, guid, componentName, methodName, JsonConvert.SerializeObject(args));

            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        private static async Task<NetworkResponse> MakeRequest(string guid, NetworkRequest request)
        {
            var nm = NetworkManager.GetInstance(); 
            Debugger.LogDebug("1");
            NetworkResponse response = null;
            while (request.Ticks < _timeout)
            {
                if (nm.HasResponses())
                {
                    if (nm.HasResponse(guid))
                    {
                        response = nm.GetResponse(guid);
                        break;
                    }
                }
                request.Ticks++;
                await CitizenAccess.GetInstance().Delay(0);
            }

            Debugger.LogDebug("1");

            if (response == null)
            {
                nm.CreateAndInsertResponse(guid, null);
            }


            Debugger.LogDebug("1");
            nm.CompleteRequest(guid);
            return response;
        }

        public static void StartNewScript(string scriptName, params object[] args)
        {
            var em = ExtensionManager.GetInstance();
            var extensions = em.GetExtensions();
            foreach (var extension in extensions)
            {
                extension.OnEngineAPICall("StartNewScript", scriptName, args);
            }
            ScriptFactory.CreateNewScript(scriptName, args);
        }
    }
}
