using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.Component.Framework.Client.Access;
using Proline.CScripting.Framework;
using Proline.Resource.Engine.Core;
using Proline.Resource.LevelScripts;

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


            EngineAPI.StartNewScript("UIMultiplayerMenu");
            EngineAPI.StartNewScript("UIPlayerSwitch");
            EngineAPI.StartNewScript("ExampleTest");
            EngineAPI.StartNewScript("UIMainMenu");
            EngineAPI.StartNewScript("RespawnScript");

            EngineAPI.CreateFile();
            EngineAPI.AddData("Test", 1);
            EngineAPI.AddData("Test2", true);
            EngineAPI.AddData("Test3", "string");
            EngineAPI.AddData("Test4", 1.0f);
            EngineAPI.AddData("Test5", new object[] { 1, "ses", false });
            EngineAPI.AddData("Test6", new TestData{ X = 1,  Y = "ses", Z = false });
            EngineAPI.UploadFile();

            while (true)
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

                    //Global.SetGlobalValue("Gamemode", "Freemode");

                    //var v = Global.GetGlobalValue("Gamemode");
                    //LogDebugLine(v.ToString());
                }
                await BaseScript.Delay(0);
            }
        }

    }
}
