using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.DBAccess.Proxies;
using Proline.Modularization.Core;

namespace Proline.ClassicOnline.MConnection
{
    public partial class DisconnectionProcessingScript : ModuleScript
    {
        public DisconnectionProcessingScript() : base(true)
        {

        }

        public override async Task OnExecute()
        {  
            var disconnectionQueue = DisconnectionQueue.GetInstance();
            while(disconnectionQueue.Count > 0)
            { 
                var connection = disconnectionQueue.Dequeue(); 
                PlayerDroppedEvent.InvokeEvent(connection.Player.Name);
            }
        }

    }

   
}
