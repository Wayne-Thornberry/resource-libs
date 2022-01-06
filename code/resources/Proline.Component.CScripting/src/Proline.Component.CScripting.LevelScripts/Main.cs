using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.Component.Framework.Client.Access;
using Proline.CScripting.Framework;

namespace Proline.Classic.Scripts
{
    public class Main : ScriptInstance
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


            StartNewScript("UIMultiplayerMenu");
            StartNewScript("UIPlayerSwitch");
            StartNewScript("ExampleTest");
            StartNewScript("UIMainMenu");
            StartNewScript("RespawnScript");

            while (true)
            {
                API.SetInstancePriorityHint(4);
                if (CitizenFX.Core.Game.IsControlJustReleased(0, Control.MultiplayerInfo))
                {

                    API.DoScreenFadeOut(100);
                    API.ResurrectPed(CitizenFX.Core.Game.PlayerPed.Handle);
                    API.SwitchOutPlayer(CitizenFX.Core.Game.PlayerPed.Handle, 1, 1);
                    API.SetEntityCoordsNoOffset(CitizenFX.Core.Game.PlayerPed.Handle, -1336.161f, -3044.03f, 13.94f, false, false, false);
                    API.FreezeEntityPosition(CitizenFX.Core.Game.PlayerPed.Handle, true);
                    CitizenFX.Core.Game.PlayerPed.Position = new Vector3(0, 0, 70);
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
                    API.FreezeEntityPosition(CitizenFX.Core.Game.PlayerPed.Handle, false);
                    API.SwitchInPlayer(CitizenFX.Core.Game.PlayerPed.Handle);


                    while (API.IsPlayerSwitchInProgress())
                    {
                        await BaseScript.Delay(0);
                    }

                    LogDebug("sdaasda");

                    //Global.SetGlobalValue("Gamemode", "Freemode");

                    //var v = Global.GetGlobalValue("Gamemode");
                    //LogDebugLine(v.ToString());
                }
                await BaseScript.Delay(0);
            }
        }

    }
}
