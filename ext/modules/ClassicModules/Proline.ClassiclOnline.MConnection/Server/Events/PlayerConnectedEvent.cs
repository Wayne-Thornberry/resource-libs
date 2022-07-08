using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection
{
    internal partial class PlayerConnectedEvent : LoudEvent
    {
        public static void InvokeEvent(string username)
        {
            var events = new PlayerConnectedEvent();
            events.Invoke(null, username);
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            return null;
        }
    }
}
