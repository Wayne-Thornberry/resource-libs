using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Networking
{
    public class EventClient : BaseScript
    {
        private readonly int TIMEOUT_TICKS = 300000;
        public Log _log = new Log();
        private string _endPoint; 

        internal EventClient()
        { 

        }

        public async Task<EventResponse> SendAsync(string endPoint, object content = null)
        {
            var ticks = 0;
            var eventListener = new EventListener();
            EventResponse response = null;
            // _log.Debug("created evenmt l;istener");
            eventListener.SetCallback(new Action<string>((responseString) =>
            {
                _log.Debug("Got Response Bakc");
                response = JsonConvert.DeserializeObject<EventResponse>(responseString);
            }));
            EventHandlers.Add(eventListener.GUID, eventListener.Action);
            eventListener.BeginListening();

            EventRequest request = new EventRequest();
            request.CallbackGUID = eventListener.GUID;
            request.Content = content;
            var api = NetworkManager.EventMethods;
            var reqString = JsonConvert.SerializeObject(request);
            _log.Debug("herte");

            api.TriggerServerEvent(endPoint, reqString);

            while (ticks <= TIMEOUT_TICKS && response == null)
            {
                ticks++;
                _log.Debug("Waiting...");
                await NetworkManager.TaskMethods.Delay(1);
            }

            if (response == null)
            {
                response = new EventResponse();
                response.ResponseCode = -1;
            }

            eventListener.EndListening();
            EventHandlers.Remove(eventListener.GUID);
            return response;
        }
    }
}
