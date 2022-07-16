using CitizenFX.Core;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.ClassicOnline.MGame;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.CFXExtended.Core;
using CitizenFX.Core.Native;


namespace Proline.ClassicOnline.SClassic
{
    public class SaveNow
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            Screen.LoadingPrompt.Show("Saving...", LoadingSpinnerType.SocialClubSaving); 
            await MData.API.SendSaveToCloud();
            await BaseScript.Delay(1000);
            Screen.LoadingPrompt.Hide();
        }
    }
}
