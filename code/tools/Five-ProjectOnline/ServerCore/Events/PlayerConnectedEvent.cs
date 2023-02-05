using CitizenFX.Core;
using Proline.Resource.Eventing;
using ProlineCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events
{
    internal class PlayerConnectedEvent : LoudEvent
    {
        public PlayerConnectedEvent() : base(PLAYERCONNECTEDHANDLER)
        {
        }

        private static PlayerConnectedEvent _event;
        public const string PLAYERCONNECTEDHANDLER = "PlayerConnectedHandler";

        public static void InvokeEvent(string username)
        {
            var events = new PlayerConnectedEvent();
            events.Invoke(null, username);
        }

    }
}
