using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection
{
    internal partial class PlayerDroppedEvent
    {
        public static void InvokeEvent(string username)
        {
            var events = new PlayerDroppedEvent();
            events.Invoke(null, username);
        }

    }
}
