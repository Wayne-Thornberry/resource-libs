using CitizenFX.Core;
using Proline.ClassicOnline.MGame;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.LevelScripts
{
    public class Test2
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            //   MGameAPI.SaveCurrentCar();

            var test = new Dictionary<string, object>();
            test.Add("PlayerHealth", Game.PlayerPed.Health);

            MData.MDataAPI.SaveObject(test);
            await BaseScript.Delay(10000);
            var x = await MData.MDataAPI.LoadFileAsync(1);
            MDebug.MDebugAPI.LogDebug(x);
            //CitizenFX.Core.Game.PlayerPed.Kill();

        }
    }
}
