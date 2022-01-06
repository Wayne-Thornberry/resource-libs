using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Proline.Resource.Component.Networking
{
    public abstract class NetObject 
    {

        private int _timeout = 1000;

        protected NetObject() : base()
        {

        }

        public async Task SendRequest(string methodName, params object[] args)
        {
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request, true);
        }

        public async Task<T> SendRequest<T>(string methodName, params object[] args)
        {
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequest(guid, EngineConfiguration.OwnerHandle, methodName, args);
            NetworkResponse response = await MakeRequest(request, true);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        private async Task<NetworkResponse> MakeRequest(NetworkRequest request, bool serverRequest = false)
        { 
            NetworkResponse response = null;
            request.Header.DateSent = DateTime.UtcNow;
            var data = JsonConvert.SerializeObject(request.Call.MethodArgs);
            TriggerEvent(NetConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, request.Header.Guid, request.Call.MethodName, data);

            INetListener listener = CreateListener();
            await listener.Listen(_timeout);
            nm.CompleteRequest(request.Header.Guid);
            response.Header.DateRecived = DateTime.UtcNow;
            return response;
        }  

        protected void TriggerEvent(string executeEngineMethodHandler, int ownerHandle, string guid, string methodName, string data)
        {
            //_source.TriggerEvent(executeEngineMethodHandler, ownerHandle, guid, methodName, data);
        }

        protected INetListener CreateListener()
        {
            return new NetListener(null);
        }

        //public void RequestResponseHandler(int playerId, string guid, string methodName, string argData)
        //{
        //    var args = JsonConvert.DeserializeObject<object[]>(argData);
        //    var nm = NetworkManager.GetInstance();
        //    if (!methodName.Equals(NetConstraints.CreateAndInsertResponse))
        //    {
        //        nm.CreateAndInsertRequest(guid, playerId, methodName, args);
        //    }
        //    else
        //    {
        //        var value = args[0];
        //        var isException = bool.Parse(args[1].ToString());
        //        nm.CreateAndInsertResponse(guid, value, isException);
        //    }
        //}
    }
}
