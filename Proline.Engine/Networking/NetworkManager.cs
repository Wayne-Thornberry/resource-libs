using Proline.Engine.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Networking
{
    public class NetworkManager
    {
        private static NetworkManager _instance;
        public const string NetworkRequestListenerHandle = "networkRequestListener";
        public const string NetworkResponseListenerHandler = "networkResponseListener";

        private Dictionary<string, NetworkRequest> _requests;
        private Dictionary<string, NetworkResponse> _responses;
        private NetworkManager()
        {
            _requests = new Dictionary<string, NetworkRequest>();
            _responses = new Dictionary<string, NetworkResponse>();
        }

        public static NetworkManager GetInstance()
        {
            if (_instance == null)
                _instance = new NetworkManager();
            return _instance;
        }

        public NetworkResponse CreateAndInsertResponse(string guid, object value, bool isException = false)
        {
            NetworkResponse response = CreateResponse(guid, value, isException);
            InsertResponse(guid, response);
            return response;
        }

        private void InsertResponse(string guid, NetworkResponse response)
        {
            if (_responses.ContainsKey(guid)) throw new Exception();
            _responses.Add(guid, response);
        }

        private static NetworkResponse CreateResponse(string guid, object value, bool isException)
        {
            NetworkResponse response;
            var header = new NetworkHeader()
            {
                Guid = guid,
                DateCreated = DateTime.UtcNow,
                DateRecived = DateTime.UtcNow,
                DateSent = DateTime.UtcNow,
            };
            if (isException)
            {
                response = new NetworkResponse()
                {
                    Header = header,
                    Result = null,
                    Exception = new Exception(value.ToString()),
                };
            }
            else
            {
                response = new NetworkResponse()
                {
                    Header = header,
                    Result = new MethodResult()
                    {
                        ResultValue = value,
                    },
                    Exception = null,
                };
            }

            return response;
        }

        public NetworkRequest CreateAndInsertRequest(string guid, string componentName, string methodName, params object[] args)
        {
            NetworkRequest request = CreateRequest(guid, componentName, methodName, args);
            InsertRequest(guid, request);
            return request;
        }

        private void InsertRequest(string guid, NetworkRequest request)
        {
            if (_requests.ContainsKey(guid)) throw new Exception();
            _requests.Add(guid, request);
        }

        private static NetworkRequest CreateRequest(string guid, string componentName, string methodName, object[] args)
        {
            var header = new NetworkHeader()
            {
                Guid = guid,
                DateCreated = DateTime.UtcNow,
            };
            var request = new NetworkRequest()
            {
                Header = header,
                Call = new MethodCall
                {
                    ComponentName = componentName,
                    MethodName = methodName,
                    MethodArgs = args,
                },
                Exception = null,
            };
            return request;
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
