using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;

namespace Proline.Resource.Eventing
{
    public abstract class CallbackEvent : EventSubscriber
    {
        private string _eventName;
        private string _callbackEventName; 
        protected bool IsEventActive { get; set; }

        public CallbackEvent(string eventName) : base(eventName)
        {
            _eventName = eventName;
            _callbackEventName = Guid.NewGuid().ToString();
        }

        private void BeginEventCallback()
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            if (!dictionary.ContainsKey(_eventName) && !IsEventActive)
            {
                Subscribe();
                IsEventActive = true;
            }
        }

        public async Task TriggerEventAsync(params object[] args)
        {
            BeginEventCallback();
            TriggerEvent();
            int ticks = 0;
            while (IsEventActive && ticks < 1000)
            {
                ticks++;
                await BaseScript.Delay(0);
            }
            // This will activate as a last resort regardless if the callback was succesful.
            EndEventCallback();
        }
#if CLIENT
        public void TriggerEvent(params object[] args)
        {
            BeginEventCallback();
            if (IsEventActive)
            {
                var callbackEventRequest = new CallbackEventRequest() { Args = args, CallBackEvent = _callbackEventName };
                var json = JsonConvert.SerializeObject(callbackEventRequest); 
                BaseScript.TriggerServerEvent(_eventName, json); 
            }
        }
#elif SERVER
        public void TriggerEvent(params object[] args)
        {
            BeginEventCallback();
            if (IsEventActive)
            {
                var callbackEventRequest = new EventTriggered() { Args = args, CallBackEvent = _callbackEventName };
                var json = JsonConvert.SerializeObject(callbackEventRequest); 
                BaseScript.TriggerClientEvent(_eventName, json);
            }
        }
        public void TriggerEvent(Player player, params object[] args)
        {
            BeginEventCallback();
            if (IsEventActive)
            {
                var callbackEventRequest = new EventTriggered() { Args = args, CallBackEvent = _callbackEventName };
                var json = JsonConvert.SerializeObject(callbackEventRequest);
                player.TriggerEvent(_eventName, json);
            }
        }
#endif

#if CLIENT
        public override void OnEventTriggered(string jsonData)
        {
            if (IsEventActive)
            {
                base.OnEventTriggered(jsonData);
                //var callbackEventResponse = JsonConvert.DeserializeObject<CallbackEventResponse>(jsonData);
                //OnEventCallback(callbackEventResponse.Result);
            }
            EndEventCallback();
        }

        public override object OnEventTriggered(params object[] args)
        {
           return OnEventCallback(args);
        }

        public abstract object OnEventCallback(params object[] args);
#elif SERVER

        public override void OnEventTriggered(Player player, string jsonData)
        {
            if (IsEventActive)
            {
                base.OnEventTriggered(player, jsonData);
                //var callbackEventResponse = JsonConvert.DeserializeObject<CallbackEventResponse>(jsonData);
                //OnEventCallback(callbackEventResponse.Result);
            }
            EndEventCallback();
        }
        public override object OnEventTriggered(Player player, params object[] args)
        {
           return OnEventCallback(player, args);
        }
        public abstract object OnEventCallback(Player player, params object[] args);

#endif



        private void EndEventCallback()
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            if (dictionary.ContainsKey(_eventName) && IsEventActive)
            {
                Unsubscribe(); 
                IsEventActive = false;
            }
        }
    }
}

