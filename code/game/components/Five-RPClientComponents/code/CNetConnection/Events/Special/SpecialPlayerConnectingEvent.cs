using Proline.Resource.Eventing;

namespace CNetConnection.Events.Special
{
    internal partial class SpecialPlayerConnectingEvent : NativeEvent
    {
        public SpecialPlayerConnectingEvent() : base(PLAYERCONNECTINGHANDLER)
        {
        }

        private static SpecialPlayerConnectingEvent _event;
        public const string PLAYERCONNECTINGHANDLER = "playerConnecting";

#if SERVER
        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new SpecialPlayerConnectingEvent();
                _event.Subscribe(new Action<Player, string, object, dynamic>(_event.OnNativeEventCalled));
            }
        }

        public static void UnsubscribeEvent()
        {
            if (_event != null)
            {
                _event.Unsubscribe();
                _event = null;
            }
        }
#endif
    }
}
