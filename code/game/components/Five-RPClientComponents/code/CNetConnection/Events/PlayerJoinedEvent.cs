using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;

namespace CNetConnection.Events
{
    internal partial class PlayerJoinedEvent : LoudEvent
    {
        private static PlayerJoinedEvent _event;
        public const string PLAYERJOINEDHANDLER = "PlayerJoinedHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerJoinedEvent();
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

        public PlayerJoinedEvent() : base(PLAYERJOINEDHANDLER)
        {
        }

        public static void InvokeEvent(string username)
        {
            _event.Invoke(username);
        }

        protected override object OnEventTriggered(params object[] args)
        {
            if (args.Length > 0)
            {
                var username = args[0].ToString();
                Console.WriteLine(username);
                if (Game.Player.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase)) return null;
                PlayerReadyEvent.InvokeEvent();
            }
            return null;
        }
    }
}
