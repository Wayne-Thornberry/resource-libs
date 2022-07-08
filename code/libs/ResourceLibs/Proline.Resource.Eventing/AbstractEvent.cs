using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Eventing
{
    public abstract class AbstractEvent
    {
        internal string EventName { get; }
        protected EventTriggered EventTriggeredData { get; set; }
        protected PlayerList Players { get; set; }
        public bool IsSubscribed { get; private set; }

        protected AbstractEvent(string eventName)
        {
            EventName = eventName;
        }

#if CLIENT
        public abstract void Invoke(params object[] args);
        internal abstract void OnEventTriggered(EventTriggered data);
        protected virtual object OnEventTriggered(params object[] args) { return null; }  
#elif SERVER
        public abstract void Invoke(Player player, params object[] args);
        internal abstract void OnEventTriggered([FromSource] Player player, EventTriggered data);
        protected virtual object OnEventTriggered(Player player, params object[] args) { return null; }
#endif

        protected EventTriggered DeserailizeJson(string json)
        {
            return JsonConvert.DeserializeObject<EventTriggered>(json);
        } 


        public void Subscribe()
        {
            var instance = EventDictionaryManager.GetInstance();  
            Players = instance.PlayerList;
            instance.AddEvent(this);
            IsSubscribed = true;
        }

        public void Unsubscribe()
        {
            var instance = EventDictionaryManager.GetInstance();  
            Players = null;
            instance.RemoveEvent(this);
            IsSubscribed = false;
        }
    }
}
