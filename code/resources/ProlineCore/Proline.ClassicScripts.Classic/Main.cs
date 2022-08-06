
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GScripting;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MGame;

using Proline.ClassicOnline.MScripting;
using Proline.Resource;

namespace Proline.ClassicOnline.SClassic
{
    public class Main
    { 
        public async Task Execute(object[] args, CancellationToken token)
        {


            var state = 0;
            // Setup the world
            API.SetInstancePriorityMode(true);
            API.SetInstancePriorityHint(3);
            // Enable Trains
            ScriptingGlobals.Testing = 2f;

            API.SwitchTrainTrack(0, true);
            API.SwitchTrainTrack(3, true);
            API.SetTrainTrackSpawnFrequency(0, 120000);
            API.SetRandomTrains(true);
            while (!token.IsCancellationRequested)
            {
                switch (state)
                {
                    case 0:
                        {
                            API.DoScreenFadeOut(100);
                            API.ResurrectPed(Game.PlayerPed.Handle);
                            API.SwitchOutPlayer(Game.PlayerPed.Handle, 1, 1);
                            API.SetEntityCoordsNoOffset(Game.PlayerPed.Handle, -1336.161f, -3044.03f, 13.94f, false, false, false);
                            API.FreezeEntityPosition(Game.PlayerPed.Handle, true);
                            Game.PlayerPed.Position = new Vector3(0, 0, 70);
                            Screen.LoadingPrompt.Show("Loading Freemode...");
                            await BaseScript.Delay(5000);
                            state = 1;
                        }
                        break;
                    case 1:
                        {
                            Screen.LoadingPrompt.Hide();
                            API.ShutdownLoadingScreen();
                            API.SetNoLoadingScreen(true);
                            API.DoScreenFadeIn(500);

                            MScriptingAPI.StartNewScript("UIMainMenu");
                            while (MScriptingAPI.GetInstanceCountOfScript("UIMainMenu") > 0)
                            {
                                await BaseScript.Delay(1);
                            }

                            if(MScriptingAPI.GetInstanceCountOfScript("CharacterCreator") > 0)
                            {
                                state = 999;
                                break;
                            }
                            else if (MScriptingAPI.GetInstanceCountOfScript("PlayerLoading") > 0)
                            {
                                while (MScriptingAPI.GetInstanceCountOfScript("PlayerLoading") > 0)
                                {
                                    await BaseScript.Delay(1);
                                }
                                API.FreezeEntityPosition(Game.PlayerPed.Handle, false);
                                API.SwitchInPlayer(Game.PlayerPed.Handle);
                                while (API.IsPlayerSwitchInProgress())
                                {
                                    await BaseScript.Delay(0);
                                }
                                state = 2;
                            }

                        }
                        break;
                    case 2:
                        {

                            //  MScriptingAPI.StartNewScript("DebugInterface");
                            //MScriptingAPI.StartNewScript("ReArmouredTruck");
                            MScriptingAPI.StartNewScript("FMVechicleExporter");
                            MScriptingAPI.StartNewScript("PlayerDeath");
                            MScriptingAPI.StartNewScript("UIPlayerSwitch");
                            MScriptingAPI.StartNewScript("FMControls");
                            MScriptingAPI.StartNewScript("VehicleFuel");
                            MScriptingAPI.StartNewScript("PassiveSaving");
                            MScriptingAPI.StartNewScript("UIFreemodeHUD");
                            MScriptingAPI.StartNewScript("BlipController");
                            MScriptingAPI.StartNewScript("Freemode");
                            MScriptingAPI.StartNewScript("CharacterApts");
                            MDebug.MDebugAPI.LogDebug($"Calling Task ID for API {Thread.CurrentThread.ManagedThreadId}");
                        }
                        state = 3;
                        break;
                    case 3:

                        //if(API.GetInteriorFromEntity(Game.PlayerPed.Handle) > 0)
                        //{
                        //    if (Game.PlayerPed.IsInVehicle())
                        //    {
                        //        if(Game.IsControlJustPressed(0, Control.MoveUpDown))
                        //        {
                        //            Game.PlayerPed.CurrentVehicle.Position = new Vector3(0, 0, 70);
                        //        }
                        //    }
                        //}
                        break;
                    case 999: 
                        while (MScriptingAPI.GetInstanceCountOfScript("CharacterCreator") > 0)
                        {
                            await BaseScript.Delay(1);
                        }


                        while (MScriptingAPI.GetInstanceCountOfScript("StartIntro") > 0)
                        {
                            await BaseScript.Delay(1);
                        }
                        state = 2;

                        break;
                }
                if (state == 3)
                    break;
                await BaseScript.Delay(0);
            }
        }
    }
}


// var json = JsonConvert.SerializeObject(new
// {
// transactionType = "playSound",
// transactionFile = "demo",


// transactionVolume = 0.2f
// });
// Game.PlayerPed.Weapons.Give(WeaponHash.Parachute, 0, true, true);
// Game.PlayerPed.Position = new Vector3(0, 0, 1800);
// Game.PlayerPed.HasGravity = true;
// API.SetGravityLevel(2);
// Console.WriteLine(json);
// API.SendNuiMessage(json);