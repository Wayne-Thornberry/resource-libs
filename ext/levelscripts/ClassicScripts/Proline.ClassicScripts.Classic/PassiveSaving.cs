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
            //   MGameAPI.SaveCurrentCar();
            var nextSaveTime = DateTime.UtcNow.AddMinutes(1);
            var saveInProgress = false;
            while (true)
            { 

                if (DateTime.UtcNow > nextSaveTime && !MDataAPI.IsSaveInProgress())
                {
                    Screen.LoadingPrompt.Show("Saving...", LoadingSpinnerType.SocialClubSaving);
                    MDataAPI.CreateFile();
                    MDataAPI.AddFileValue("PlayerHealth", Game.PlayerPed.Health);
                    MDataAPI.AddFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
                    MDataAPI.SaveFile();
                    saveInProgress = true;
                    nextSaveTime = DateTime.UtcNow.AddMinutes(1);
                }
                else
                { 
                    if (!MDataAPI.IsSaveInProgress() && saveInProgress)
                    {
                        switch (MDataAPI.GetSaveState())
                        {
                            case 0: Screen.LoadingPrompt.Show("Save complete"); break;
                            case 1: Screen.LoadingPrompt.Show("Save failed... "); break;
                        }
                        saveInProgress = false;
                    }
                }

                await BaseScript.Delay(0);
            } 

        }
    }
}
