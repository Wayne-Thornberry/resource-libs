using CitizenFX.Core;
using CNetConnection.Events;
using System.Threading.Tasks;

namespace CNetConnection.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            PlayerJoinedEvent.InvokeEvent(Game.Player.Name);
        }
    }
}
