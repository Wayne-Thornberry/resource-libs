using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proline.Resource.Eventing
{
    public class EventManager
    {
        public const string Key = "Resource:EventInvoked";
        private static EventManager _instance;
        private EventHandlerDictionary _eventHandlerDictionary;
        private List<AbstractEvent> _eventSubscribers;
        public PlayerList PlayerList { get; set; }

        public EventManager()
        {
            _eventSubscribers = new List<AbstractEvent>();
        }


        public void AddEvent(AbstractEvent eventType)
        {
            _eventSubscribers.Add(eventType);
            Console.WriteLine(String.Format("{0} subscribed handler", eventType.EventName));
        }
#if CLIENT 
        private void OnResourceEventTriggered(string json)
        {
            var eventTriggered = JsonConvert.DeserializeObject<EventTriggered>(json);
            Console.WriteLine(String.Format("Event {0} called from SERVER passing data {1}", eventTriggered.EventName, json));
            var events = _eventSubscribers.Where(e => e.EventName.Equals(eventTriggered.EventName)).ToArray();
            foreach (var item in events)
            {
                item.OnEventTriggered(eventTriggered);
            } 
        }
#elif SERVER
        private void OnResourceEventTriggered([FromSource] Player player, string json)
        {
            var eventTriggered = JsonConvert.DeserializeObject<EventTriggered>(json); 
            Console.WriteLine(String.Format("Event {0} called from {1} passing data {2}", eventTriggered.EventName, player != null ? player.Name : "SERVER", json));
            var events = _eventSubscribers.Where(e => e.EventName.Equals(eventTriggered.EventName)).ToArray();
            foreach (var item in events)
            {
                item.OnEventTriggered(player, eventTriggered);
            }
            if(eventTriggered.EventType == 0)
                BaseScript.TriggerClientEvent(Key, json);
        }
#endif


        public void SetEventHandlerDictionary(EventHandlerDictionary eventHandler)
        {
            _eventHandlerDictionary = eventHandler;
#if CLIENT
                _eventHandlerDictionary.Add(Key, new Action<string>(OnResourceEventTriggered));
#elif SERVER
            _eventHandlerDictionary.Add(Key, new Action<Player, string>(OnResourceEventTriggered));
#endif 
        }

        internal void RemoveEvent(AbstractEvent abstractEvent)
        {
            _eventSubscribers.Remove(abstractEvent);
            Console.WriteLine("Unsubscribed " + abstractEvent.EventName);
            if (_eventSubscribers.Count == 0)
            { 
                _eventHandlerDictionary.Remove(Key);
            }
        }

        public EventHandlerDictionary GetEventHandlerDictionary()
        {
            return _eventHandlerDictionary;
        }

        public static EventManager GetInstance()
        {
            if (_instance == null)
                _instance = new EventManager();
            return _instance;
        }
    }
}

