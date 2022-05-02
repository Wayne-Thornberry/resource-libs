using Proline.ClassicOnline.MGame;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.LevelScripts
{
    public class Test2
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            PCAPIs.SaveCurrentCar();
            //CitizenFX.Core.Game.PlayerPed.Kill();

        }
    }
}
