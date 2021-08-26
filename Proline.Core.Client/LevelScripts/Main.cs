using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.Engine;

namespace Proline.Freemode.LevelScripts
{
    public class Main : LevelScript
    { 

        public Main()
        {
        }

        public int Frames { get; private set; }

        public override async Task Execute(params object[] args)
        {
            API.SwitchTrainTrack(0, true);
            API.SwitchTrainTrack(3, true);
            API.SetTrainTrackSpawnFrequency(0, 120000);
            API.SetRandomTrains(true);

            API.DoScreenFadeOut(100);
            API.ResurrectPed(Game.PlayerPed.Handle);
            API.SwitchOutPlayer(Game.PlayerPed.Handle, 1, 1);
            API.SetEntityCoordsNoOffset(Game.PlayerPed.Handle, -1336.161f, -3044.03f, 13.94f, false, false, false);
            API.FreezeEntityPosition(Game.PlayerPed.Handle, true);
            Game.PlayerPed.Position = new Vector3(0, 0, 70);
            Screen.LoadingPrompt.Show("Loading Freemode...");
            while (Frames < 500)
            {
                Frames++;
                await BaseScript.Delay(0);
            }

            LogDebug("THIS ISA  TEST");

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

            LogDebug("sdaasda");
            StartNewScript("UIMultiplayerMenu", null);
            StartNewScript("UIPlayerSwitch", null);
            StartNewScript("ExampleTest", null);
            StartNewScript("UIMainMenu", null);
            StartNewScript("RespawnScript", null);

            //Global.SetGlobalValue("Gamemode", "Freemode");

            //var v = Global.GetGlobalValue("Gamemode");
            //LogDebugLine(v.ToString());


            while (true)
            { 

                API.SetInstancePriorityHint(4);

                if (Game.IsControlJustReleased(0, Control.MultiplayerInfo))
                {
                    StartNewScript("UIMultiplayerMenu", null); 
                }  
                await BaseScript.Delay(0);
            }
        }

    }
}
