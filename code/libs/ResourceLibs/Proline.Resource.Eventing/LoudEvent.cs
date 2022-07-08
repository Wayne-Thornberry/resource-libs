using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Eventing
{
    /// <summary>
    /// A loud event triggers from client -> server -> all clients. A call from the server, triggers the server first then all clients
    /// </summary>
    public class LoudEvent : AbstractEvent
    {

        public LoudEvent(string eventName) : base(eventName)
        {

        }

#if CLIENT
        public override void Invoke(params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { EventName = EventName, Args = args }; 
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerServerEvent(EventDictionaryManager.Key, json);
        }
        
        internal override void OnEventTriggered(EventTriggered data)
        {
            EventTriggeredData = data; 
            var args = data.Args; 
            Console.WriteLine("Calling event triggered");
            var returnData = OnEventTriggered(args);  
        }
#elif SERVER
        public override void Invoke(Player player, params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { EventName = EventName, Args = args }; 
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerEvent(EventDictionaryManager.Key, json); 
        } 

        internal override void OnEventTriggered([FromSource] Player player, EventTriggered data)
        {
            EventTriggeredData = data; 
            var args = data.Args; 
            Console.WriteLine("Calling event triggered");
            var returnData = OnEventTriggered(player, args);
            BaseScript.TriggerClientEvent(EventDictionaryManager.Key, JsonConvert.SerializeObject(data)); 
        } 
#endif
    }
}
