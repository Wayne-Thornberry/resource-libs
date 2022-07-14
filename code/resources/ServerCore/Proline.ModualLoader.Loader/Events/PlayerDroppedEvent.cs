using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events
{

    internal class PlayerDroppedEvent : LoudEvent
    {
        public PlayerDroppedEvent() : base(PLAYERDROPPEDHANDLER)
        {
        }

        private static PlayerDroppedEvent _event;
        public const string PLAYERDROPPEDHANDLER = "PlayerDroppedHandler";

        internal static void InvokeEvent(string playerName)
        {
            var event2 = new PlayerConnectingEvent();
            event2.Invoke(null, playerName);
        }

    }
}
