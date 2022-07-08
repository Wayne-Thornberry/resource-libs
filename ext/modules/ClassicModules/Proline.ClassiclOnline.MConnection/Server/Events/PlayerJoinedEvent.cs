using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection
{
    internal partial class PlayerJoinedEvent : LoudEvent
    {
        protected override object OnEventTriggered(Player player, params object[] args)
        {
            return null;
        }
    }
}
