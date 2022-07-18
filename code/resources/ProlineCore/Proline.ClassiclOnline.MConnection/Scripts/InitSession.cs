using CitizenFX.Core;
using Proline.ClassicOnline.MConnection.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        { 
            PlayerJoinedEvent.InvokeEvent(Game.Player.Name);
        }
    }
}
