using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proline.Resource.Logging;


namespace Proline.Resource.Networking
{
    public class EventClient
    {
        private readonly int TIMEOUT_TICKS = 300000;
        public Log _log = new Log();
        private string _endPoint;

        public EventClient()
        {

        }

        public async Task<EventResponse> SendAsync(string endPoint, object content = null)
        {
            var ticks = 0;
            var eventListener = new EventListener();
            EventResponse response = null;
           // _log.Debug("created evenmt l;istener");
            eventListener.SetCallback(new Action<string>((responseString) => {
               // _log.Debug("Got Response Bakc");
                response =JsonConvert.DeserializeObject<EventResponse>(responseString); 
            }));
           // _api.AddEventListener(eventListener.GUID, eventListener.Action);
            eventListener.BeginListening();

            EventRequest request = new EventRequest();
            request.CallbackGUID = eventListener.GUID;
            request.Content = content;
            var api = NetworkManager.EventMethods;
            var reqString = JsonConvert.SerializeObject(request);
            //_log.Debug("herte");

            api.TriggerServerEvent(endPoint, reqString);

            while (ticks <= TIMEOUT_TICKS && response == null)
            {
                ticks++;
               // _log.Debug("Waiting...");
                await NetworkManager.TaskMethods.Delay(1);
            }

            if (response == null)
            {
                response = new EventResponse();
                response.ResponseCode = -1;
            }

            eventListener.EndListening();
            //_api.RemoveEventListener(eventListener.GUID);
            return response;
        }
    }
}
