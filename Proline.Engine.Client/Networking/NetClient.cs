


using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Proline.Engine.AAPI;
using Proline.Engine.Componentry;
using Proline.Engine.Data;
using Proline.Engine.Internals;

namespace Proline.Engine.Networking
{
    internal class NetClient : EngineObject
    { 
        private int _timeout = 1000;
        private NetworkManager nm;

        public NetClient() : base("NetClient")
        { 
            nm = NetworkManager.GetInstance();
        }
        private object ExecuteEngineMethod(string methodName, params object[] args)
        {
            //LogDebug("Called Engine event " + methodName);
            switch (methodName)
            {
                case EngineConstraints.Log:
                    var type = long.Parse(args[0].ToString());
                    var data = args[1].ToString();
                    switch (type)
                    {
                        case 0: LogDebug(data); break;
                        case 1: LogWarn(data); break;
                        case 2: LogError(data); break;
                    }
                    return null;
                case EngineConstraints.HealthCheck:
                    return EngineStatus.IsEngineInitialized ? 1 : 0;
                case EngineConstraints.ExecuteAPI:
                    var apiName = int.Parse(args[0].ToString());
                    var list = JsonConvert.DeserializeObject<object[]>(args[1].ToString());
                    return APIMethod.CallAPI(apiName, list.ToArray());
                case EngineConstraints.PullHandler:
                    var cn = args[0].ToString();
                    var c = im.GetComponent(cn);
                    return c.PullData();
                case EngineConstraints.PushHandler:
                    var compeonentName = args[0].ToString();
                    var data2 = args[1].ToString();
                    var component = im.GetComponent(compeonentName);
                    component.PushData(data2);
                    break;
                default:
                    return null;
            }
            return null;
        }

        internal async Task ExecuteEngineMethodServer(string methodName, params object[] args)
        { 
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request, true);
        }

        internal async Task<T> ExecuteEngineMethodServer<T>(string methodName, params object[] args)
        {
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request, true);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        internal async Task<T> ExecuteEngineMethod<T>(string methodName, params object[] args)
        { 
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        private async Task<NetworkResponse> MakeRequest(NetworkRequest request, bool serverRequest = false)
        { 

            NetworkResponse response = null;
            request.Header.DateSent = DateTime.UtcNow;
            var data = JsonConvert.SerializeObject(request.Call.MethodArgs);

            if (serverRequest)
            {
                BaseScript.TriggerServerEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, request.Header.Guid, request.Call.MethodName, data);

            }

            else
            {
                BaseScript.TriggerEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, request.Header.Guid, request.Call.MethodName, data);
            }

            while (request.Ticks < _timeout)
            {
                //var queue = _nm.GetEnqueuedRequests();
                //while (queue.Count > 0)
                //{
                //    var request = queue.Dequeue();
                //    var guid = request.Header.Guid;
                //    object result = null;
                //    bool isException = false;
                //    try
                //    {
                //        result = ExecuteEngineMethod(request.Call.MethodName, request.Call.MethodArgs);
                //        if (result != null)
                //        {
                //            if (!result.GetType().IsPrimitive)
                //            {
                //                result = JsonConvert.SerializeObject(result);
                //            }
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        isException = true;
                //        throw;
                //    }
                //    finally
                //    {
                //        /// THIS NEEDS TO BE HERE, OTHERWISE IT WILL CAUSE A CIRCLAR DEPENENCY OF THE CLIENT CALLING THE SERVER AND THE SERVER CALLING THE CLIENT
                //        if (request.Header.PlayerId != -1)
                //        {
                //            var response = new object[] { result, isException };
                //            var data = JsonConvert.SerializeObject(response);
                //            // LogDebug(guid + methodName + argData + data + playerId);
                //            //if (!EngineConfiguration.IsClient)
                //            //{ 
                //            //    Server.CitizenFX.Core.BaseScript.TriggerClientEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, guid, EngineConstraints.CreateAndInsertResponse, data);
                //            //}
                //            //else if (EngineConfiguration.IsClient)
                //            //    Server.CitizenFX.Core.BaseScript.TriggerEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, guid, EngineConstraints.CreateAndInsertResponse, data);
                //        }
                //    }
                //}


                if (nm.HasResponses())
                {
                    if (nm.HasResponse(request.Header.Guid))
                    {
                        response = nm.GetResponse(request.Header.Guid);
                        break;
                    }
                }
                request.Ticks++;
                await BaseScript.Delay(0);
            }
            if (response == null)
            {
                nm.CreateAndInsertResponse(request.Header.Guid, null);
            }
            nm.CompleteRequest(request.Header.Guid);
            response.Header.DateRecived = DateTime.UtcNow;
            return response;
        }

        public void RequestResponseHandler(int playerId, string guid, string methodName, string argData)
        {
            var args = JsonConvert.DeserializeObject<object[]>(argData);
            var nm = NetworkManager.GetInstance();
            if (!methodName.Equals(EngineConstraints.CreateAndInsertResponse))
            {
                nm.CreateAndInsertRequest(guid, playerId, methodName, args);
            }
            else
            {
                var value = args[0];
                var isException = bool.Parse(args[1].ToString());
                nm.CreateAndInsertResponse(guid, value, isException);
            }
        }
    }
}
