using CNetConnection.Events;
using System.Threading.Tasks;


namespace CNetConnection.Scripts
{
    public class InitCore
    {

        public async Task Execute()
        {
            PlayerJoinedEvent.SubscribeEvent();
        }
    }
}