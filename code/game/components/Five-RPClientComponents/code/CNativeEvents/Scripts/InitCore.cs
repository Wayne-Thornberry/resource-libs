using CNativeEvents.Events;
using System.Threading.Tasks;

namespace CNativeEvents.Scripts
{
    public class InitCore
    {

        public async Task Execute()
        {
            GameEventTriggered.SubscribeEvent();
        }
    }
}