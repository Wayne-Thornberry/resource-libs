using System;
using System.Collections.Generic;

namespace Proline.Resource.Component.Networking
{
    public class NetworkManager
    {
        private static NetworkManager _instance;

        private Dictionary<string, NetworkRequest> _requests;
        private Dictionary<string, NetworkResponse> _responses;
        private Queue<NetworkRequest> _requestQueue;
        internal NetworkManager()
        {
            _requests = new Dictionary<string, NetworkRequest>();
            _responses = new Dictionary<string, NetworkResponse>();
            _requestQueue = new Queue<NetworkRequest>();
        }

        public static NetworkManager GetInstance()
        {
            if (_instance == null)
                _instance = new NetworkManager();
            return _instance;
        }

       

        public NetworkRequest CreateAndInsertRequest(string guid, int playerId, string methodName, params object[] args)
        {
            NetworkRequest request = CreateRequest(guid, playerId, methodName, args);
            InsertRequest(guid, request);
            return request;
        }

        private void InsertRequest(string guid, NetworkRequest request)
        {
            if (_requests.ContainsKey(guid)) throw new Exception();
            _requests.Add(guid, request);
        }

        internal NetworkRequest GetRequest(string guid)
        {
            return _requests[guid];
        }

        private static NetworkRequest CreateRequest(string guid, int playerId, string methodName, object[] args)
        {
            var header = new NetworkHeader()
            {
                Guid = guid,
                DateCreated = DateTime.UtcNow,
                PlayerId = playerId,
            };
            var request = new NetworkRequest()
            {
                Header = header,
                Call = new MethodCall
                {
                    MethodName = methodName,
                    MethodArgs = args,
                },
                Exception = null,
            };
            return request;
        }

        internal Queue<NetworkRequest> GetEnqueuedRequests()
        {
            return _requestQueue;
        }

        public bool HasResponses()
        {
            return _responses.Count > 0;
        }

        public NetworkResponse GetResponse(string guid)
        {
            return _responses[guid];
        }

        public bool HasResponse(string guid)
        {
            return _responses.ContainsKey(guid);
        }

        public void CompleteRequest(string guid)
        {
            if (_requests.ContainsKey(guid))
                _requests.Remove(guid);

            if (_responses.ContainsKey(guid))
                _responses.Remove(guid);
        }
    }
}
