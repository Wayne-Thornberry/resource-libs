using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection.Events
{
    internal partial class PlayerDroppedEvent : LoudEvent
    {
        public PlayerDroppedEvent() : base(PLAYERDROPPEDHANDLER)
        {
        }

        private static PlayerDroppedEvent _event;
        public const string PLAYERDROPPEDHANDLER = "PlayerDroppedHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerDroppedEvent();
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
