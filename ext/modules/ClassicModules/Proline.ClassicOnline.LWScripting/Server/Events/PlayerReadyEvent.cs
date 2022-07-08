using CitizenFX.Core;
using Proline.ClassicOnline.MScripting.Config;
using Proline.Modularization.Core;
using Proline.Resource.Eventing;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting
{
    internal partial class PlayerReadyEvent : LoudEvent
    {
        private Log _log => new Log();


        protected override object OnEventTriggered(Player player, params object[] args)
        {
            return null;
        }
    }
}
