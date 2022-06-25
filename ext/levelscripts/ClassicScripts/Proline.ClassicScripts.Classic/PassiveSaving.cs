using CitizenFX.Core;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.ClassicOnline.MGame;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.ClassicOnline.MData;
using Proline.CFXExtended.Core;
using CitizenFX.Core.Native;

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
                            if (!MDataAPI.IsFileLoaded())
                            {
                                MDataAPI.CreateFile();
                            };
                            MDataAPI.AddFileValue("PlayerHealth", Game.PlayerPed.Health);
                            MDataAPI.AddFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
                            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                            MDataAPI.AddFileValue("MP0_WALLET_BALANCE", stat.GetValue());
                            MDataAPI.AddFileValue("BANK_BALANCE", stat2.GetValue());

                            var list = new OutfitComponent[12];
                            for (int i = 0; i < list.Length; i++)
                            {
                                var component = list[i];
                                component.ComponentIndex = API.GetPedDrawableVariation(Game.PlayerPed.Handle, i);
                                component.ComponentPallet = API.GetPedPaletteVariation(Game.PlayerPed.Handle, i);
                                component.ComponentTexture =  API.GetPedTextureVariation(Game.PlayerPed.Handle, i);

                            }

                            var outfit = new PedOutfit();
                            outfit.Components = list;

                            var json = JsonConvert.SerializeObject(outfit);
                            MDataAPI.AddFileValue("CharacterOutfit", json);

                            // MDataAPI.AddFileValue("Character", JsonConvert.SerializeObject(Game.PlayerPed.Position));
                            await MDataAPI.SaveFile();
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
                        if (Screen.LoadingPrompt.IsActive && ticks > 100)
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
