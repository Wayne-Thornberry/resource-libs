using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.ClassicOnline.MConnection.Events;
using Proline.Modularization.Core;

namespace Proline.ClassicOnline.MConnection.Scripts
{
    public partial class EventSetupScript : ModuleScript
    {
        public EventSetupScript()
        {

        }

        public override async Task OnExecute()
        {
            PlayerJoinedEvent.SubscribeEvent();
        }
    }
}