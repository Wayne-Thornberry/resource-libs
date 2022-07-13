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
    /// An internal event will only trigger for that specific env
    /// </summary>
    public class InternalEvent : AbstractEvent
    {
        public InternalEvent(string eventName) : base(eventName)
        {
        }


#if CLIENT
        public override void Invoke(params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { EventName = EventName, Source = "CLIENT", IsInternal = true, Args = args };
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerEvent(EventDictionaryManager.Key, json);
        }

        internal override void OnEventTriggered(EventTriggered data)
        {
            EventTriggeredData = data;
            if (data.IsInternal && data.Source.Equals("SERVER"))
                throw new Exception("Detected event trigger that is on the CLIENT and is internal but the call is  coming from the SERVER");
            var isCallbackExecution = data.IsCallback;
            var args = data.Args;
            object returnData = null;
            Console.WriteLine("Calling event triggered");
            returnData = OnEventTriggered(args);
        }
#elif SERVER

        public override void Invoke(Player player, params object[] args)
        { 
            var callbackEventRequest = new EventTriggered() { EventName = EventName, Source = "SERVER", IsInternal = true, Args = args };
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerEvent(EventManager.Key, json);
        }

        internal override void OnEventTriggered([FromSource] Player player, EventTriggered data)
        {  
            EventTriggeredData = data;
            if (data.IsInternal && data.Source.Equals("CLIENT")) 
                throw new Exception("Detected event trigger that is on the SERVER and is internal but the call is  coming from the CLIENT");
            var args = data.Args;
            Console.WriteLine("Calling event triggered");
            var returnData = OnEventTriggered(player, args);
        }
#endif
    }
}
