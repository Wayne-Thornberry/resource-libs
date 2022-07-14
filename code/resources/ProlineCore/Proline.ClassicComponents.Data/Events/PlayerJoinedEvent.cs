using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Events
{
    internal partial class PlayerJoinedEvent : LoudEvent
    {
        public PlayerJoinedEvent() : base(PLAYERJOINEDHANDLER)
        {
        }

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
        protected override object OnEventTriggered(params object[] args)
        {
            return null;
        }
    }
}
