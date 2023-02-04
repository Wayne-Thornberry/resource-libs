using CitizenFX.Core;
using Proline.Resource.Eventing;
using ProlineCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events.Special
{

    internal partial class SpecialPlayerConnectingEvent : NativeEvent
    {
        public SpecialPlayerConnectingEvent() : base(PLAYERCONNECTINGHANDLER)
        {
        }

        private static SpecialPlayerConnectingEvent _event;
        public const string PLAYERCONNECTINGHANDLER = "playerConnecting";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new SpecialPlayerConnectingEvent();
                _event.Subscribe(new Action<Player, string, object, dynamic>(_event.OnNativeEventCalled));
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

        public void OnNativeEventCalled([FromSource] Player player, string playerName, object setKickReason, dynamic deferrals)
        {
            PlayerConnectingEvent.InvokeEvent(player.Name);
            //deferrals.defer();
            //var instance = ConnectionQueue.GetInstance();
            //var connecting = new PlayerConnection
            //{
            //    Player = player,
            //    PlayerName = playerName,
            //    KickReason = setKickReason,
            //    Defferal = deferrals
            //};
            //instance.Enqueue(connecting);
            PlayerConnectedEvent.InvokeEvent(player.Name);
        }
    }
}
