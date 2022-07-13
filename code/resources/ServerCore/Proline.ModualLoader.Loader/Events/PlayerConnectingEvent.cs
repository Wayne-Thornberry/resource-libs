using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events
{

    internal partial class PlayerConnectingEvent : LoudEvent
    {
        public PlayerConnectingEvent() : base(PLAYERCONNECTINGHANDLER)
        {
        }

        private static PlayerConnectingEvent _event;
        public const string PLAYERCONNECTINGHANDLER = "PlayerConnectingHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerConnectingEvent();
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

        internal static void InvokeEvent(string playerName)
        {
            var event2 = new PlayerConnectingEvent();
            event2.Invoke(null, playerName);
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            return null;
        }
    }
}
