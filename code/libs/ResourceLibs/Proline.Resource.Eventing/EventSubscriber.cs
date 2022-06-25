using CitizenFX.Core;
using Newtonsoft.Json;
using System;

namespace Proline.Resource.Eventing
{
    public class EventSubscriber
    {
        private string _eventName; 
        private bool _hasCallback;
        private string _callbackEvent;

        public EventSubscriber(string eventName)
        {
            _eventName = eventName;
        }

        public void Subscribe()
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
#if CLIENT
            dictionary.Add(_eventName, new Action<string>(OnEventTriggered));
#elif SERVER
            dictionary.Add(_eventName, new Action<Player,string>(OnEventTriggered));
#endif
        }
#if CLIENT
        public virtual void OnEventTriggered(string json)
        {
            var eventTriggered = JsonConvert.DeserializeObject<EventTriggered>(json);
            _hasCallback = !string.IsNullOrEmpty(eventTriggered.CallBackEvent);
            _callbackEvent = eventTriggered.CallBackEvent;
            object returnData = OnEventTriggered(eventTriggered.Args);
            TriggerCallback(returnData);
        }
        public virtual object OnEventTriggered(params object[] args) { return null; }

        public void TriggerCallback(params object[] args)
        {
            if (_hasCallback)
            {
                BaseScript.TriggerServerEvent(_callbackEvent, args);
            }
        }
#elif SERVER
        public virtual void OnEventTriggered([FromSource] Player player, string json)
        {
            var eventTriggered = JsonConvert.DeserializeObject<EventTriggered>(json);
            _hasCallback = !string.IsNullOrEmpty(eventTriggered.CallBackEvent);
            _callbackEvent = eventTriggered.CallBackEvent;
            object returnData = OnEventTriggered(player, eventTriggered.Args);
            TriggerCallback(player, returnData);
        }
        public virtual object OnEventTriggered(Player player, params object[] args) { return null; } 


        public void TriggerCallback(Player player, params object[] args)
        {
            if (_hasCallback)
            {
                var response = new EventTriggered();
                response.Args = null;
                var json = JsonConvert.SerializeObject(response);
                player.TriggerEvent(_callbackEvent, json);
            }
        }
#endif 

        public void Unsubscribe()
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            dictionary.Remove(_eventName);
        }

    }
}

