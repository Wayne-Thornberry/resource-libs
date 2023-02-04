using Proline.Resource.Eventing;
using System;

namespace Proline.ResourceLibs.ServerEventCaller
{
    public class EventCaller : ExtendedEvent
    {
        private Action<object[]> _callback;

        public EventCaller(string eventName) : base(eventName, true)
        {
        }

        public void OnEventCallback(Action<object[]> action)
        {
            _callback = action;
        }

        protected override void OnEventCallback(params object[] args)
        {
            _callback.Invoke(args);
            return;
        }
    }
}