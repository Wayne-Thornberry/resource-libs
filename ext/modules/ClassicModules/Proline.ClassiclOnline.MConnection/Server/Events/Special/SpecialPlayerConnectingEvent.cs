using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection
{

    internal partial class SpecialPlayerConnectingEvent
    {
        public void OnNativeEventCalled([FromSource] Player player, string playerName, object setKickReason, dynamic deferrals)
        {
            deferrals.defer();
            var instance = ConnectionQueue.GetInstance();
            var connecting = new PlayerConnection
            {
                Player = player,
                PlayerName = playerName,
                KickReason = setKickReason,
                Defferal = deferrals
            };
            instance.Enqueue(connecting);
            PlayerConnectingEvent.InvokeEvent(player.Name);
        }
    }
}
