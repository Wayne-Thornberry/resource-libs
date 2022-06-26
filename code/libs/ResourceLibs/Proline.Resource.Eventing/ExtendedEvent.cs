using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Eventing
{
    internal class EampleEvent : ExtendedEvent
    {
        public EampleEvent() : base("DOX")
        {
        }

        protected override object OnEventTriggered(bool isCallbackExecution, params object[] args)
        {
            if (isCallbackExecution)
            { 
#if CLIENT
                // Needs to support callback and calling code
#elif SERVER
            // Needs to support callback and calling code 
#endif
            }
            else
            { 
#if CLIENT
                // Needs to support callback and calling code
#elif SERVER
            // Needs to support callback and calling code 
#endif
            }
            return null;
        }
    }

    public abstract class ExtendedEvent : IDisposable
    {
        private string _eventName;
        private bool _hasCallback;

        public ExtendedEvent(string eventName)
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
        private void OnEventTriggered(string json)
        {
            object returnData = CallEventTriggered(json);
            if(returnData != null)
            {
                //_hasCallback = !string.IsNullOrEmpty(eventTriggered.CallBackEvent);
                //_callbackEvent = eventTriggered.CallBackEvent;
                // callback space
                // invoke(returnData);
            }
        }
#elif SERVER
        private void OnEventTriggered(Player player, string json)
        {
            object returnData = CallEventTriggered(json);
            if (returnData != null)
            {
                //_hasCallback = !string.IsNullOrEmpty(eventTriggered.CallBackEvent);
                //_callbackEvent = eventTriggered.CallBackEvent;
                // callback space
                // invoke(player, returnData);
            }
        }
#endif

        private object CallEventTriggered(string json)
        {
            var eventTriggered = JsonConvert.DeserializeObject<EventTriggered>(json);
            var isCallbackExecution = !_hasCallback;
            var args = eventTriggered.Args;
            object returnData = OnEventTriggered(isCallbackExecution, args);
            return returnData;
        }
        protected virtual object OnEventTriggered(bool isCallbackExecution, params object[] args) { return null; }

        public void Unsubscribe()
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            dictionary.Remove(_eventName);
        }
#if CLIENT
        public void Invoke(params object[] args)
        {
            if(_hasCallback)
            {
                // callback space
            }
            var callbackEventRequest = new EventTriggered() { Args = args };
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerServerEvent(_eventName, json);
        }

#elif SERVER
        public void Invoke(Player player, params object[] args)
        {
            if(_hasCallback)
            {
                // prepping callback space
            }
            var callbackEventRequest = new EventTriggered() { Args = args };
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            player.TriggerEvent(_eventName, json);
        }
#endif
        public void Dispose()
        {
            Unsubscribe();
            GC.SuppressFinalize(this);
        }
    }
}

// [Client] Invoke -> [Server] OnEventTriggered -> Do stuff -> May callback -> [Client] OnEventTriggered
