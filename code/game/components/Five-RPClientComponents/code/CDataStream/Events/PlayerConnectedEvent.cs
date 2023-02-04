using Proline.Resource.Eventing;

namespace CDataStream.Events
{
    internal partial class PlayerConnectedEvent : LoudEvent
    {
        public PlayerConnectedEvent() : base(PLAYERCONNECTEDHANDLER)
        {
        }

        private static PlayerConnectedEvent _event;
        public const string PLAYERCONNECTEDHANDLER = "PlayerConnectedHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerConnectedEvent();
                _event.Subscribe();
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
    }
}
