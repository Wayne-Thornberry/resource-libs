using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Eventing
{
    public abstract class NativeEvent
    {
        internal string EventName { get; }
        public bool IsSubscribed { get; private set; }

        protected NativeEvent(string eventName)
        {
            EventName = eventName;
        }

        public void Subscribe(Delegate action, string eventNameOverride = null)
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            var eventName = string.IsNullOrEmpty(eventNameOverride) ? EventName : eventNameOverride;
            if (!dictionary.ContainsKey(eventName))
            {
                dictionary.Add(eventName, action);
                IsSubscribed = true;
                Console.WriteLine(String.Format("{0} subscribed handler", eventName));
            }
            else
            {
                Console.WriteLine(String.Format("Cannot subscribe a handler to this event {0}, this event already has a handler, pharaps being used by another instance?", eventName)); 
            }
        }

        public void Unsubscribe(string eventNameOverride = null)
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            var eventName = string.IsNullOrEmpty(eventNameOverride) ? EventName : eventNameOverride;
            Console.WriteLine("Unsubscribed " + eventName); 
            IsSubscribed = false;
            dictionary.Remove(eventName);
        }
    }
}
