using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MGame;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.ClassicOnline.MData;
using Proline.ClassicOnline.MDebug;

namespace Proline.LevelScripts.Classic
{
    internal class PlayerLoading
    {
        public async Task Execute(object[] args, CancellationToken token)
        {

            if (!MDataAPI.IsSaveInProgress())
            {
                MDataAPI.LoadFile(16); // Sends a load request to the server
                MDebugAPI.LogDebug("Load Request put in");
                while (!MDataAPI.IsFileLoaded())
                {
                    await BaseScript.Delay(0);
                }
                Game.PlayerPed.Health = MDataAPI.GetFileValue<int>("PlayerHealth");
                Game.PlayerPed.Position = MDataAPI.GetFileValue<Vector3>("PlayerPosition");
            }

        }
    }
}
