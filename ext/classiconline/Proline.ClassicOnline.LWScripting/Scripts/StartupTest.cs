using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Client.Scripts
{
    public class StartupTest : ModuleScript
    {
        public override async Task OnExecute()
        { 
            //PlayerJoinedEvent.SubscribeEvent();
            PlayerReadyEvent.SubscribeEvent();
           // API.RegisterCommand("Ping", new Action<int, string>(DoX), false);
        }
    }
}
