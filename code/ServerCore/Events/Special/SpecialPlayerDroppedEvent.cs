using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events.Special
{
    internal class SpecialPlayerDroppedEvent : NativeEvent
    {
        public SpecialPlayerDroppedEvent() : base(PLAYERDROPPEDHANDLER)
        {
        }

        private static SpecialPlayerDroppedEvent _event;
        public const string PLAYERDROPPEDHANDLER = "playerDropped";

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

        public void OnNativeEventCalled([FromSource] Player player, string reason)
        {
            //var instance = DisconnectionQueue.GetInstance();
            //var connecting = new PlayerDisconnection
            //{
            //    Player = player,
            //    Reason = reason
            //};
            ////instance.Enqueue(connecting);
            PlayerDroppedEvent.InvokeEvent(player.Name);
        }
    }
}
