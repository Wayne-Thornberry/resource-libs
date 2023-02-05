using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events
{

    internal class PlayerConnectingEvent : LoudEvent
    {
        public PlayerConnectingEvent() : base(PLAYERCONNECTINGHANDLER)
        {
        }

        private static PlayerConnectingEvent _event;
        public const string PLAYERCONNECTINGHANDLER = "PlayerConnectingHandler";

        internal static void InvokeEvent(string playerName)
        {
            var event2 = new PlayerConnectingEvent();
            event2.Invoke(null, playerName);
        }
    }
}
