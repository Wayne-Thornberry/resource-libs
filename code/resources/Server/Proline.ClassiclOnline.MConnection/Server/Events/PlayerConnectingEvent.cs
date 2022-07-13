using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection
{

    internal partial class PlayerConnectingEvent
    {

        internal static void InvokeEvent(string playerName)
        {
            var event2 = new PlayerConnectingEvent();
            event2.Invoke(null, playerName);
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            return null;
        }
    }
}
