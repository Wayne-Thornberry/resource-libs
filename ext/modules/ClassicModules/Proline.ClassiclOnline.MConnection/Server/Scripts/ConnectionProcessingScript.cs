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
    public partial class ConnectionProcessingScript : ModuleScript
    {

        public ConnectionProcessingScript() : base(true)
        {

        }

        public override async Task OnExecute()
        { 

            var instance = ConnectionQueue.GetInstance();
            while(instance.Count > 0)
            {
                var connection = instance.Dequeue();
                PlayerConnectingEvent.InvokeEvent(connection.Player.Name);
                connection.Defferal.done();
                PlayerConnectedEvent.InvokeEvent(connection.Player.Name);
            } 
        }

    }

   
}
