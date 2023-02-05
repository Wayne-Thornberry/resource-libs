using Proline.Resource.Eventing;
using System;

namespace Proline.Resource.Eventing
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

#if CLIENT
        protected override void OnEventCallback(params object[] args)
        {
            _callback.Invoke(args);
            return;
        }
#endif
    }
}