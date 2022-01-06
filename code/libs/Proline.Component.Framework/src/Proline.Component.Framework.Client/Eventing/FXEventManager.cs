using CitizenFX.Core;
using Proline.Resource.Component.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Eventing
{
    public static class FXEventManager
    {
        private static Dictionary<string, FXEvent> _fxEvents;
        private static EventHandlerDictionary _handlerCollection; 

        internal static EventHandlerDictionary GetHandlerCollection()
        {
            return _handlerCollection;
        }

        public static void SetHandlerCollection(EventHandlerDictionary handlerCollection)
        {
            _handlerCollection = handlerCollection;
            _fxEvents = new Dictionary<string, FXEvent>();
        }

        public static FXEvent CreateNewEvent(string eventName)
        {
            if (_handlerCollection.ContainsKey(eventName))
                return _fxEvents[eventName];
            var evnt = new FXEvent(eventName);
            var callbacks = evnt.GetCallbacks();
            foreach (var item in callbacks)
            { 
                _handlerCollection.Add(eventName, item);
            }
            return evnt;
        }
    }
}
