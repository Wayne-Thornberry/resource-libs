using CitizenFX.Core;
using Proline.Resource.Client.Eventing;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Component.Events
{

    public class EventManager
    { 
        private static EventHandlerDictionary _internalHandlerCollection;
        private static Dictionary<string, CEventHandler> _eventHandlerDictionary;

        internal static void SetHandlerCollection(EventHandlerDictionary handlers)
        {
            if (_eventHandlerDictionary == null)
                _eventHandlerDictionary = new Dictionary<string, CEventHandler>();
            _internalHandlerCollection = handlers;
        }

        public static void InvokeEvent(string eventName, params object[] args)
        {
            var log = Logger.GetInstance().GetLog();
            //log.Debug(eventName + " Invoke");
            BaseScript.TriggerEvent(eventName, new List<object>(args));
        }

        public static void InvokeEventV2(string eventName, params object[] args)
        {
            BaseScript.TriggerEvent(eventName, args);
        }

        public static void AddEventListener(string eventName, Delegate delegat)
        {
            if (_eventHandlerDictionary == null)
                _eventHandlerDictionary = new Dictionary<string, CEventHandler>();

            if (_eventHandlerDictionary.ContainsKey(eventName))
                _eventHandlerDictionary[eventName].AddCallBack(delegat);
            else
            {
                var eh = new CEventHandler(eventName);
                eh.AddCallBack(delegat);
                _eventHandlerDictionary.Add(eventName, eh);
                _internalHandlerCollection.Add(eventName, eh.Callback);
            }
        }

        public static void AddEventListenerV2(string eventName, Delegate delegat)
        {
            _internalHandlerCollection.Add(eventName, delegat);
        }

        public static void RemoveEventListener(string eventName)
        {
            if (_eventHandlerDictionary == null)
                _eventHandlerDictionary = new Dictionary<string, CEventHandler>();

            if (_eventHandlerDictionary.ContainsKey(eventName))  
            {
                var eh = _eventHandlerDictionary[eventName];
                eh.RemoveCallbacks();
                _eventHandlerDictionary.Remove(eventName);
                _internalHandlerCollection.Remove(eventName);
            }
        } 
 
    }

}
