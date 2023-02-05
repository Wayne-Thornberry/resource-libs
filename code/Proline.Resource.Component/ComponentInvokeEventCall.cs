using System;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.EventQueue
{
    internal class ComponentInvokeEventCall
    {
        private string _eventName;
        private object[] _args;

        public ComponentInvokeEventCall(string eventName, object[] args)
        {
            this._eventName = eventName;
            this._args = args;
        }

        internal void Invoke()
        {
            var eve = ComponentEvent.GetRegisteredEvents(_eventName);
            foreach (var item in eve)
            {
                try
                {
                    var type = item.Item2;
                    var eventInstance = Activator.CreateInstance(type);
                    if(eventInstance == null)
                        throw new Exception("Event invokation failed, event instance failed to create " + _eventName); 
                    var method = type.GetMethod("OnEventInvoked");
                    if ((object)method == null)
                        throw new Exception("Event invokation failed, method not found for " + type.FullName);
                    method.Invoke(eventInstance, new object[1] { _args });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}