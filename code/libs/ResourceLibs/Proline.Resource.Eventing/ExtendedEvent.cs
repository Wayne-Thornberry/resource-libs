using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.Resource.Eventing
{
#if CLIENT
    internal class EampleEvent : ExtendedEvent
    {
        public EampleEvent() : base("DOX")
        {
        }

        protected override void OnEventCallback(params object[] args)
        {

        }

        protected override object OnEventTriggered(params object[] args)
        {
            return null;
        }
    }
#elif SERVER 
    internal class EampleEvent : ExtendedEvent
    {
        public EampleEvent() : base("DOX")
        {
        }
        protected override void OnEventCallback(Player player, params object[] args)
        {

        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            return null;
        }
    }
#endif
    public abstract class ExtendedEvent : IDisposable
    {
        private string _eventName;
        private bool _hasCallback;
        private bool _awaitingCallback;
        private bool _altEventName;

        public ExtendedEvent(string eventName, bool hasCallback = false)
        {
            _eventName = eventName;
            _hasCallback = hasCallback;
        }

        public void Subscribe(string eventNameOverride = null)
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            var eventName = string.IsNullOrEmpty(eventNameOverride) ? _eventName : eventNameOverride;
            if (!dictionary.ContainsKey(eventName))
            {
#if CLIENT
            dictionary.Add(eventName, new Action<string>(OnEventTriggered));
#elif SERVER
                dictionary.Add(eventName, new Action<Player, string>(OnEventTriggered));
#endif
                Console.WriteLine("Subscribed handler");
            }
            else
            {
                Console.WriteLine("Cannot subscribe a handler to this event, this event already has a handler, pharaps being used by another instance?");
            }
        }
#if CLIENT
        private void OnEventTriggered(string json)
        {
            Console.WriteLine(String.Format("Event {0} called from SERVER passing data {1}",_eventName, json));
            var eventTriggered = DeserailizeJson(json);
            var isCallbackExecution = eventTriggered.IsCallback;
            var args = eventTriggered.Args;
            object returnData = null;
            if (isCallbackExecution)
            {
                Console.WriteLine("Calling event callback");
                OnEventCallback(args);
                _awaitingCallback = false;
            }
            else
            {
                Console.WriteLine("Calling event triggered");
                returnData = OnEventTriggered(args);
                InvokeCallback(returnData);
            }
        }

        private void InvokeCallback(params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { Args = args, IsCallback = true };
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerServerEvent(_eventName, json);
        }

        public void Invoke(params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { Args = args };
            if (_hasCallback)
            {
                callbackEventRequest.HasCallback = true;
                _awaitingCallback = true; 
                Subscribe();
            }
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerServerEvent(_eventName, json); 
        }

        protected virtual void OnEventCallback(params object[] args) { }
        protected virtual object OnEventTriggered(params object[] args) { return null; }

#elif SERVER
        private void OnEventTriggered([FromSource] Player player, string json)
        {
            Console.WriteLine(String.Format("Event {0} called from {1} passing data {2}",_eventName, player.Name, json));
            var eventTriggered = DeserailizeJson(json);
            var isCallbackExecution = eventTriggered.IsCallback;
            var args = eventTriggered.Args;
            Console.WriteLine(json);
            object returnData = null;
            if (isCallbackExecution)
            {
                Console.WriteLine("Calling event callback");
                OnEventCallback(player, args);
                _awaitingCallback = false;
            }
            else
            {
                Console.WriteLine("Calling event triggered");
                returnData = OnEventTriggered(player, args);
                InvokeCallback(player, returnData);
            }
        }

        private void InvokeCallback(Player player, params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { Args = args, IsCallback = true };
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            player.TriggerEvent(_eventName, json);
        }

        public void Invoke(Player player, params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { Args = args };
            if (_hasCallback)
            {
                callbackEventRequest.HasCallback = true;
                Subscribe();
                _awaitingCallback = true;
            }
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            player.TriggerEvent(_eventName, json); 
        }

        protected virtual void OnEventCallback(Player player, params object[] args) { }
        protected virtual object OnEventTriggered(Player player, params object[] args) { return null; }  
#endif

        public async Task<int> WaitForCallback()
        {
            var timeoutTicks = 0;
            while(_awaitingCallback && timeoutTicks <= 10000)
            {
                timeoutTicks++;
                await BaseScript.Delay(0);
            }

            if (!_awaitingCallback)
                return 0; // callback was succesfully run
            return 1;// wait has timed out
        }

        private EventTriggered DeserailizeJson(string json)
        {
            return JsonConvert.DeserializeObject<EventTriggered>(json);
        }

        public void Unsubscribe()
        {
            var instance = EventDictionaryManager.GetInstance();
            var dictionary = instance.GetEventHandlerDictionary();
            dictionary.Remove(_eventName);
        } 
        public void Dispose()
        {
            Unsubscribe();
            GC.SuppressFinalize(this);
        }
    }
}

// [Client] Invoke -> [Server] OnEventTriggered -> Do stuff -> May callback -> [Client] OnEventTriggered
