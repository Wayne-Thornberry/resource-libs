using CitizenFX.Core;
using CPoolObjects.Tasks;
using System.Threading.Tasks;

namespace CPoolObjects.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            var gc = new PoolObjectTracker();
            while (true)
            {
                var task = Task.Factory.StartNew(gc.Execute);
                await BaseScript.Delay(500);
            }
        }
    }
}
