using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection.Events.Special
{
    internal partial class SpecialPlayerDroppedEvent : NativeEvent
    {
        public SpecialPlayerDroppedEvent() : base(PLAYERDROPPEDHANDLER)
        {
        }

        private static SpecialPlayerDroppedEvent _event;
        public const string PLAYERDROPPEDHANDLER = "playerDropped";

#if SERVER
        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new SpecialPlayerDroppedEvent();
                _event.Subscribe(new Action<Player, string>(_event.OnNativeEventCalled));
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
