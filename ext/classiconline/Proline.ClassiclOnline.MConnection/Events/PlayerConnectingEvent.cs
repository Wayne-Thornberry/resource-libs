using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection.Events
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
    }
}
