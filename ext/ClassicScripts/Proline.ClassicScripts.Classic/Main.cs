using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.EngineFramework;
using Proline.EngineFramework.Scripting;
using Proline.ExampleClient.ComponentUI;
using Proline.ExampleClient.DataComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ExampleClient.Scripts
{
    public class Main : StartupScript
    {
        public override async Task Execute(object[] args, CancellationToken token)
        {
            // Enable Trains
            API.SwitchTrainTrack(0, true);
            API.SwitchTrainTrack(3, true);
            API.SetTrainTrackSpawnFrequency(0, 120000);
            API.SetRandomTrains(true);


            StartNewScript("DebugInterface");
            StartNewScript("ReArmouredTruck");
            StartNewScript("FMVechicleExporter");
            StartNewScript("PlayerDeath");
            StartNewScript("UIPlayerSwitch");
            StartNewScript("FMControls");
            StartNewScript("UIMainMenu");
            StartNewScript("LSCustoms", new Vector3(
            -339.84f,
             -136.81f,
             38.76f));


            LogDebug(ResourceFile.GetFileValue("data/character01.json", ""));

            while (!token.IsCancellationRequested)
            {
                API.SetInstancePriorityHint(4);


                if (Game.IsControlJustReleased(0, Control.MultiplayerInfo))
                {

                    API.DoScreenFadeOut(100);
                    API.ResurrectPed(Game.PlayerPed.Handle);
                    API.SwitchOutPlayer(Game.PlayerPed.Handle, 1, 1);
                    API.SetEntityCoordsNoOffset(Game.PlayerPed.Handle, -1336.161f, -3044.03f, 13.94f, false, false, false);
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, true);
                    Game.PlayerPed.Position = new Vector3(0, 0, 70);
                    Screen.LoadingPrompt.Show("Loading Freemode...");
                    var ms = 0;
                    while (ms < 500)
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

                    MarkScriptAsNoLongerNeeded();
                    //StartNewScript("CharacterCreator");
                }else if (Game.IsControlJustReleased(0, Control.PhoneUp))
                {
                    StartNewScript("EditorScript");
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
