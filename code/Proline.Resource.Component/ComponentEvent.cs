using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.EventQueue
{
    public static class ComponentEvent
    {
        private static List<Tuple<string, Type>> _events;
        private static Queue<ComponentInvokeEventCall> _eventCallingQueue;

        public static void InvokeEvent(string eventName, params object[] args)
        {
            var eve = _events.Where(e => e.Item1.Equals(eventName, StringComparison.OrdinalIgnoreCase));
            if (_eventCallingQueue == null)
                _eventCallingQueue = new Queue<ComponentInvokeEventCall>();
            if (args == null)
            {
                args = new object[0];
            }
            _eventCallingQueue.Enqueue(new ComponentInvokeEventCall(eventName, args));
        }

        internal static IEnumerable<Tuple<string, Type>> GetRegisteredEvents()
        {
            if (_events == null)
                return null;
            return _events;
        }

        internal static bool HasQueueGotItems()
        {
            if (_eventCallingQueue == null)
                return false;
            return _eventCallingQueue.Count > 0;
        }


        internal static IEnumerable<Tuple<string, Type>> GetRegisteredEvents(string eventName)
        {
            if (_events == null)
                return null;
            return _events.Where(e=>e.Item1.Equals(eventName, StringComparison.CurrentCultureIgnoreCase));
        }

        internal static ComponentInvokeEventCall GetNextQueuedEventCall()
        {
            if (_eventCallingQueue == null)
                return null;
            if (_eventCallingQueue.Count == 0) 
                return null;
            return _eventCallingQueue.Dequeue();
        }

        public static void RegisterEvent(Type callbackObj)
        {
            var eventName = callbackObj.Name;
            if (_events == null)
                _events = new List<Tuple<string, Type>>(); 
            _events.Add(new Tuple<string, Type>(eventName, callbackObj));

        }
    }
}
