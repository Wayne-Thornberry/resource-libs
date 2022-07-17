using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.ClassicOnline.MConnection.Events;


namespace Proline.ClassicOnline.MConnection.Scripts
{
    public partial class InitCore 
    {

        public async Task Execute()
        {
            PlayerJoinedEvent.SubscribeEvent();
            PlayerJoinedEvent.InvokeEvent(Game.Player.Name);
        }
    }
}