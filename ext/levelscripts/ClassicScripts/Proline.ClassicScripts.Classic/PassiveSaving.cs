using CitizenFX.Core;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.ClassicOnline.MGame;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.ClassicOnline.MData;

namespace Proline.ClassicOnline.LevelScripts
{
    public class PassiveSaving
    {

        public async Task Execute(object[] args, CancellationToken token)
        { 
            var nextSaveTime = DateTime.UtcNow.AddMinutes(1); 
            var state = 0;
            var ticks = 0;
            while (true)
            {
                switch (state)
                {
                    case 0:
                        if (DateTime.UtcNow > nextSaveTime)
                        {
                            MDataAPI.CreateFile();
                            MDataAPI.AddFileValue("PlayerHealth", Game.PlayerPed.Health);
                            MDataAPI.AddFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
                            MDataAPI.SaveFile();
                            nextSaveTime = DateTime.UtcNow.AddMinutes(1);
                            Screen.LoadingPrompt.Show("Saving...", LoadingSpinnerType.SocialClubSaving);
                            state = 1;
                        }break;
                    case 1:
                        if (!MDataAPI.IsSaveInProgress())
                        {
                            switch (MDataAPI.GetSaveState())
                            {
                                case 0: Screen.LoadingPrompt.Show("Save complete"); break;
                                case 1: Screen.LoadingPrompt.Show("Save failed... "); break;
                            }
                            ticks = 0;
                            state = 2;
                        }break;
                    case 2:
                        if (Screen.LoadingPrompt.IsActive && ticks > 1000)
                        {
                            Screen.LoadingPrompt.Hide();
                            state = 0;
                        }
                        ticks++;
                        break;
                }  
                await BaseScript.Delay(0);
            } 

        }
    }
}
