using System;
using System.Threading.Tasks;
using Proline.Resource.Component.Networking;
using Proline.Resource.Common.CFX;

namespace Proline.Resource.Component.Networking
{
    public class NetListener : INetListener, IDisposable
    {
        private string _reponseData;
        // this should probably be a queue
        private NetworkResponse _response;
        private int _ticks;
        private IFXTask _tick;

        public int AliveFor { get; set; }
        public bool HasResponse { get; internal set; }

        public NetListener(IFXTask tick)
        {
            _tick = tick;
         //   EventHandlers.Add("guid", new Action<string>(OnResponse));
        }

        private void OnResponse(string obj)
        {
            _reponseData = obj;
            HasResponse = true;
            CreateAndInsertResponse(null,null,false);
        }

        public async Task Listen(long ms)
        {
            while (_ticks < ms && !HasResponse)
            {
                _ticks++;
               await _tick.Wait(0);
            }
            Dispose();
        }

        public NetworkResponse CreateAndInsertResponse(string guid, object value, bool isException = false)
        {
            _response = CreateResponse(guid, value, isException);
            //InsertResponse(guid, response);
            return _response;
        }

        //private void InsertResponse(string guid, NetworkResponse response)
        //{
        //    if (_responses.ContainsKey(guid)) throw new Exception();
        //    _responses.Add(guid, response);
        //}

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

        public void Dispose()
        {
           // UnregisterScript(this);
        }
    }
}
