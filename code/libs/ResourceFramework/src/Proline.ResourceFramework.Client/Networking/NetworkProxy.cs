using CitizenFX.Core;
using System;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public abstract class NetworkProxy : IDisposable
    {
        private const int MAX_TICKS = 1000;
        private object _result;
        protected NetworkListener Listener;
        /// ComponentName/[-1..N]/API
        public string URL { get; set; }
        public string TargetComponentName { get; private set; }
        public int TargetHandle { get; private set; } 

        public NetworkProxy(string url)
        {
            Listener = new NetworkListener();
            Listener.SetCallback(new Action<string>(ListenerCallback));
            URL = url;
            ParseURL(URL);
        }

        private void ParseURL(string url)
        {
            var items  = url.Split('/');
            TargetComponentName = items[0];
            TargetHandle = int.Parse(items[1]);
        }

        public async Task<T> CallAPI<T>(string methodName, params object[] args)
        { 
            OpenEventHandler();
            SendCall(methodName, args); 
            var result = (T)await AwaitResult();
            CloseEventHandler();
            return result;
        }

        protected void ListenerCallback(string str)
        {
            // TODO
            // Convert data into a result obj
            _result = str;
        }

        private void SendCall(string methodName, object[] args)
        { 
            // build a request

            if (TargetHandle > -1)
            {
                //CFXBaseScript.TriggerClientEvent(TargetComponentName);
            }
            if (TargetHandle == -1)
            {
                //CFXBaseScript.TriggerServerEvent(TargetComponentName);
            }
            else
            {
                throw new Exception("Target handle not in range");
            }
        }

        protected void OpenEventHandler()
        {
            Listener.BeginListening();
        }  

        protected void CloseEventHandler()
        {
            Listener.EndListening();
        }

        protected async Task<object> AwaitResult()
        {
            if (!Listener.IsListening)
                throw new Exception("Listener is not listening to events");
            int ticks = 0;
            while (ticks < MAX_TICKS)
            {
                if (_result != null)
                {
                    var result = _result;
                    _result = null;
                    return result;
                }
                ticks++;
                await BaseScript.Delay(0);
            }
            return null;
        }

        public abstract void Dispose();
    }
}