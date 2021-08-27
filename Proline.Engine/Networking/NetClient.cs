extern alias Server;
extern alias Client;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Networking
{
    internal class NetClient : EngineObject
    { 
        private int _timeout = 1000;

        public NetClient() : base("NetClient")
        {
        }

        internal async Task ExecuteEngineMethodServer(string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request, true);
        }

        internal async Task<T> ExecuteEngineMethodServer<T>(string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request, true);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        internal async Task<T> ExecuteEngineMethod<T>(string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        private async Task<NetworkResponse> MakeRequest(NetworkRequest request, bool serverRequest = false)
        {
            var nm = NetworkManager.GetInstance();
            var dm = EngineService.GetInstance();


            NetworkResponse response = null;
            request.Header.DateSent = DateTime.UtcNow;
            var data = JsonConvert.SerializeObject(request.Call.MethodArgs);

            if (serverRequest)
            {
                Client.CitizenFX.Core.BaseScript.TriggerServerEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, request.Header.Guid, request.Call.MethodName, data);

            }

            else
            {
                Client.CitizenFX.Core.BaseScript.TriggerEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, request.Header.Guid, request.Call.MethodName, data);
            }

            while (request.Ticks < _timeout)
            {
                if (nm.HasResponses())
                {
                    if (nm.HasResponse(request.Header.Guid))
                    {
                        response = nm.GetResponse(request.Header.Guid);
                        break;
                    }
                }
                request.Ticks++;
                await Delay(0);
            }
            if (response == null)
            {
                nm.CreateAndInsertResponse(request.Header.Guid, null);
            }
            nm.CompleteRequest(request.Header.Guid);
            response.Header.DateRecived = DateTime.UtcNow;
            return response;
        }
    }
}
