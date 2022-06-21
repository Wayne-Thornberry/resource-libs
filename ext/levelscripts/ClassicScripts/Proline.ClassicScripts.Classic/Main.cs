﻿using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.MData;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MScripting;

namespace Proline.ClassicOnline.LevelScripts
{
    public class Main
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Enable Trains
            API.SwitchTrainTrack(0, true);
            API.SwitchTrainTrack(3, true);
            API.SetTrainTrackSpawnFrequency(0, 120000);
            API.SetRandomTrains(true);



            MScriptingAPI.StartNewScript("DebugInterface");
            MScriptingAPI.StartNewScript("ReArmouredTruck");
            MScriptingAPI.StartNewScript("FMVechicleExporter");
            MScriptingAPI.StartNewScript("PlayerDeath");
            MScriptingAPI.StartNewScript("UIPlayerSwitch");
            MScriptingAPI.StartNewScript("FMControls");
            MScriptingAPI.StartNewScript("UIMainMenu");
            MScriptingAPI.StartNewScript("VehicleFuel");


            //Script.StartNewScript("LSCustoms", new Vector3(
            //-339.84f,
            // -136.81f,
            // 38.76f));


            // MDebugAPI.LogDebug(MDataAPI.GetFileValue("data/character01.json", ""));

            while (!token.IsCancellationRequested)
            {
                API.SetInstancePriorityHint(0);


                if (Game.IsControlJustReleased(0, Control.MultiplayerInfo))
                {

                    API.DoScreenFadeOut(100);
                    API.ResurrectPed(Game.PlayerPed.Handle);
                    API.SwitchOutPlayer(Game.PlayerPed.Handle, 1, 1);
                    API.SetEntityCoordsNoOffset(Game.PlayerPed.Handle, -1336.161f, -3044.03f, 13.94f, false, false, false);
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, true);
                    Game.PlayerPed.Position = new Vector3(0, 0, 70);
                    Screen.LoadingPrompt.Show("Loading Freemode...");
                    MScriptingAPI.StartNewScript("PlayerLoading");
                    var ms = 0;
                    while (ms < 500 && MScriptingAPI.GetInstanceCountOfScript("PlayerLoading") > 0)
                    {
                        ms++;
                        await BaseScript.Delay(1);
                    } 
                    Screen.LoadingPrompt.Hide();
                    API.ShutdownLoadingScreen();
                    API.SetNoLoadingScreen(true);
                    API.DoScreenFadeIn(500);
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, false);
                    API.SwitchInPlayer(Game.PlayerPed.Handle);


                    while (API.IsPlayerSwitchInProgress())
                    {
                        await BaseScript.Delay(0);
                    }
                    MScriptingAPI.StartNewScript("PassiveSaving");
                    MScriptingAPI.MarkScriptAsNoLongerNeeded();
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
