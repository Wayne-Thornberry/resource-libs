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
    /// <summary>
    /// A callback event that triggers from the client/server and comes back when the called enviorment has a result
    /// </summary>
    public abstract class ExtendedEvent : AbstractEvent, IDisposable
    { 
        private bool _hasCallback;
        private bool _awaitingCallback;
        internal string CallbackValue { get; set; }
        protected bool DisableAutoCallback { get; set; }

        public ExtendedEvent(string eventName, bool hasCallback = false) : base(eventName)
        { 
            _hasCallback = hasCallback;
        }

#if CLIENT
        internal override void OnEventTriggered(EventTriggered data)
        { 
            EventTriggeredData = data;
            var isCallbackExecution = data.IsCallback;
            var args = data.Args;
            object returnData = null;
            if (isCallbackExecution && data.CallbackEventName.Equals(CallbackValue))
            {
                Console.WriteLine("Calling event callback");
                OnEventCallback(args);
                _awaitingCallback = false;
                Unsubscribe();
            }
            else
            {
                Console.WriteLine("Calling event triggered");
                returnData = OnEventTriggered(args);
                if(!DisableAutoCallback)
                    InvokeCallback(data.CallbackEventName, returnData);
            }
        }

        private void InvokeCallback(string callbackName, params object[] args)
        {
            if (!string.IsNullOrEmpty(callbackName))
            {
                var callbackEventRequest = new EventTriggered() { EventName = EventName,Args = args, IsCallback = true, CallbackEventName = callbackName };
                var json = JsonConvert.SerializeObject(callbackEventRequest);
                Console.WriteLine("Sending Data... " + json);
                BaseScript.TriggerServerEvent(EventManager.Key, json);
            }
        }

        public override void Invoke(params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { EventName = EventName, Args = args };
            if (_hasCallback)
            {
                callbackEventRequest.HasCallback = true;
                callbackEventRequest.CallbackEventName = string.Format("{0}:Callback:{1}", EventName, Guid.NewGuid().ToString());
                CallbackValue = callbackEventRequest.CallbackEventName;
                _awaitingCallback = true;
                Subscribe();
            }
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            BaseScript.TriggerServerEvent(EventManager.Key, json); 
        }
        protected void ExternalInvokeCallback(params object[] args)
        {
            if(DisableAutoCallback)
                InvokeCallback(EventTriggeredData.CallbackEventName, args);
        }

        protected virtual void OnEventCallback(params object[] args) { }

#elif SERVER
        internal override void OnEventTriggered([FromSource] Player player, EventTriggered data)
        { 
            EventTriggeredData = data;
            var isCallbackExecution = data.IsCallback;
            var args = data.Args; 
            object returnData = null;
            if (isCallbackExecution && data.CallbackEventName.Equals(CallbackValue))
            {
                Console.WriteLine("Calling event callback");
                OnEventCallback(player, args);
                _awaitingCallback = false;
                Unsubscribe();
            }
            else
            {
                Console.WriteLine("Calling event triggered");
                returnData = OnEventTriggered(player, args);
                if (!DisableAutoCallback)
                    InvokeCallback(player, data.CallbackEventName, returnData);
            }
        }

        private void InvokeCallback(Player player, string callbackName, params object[] args)
        {
            if (!string.IsNullOrEmpty(callbackName))
            {
                var eventTriggered = new EventTriggered() { EventName = EventName,Args = args, IsCallback = true, CallbackEventName = callbackName };
                var json = JsonConvert.SerializeObject(eventTriggered);
                Console.WriteLine("Sending Data... " + json);
                player.TriggerEvent(EventManager.Key, json); 
            }
        }

        protected void ExternalInvokeCallback(Player player, params object[] args)
        {
            if(DisableAutoCallback)
                InvokeCallback(player, EventTriggeredData.CallbackEventName, args);
        }

        public override void Invoke(Player player, params object[] args)
        {
            var callbackEventRequest = new EventTriggered() { Args = args };
            if (_hasCallback)
            {
                callbackEventRequest.HasCallback = true;
                callbackEventRequest.CallbackEventName = string.Format("{0}:Callback:{1}", EventName, Guid.NewGuid().ToString());
                _awaitingCallback = true;
                Subscribe();
            }
            var json = JsonConvert.SerializeObject(callbackEventRequest);
            player.TriggerEvent(EventManager.Key, json); 
        }

        protected virtual void OnEventCallback(Player player, params object[] args) { }
#endif
        public async Task<int> WaitForCallback()
        {
            var timeoutTicks = 0;
            while(_awaitingCallback && timeoutTicks <= 1000)
            {
                timeoutTicks++;
                await BaseScript.Delay(0);
            }

            if (!_awaitingCallback)
                return 0; // callback was succesfully run
            return 1;// wait has timed out
        }


        public void Dispose()
        {
            Unsubscribe();
            GC.SuppressFinalize(this);
        }
    }
}

// [Client] Invoke -> [Server] OnEventTriggered -> Do stuff -> May callback -> [Client] OnEventTriggered
