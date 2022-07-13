using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection
{
    internal partial class SpecialPlayerDroppedEvent
    {

        public void OnNativeEventCalled([FromSource] Player player, string reason)
        { 
            var instance = DisconnectionQueue.GetInstance();
            var connecting = new PlayerDisconnection
            { 
                Player = player, 
                Reason = reason
            };
            instance.Enqueue(connecting);
            PlayerConnectingEvent.InvokeEvent(player.Name);
        }
    }
}
