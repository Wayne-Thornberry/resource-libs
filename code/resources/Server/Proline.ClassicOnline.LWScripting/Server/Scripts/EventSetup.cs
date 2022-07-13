using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Server.Scripts
{
    public class EventSetup : ModuleScript
    {

        public override async Task OnExecute()
        {
            PlayerReadyEvent.SubscribeEvent();
        }
    }
}
