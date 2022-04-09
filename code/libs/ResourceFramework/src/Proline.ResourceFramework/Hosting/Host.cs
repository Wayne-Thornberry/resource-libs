using System;

namespace Proline.ResourceFramework
{
    internal class Host : IHost
    {
        public IServiceProvider Services { get; }
        private IResourceHost _resourceHost;

        public Host(IResourceHost resourceHost)
        {
            _resourceHost = resourceHost;
        }

        public void Start()
        {
            //_env.InitializeFeatures();

            //// Run a event socket
            //EventHandlers.Add(_socket.EndPoint, _socket.Action);
            //_socket.SetCallback(new Action<string>(OnEventResponse));
            //Debug.WriteLine("Configruing...");

            //var type = _sourcAssembly.GetTypes().First(e => e.Name.Equals("Startup"));
            //foreach (var item in _sourcAssembly.GetTypes())
            //{
            //    _log.Debug(item.Name);
            //} 

            //_env.InitializeResource();
            _resourceHost.Start();
        }

        private void OnEventResponse(string arg2)
        {
            //_log.Debug("Got Request");
            //var request = JsonConvert.DeserializeObject<EventRequest>(arg2);
            //var response = new EventResponse();
            //_log.Debug(request.CallbackGUID);
            //BaseScript.TriggerServerEvent(request.CallbackGUID, JsonConvert.SerializeObject(response));
        }

    }
}