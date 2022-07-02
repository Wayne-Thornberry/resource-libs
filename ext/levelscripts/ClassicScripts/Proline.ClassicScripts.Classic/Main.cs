using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MGame;
using Proline.ClassicOnline.MGame.Data;
using Proline.ClassicOnline.MScripting;
using Proline.Resource;

namespace Proline.ClassicOnline.LevelScripts
{
    public class Main
    {

        public async Task Execute(object[] args, CancellationToken token)
        {

            var state = 0;
            while (!token.IsCancellationRequested)
            {
                API.SetInstancePriorityHint(0);

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
                            Screen.LoadingPrompt.Hide();
                            API.ShutdownLoadingScreen();
                            API.SetNoLoadingScreen(true);
                            API.DoScreenFadeIn(500); 
                            state = 1;
                        }
                        break;
                    case 1:
                        {
                            MScriptingAPI.StartNewScript("UIMainMenu");
                            while (MScriptingAPI.GetInstanceCountOfScript("UIMainMenu") > 0)
                            {
                                await BaseScript.Delay(1);
                            }
                            MScriptingAPI.StartNewScript("PlayerLoading");
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
                        break;
                    case 2:
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
                            MScriptingAPI.StartNewScript("VehicleFuel"); 
                            MScriptingAPI.StartNewScript("PassiveSaving"); 

                            //World.CreateBlip(new Vector3(-1662.98f, 5917.54f, -51f));
                            //World.CreateBlip(new Vector3(-466f, -688.9f, 100f));
                            //World.CreateBlip(new Vector3(1207.67f, -454.74f, 65.69f));
                            //World.CreateBlip(new Vector3(-1007.973f, -487.1707f, 38.9745f));
                            //World.CreateBlip(new Vector3(-143.7216f, 229.9331f, 93.9431f));
                            //World.CreateBlip(new Vector3(-1010.027f, -479.242f, 49.025f));
                            //World.CreateBlip(new Vector3(130.1238f, -1300.266f, 28.2811f));
                            //World.CreateBlip(new Vector3(130.1238f, -1300.266f, 28.2811f));
                            //World.CreateBlip(new Vector3(959.3535f, -2994.553f, -39.9685f));
                            //World.CreateBlip(new Vector3(-1662.98f, 5917.54f, -51f));
                            //World.CreateBlip(new Vector3(-466f, -688.9f, 100f));


                            //World.CreateBlip(new Vector3(1719.613f, 4780.799f, 47.016f));
                            //World.CreateBlip(new Vector3(1856.554f, 3790.708f, 35.685f));
                            //World.CreateBlip(new Vector3(1119.367f, 2688.088f, 37.503f));
                            //World.CreateBlip(new Vector3(1504.917f, 3641.313f, 33.909f));
                            //World.CreateBlip(new Vector3(-76.144f, 6499.662f, 30.491f));
                            //World.CreateBlip(new Vector3(-263.768f, 6264.983f, 30.359f));
                            //World.CreateBlip(new Vector3(-3235.225f, 1069.281f, 16.902f));
                            //World.CreateBlip(new Vector3(459.142f, 2656.295f, 42.441f));
                            //World.CreateBlip(new Vector3(-3010.025f, 490.341f, 11.458f));
                            //World.CreateBlip(new Vector3(1568.543f, 6403.176f, 23.896f));
                            //World.CreateBlip(new Vector3(1432.17f, 1129.087f, 113.296f));
                            //World.CreateBlip(new Vector3(1287.894f, -1657.979f, 48.546f));
                            //World.CreateBlip(new Vector3(1135.572f, -450.011f, 76.279f));
                            //World.CreateBlip(new Vector3(-1279.644f, -1410.419f, 6.535f));
                            //World.CreateBlip(new Vector3(-1758.007f, -1141.903f, 12.115f));
                            //World.CreateBlip(new Vector3(-2269.288f, 289.867f, 173.568f));
                            //World.CreateBlip(new Vector3(-1107.278f, -505.527f, 34.335f));
                            //World.CreateBlip(new Vector3(205.832f, -933.469f, 29.692f));
                            //World.CreateBlip(new Vector3(207.991f, 213.379f, 104.605f));
                            //World.CreateBlip(new Vector3(-742.511f, 43.149f, 45.473f));
                            //World.CreateBlip(new Vector3(-742.511f, 43.149f, 45.473f));

                            //for (int i = 0; i < 20; i++)
                            //{
                            //    World.CreateBlip(func2364(i)); 
                            //}

                            for (int i = 747; i < 792; i++)
                            {
                                World.CreateBlip(func7199(i));
                            }


                        }
                        state = 3;
                        break;
                    case 3:
                        if (Game.IsControlJustPressed(0, Control.PhoneUp))
                        {
                            MScriptingAPI.StartNewScript("PlayerLoading");
                            while (MScriptingAPI.GetInstanceCountOfScript("PlayerLoading") > 0)
                            {
                                await BaseScript.Delay(1);
                            }
                        } else if (Game.IsControlJustPressed(0, Control.PhoneDown))
                        { 
                            MScriptingAPI.StartNewScript("SaveNow");
                        }else if(Game.IsControlJustPressed(0, Control.MultiplayerInfo))
                        {
                            MScriptingAPI.StartNewScript("EditorScript");
                        }
                        state = 4;
                        break;
                    case 4: 
                        while (MScriptingAPI.GetInstanceCountOfScript("EditorScript") > 0)
                        {
                            await BaseScript.Delay(1);
                        }
                        state = 3;
                        break;
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

                //MScriptingAPI.MarkScriptAsNoLongerNeeded();
                await BaseScript.Delay(0);
            }
        }

        Vector3 func7199(int iParam0)
        {
            switch (iParam0)
            {
                case 747:
                    return new Vector3( 2332.839f, 2582.595f, 45.6677f);

                case 748:
                    return new Vector3( -436.1613f, 6028.733f, 30.3405f);

                case 749:
                    return new Vector3( -1068.84f, -851.9506f, 3.8671f);

                case 750:
                    return new Vector3( 1865.898f, 3699.468f, 32.5628f);

                case 751:
                    return new Vector3( 446.1463f, -1019.909f, 27.5497f);

                case 752:
                    return new Vector3( -104.1688f, 2796.184f, 52.3266f);

                case 753:
                    return new Vector3( 934.3446f, -1803.678f, 29.7528f);

                case 754:
                    return new Vector3( -138.0878f, -1396.641f, 28.8028f);

                case 805:
                    return new Vector3( -1081.882f, -501.9055f, 35.6193f);

                case 756:
                    return new Vector3( 934f, -2.6f, 78f);

                case 755:
                    return new Vector3( -1079.98f, -2867.023f, 12.9505f);

                case 758:
                    return new Vector3( -154.8892f, -2663.216f, 5.0002f);

                case 759:
                    return new Vector3( -1615.657f, 5271.33f, -0.4f);

                case 760:
                    return new Vector3( 1738.386f, 3280.542f, 40.1063f);

                case 761:
                    return new Vector3( -1148.742f, -3403.921f, 12.945f);

                case 762:
                    return new Vector3( -1394.095f, -3263.974f, 12.9448f);

                case 763:
                    return new Vector3( 1376.011f, -4400.693f, 150.6801f);

                case 764:
                    return new Vector3( -1294.799f, -3539.78f, 0.1343f);

                case 757:
                    return new Vector3(0f, 30.6f, -1f);

                case 796:
                    return new Vector3( 360.084f, -74.7928f, 66.1459f);

                case 798:
                    return new Vector3( -1031.865f, -410.4693f, 32.2732f);

                case 800:
                    return new Vector3( -616.354f, -738.2424f, 26.8466f);

                case 802:
                    return new Vector3( -987.0505f, -765.7468f, 14.7657f);

                case 803:
                    return new Vector3( 214.4743f, -1.2945f, 73.3064f);

                case 804:
                    return new Vector3( -1421.36f, -240.4189f, 45.3791f);

                case 765:
                    return new Vector3( 762.16f, -677.76f, 27.7908f);

                case 766:
                    return new Vector3( -147.3731f, -1343.693f, 28.8769f);

                case 767:
                    return new Vector3( -169.0834f, -34.9386f, 51.4423f);

                case 768:
                    return new Vector3( 235.1787f, -1874.712f, 25.4822f);

                case 769:
                    return new Vector3( 492.1651f, -894.889f, 24.742f);

                case 770:
                    return new Vector3( 1170.16f, -2973.49f, 4.902108f);

                case 771:
                    return new Vector3( 1445.205f, 2327.525f, 72.8152f);

                case 772:
                    return new Vector3( 1411.023f, 2582.441f, 36.0159f);

                case 792:
                    return new Vector3( -153.3114f, -2658.146f, 5.001f);

                case 793:
                    return new Vector3( 194.9886f, 2742.808f, 42.4263f);

                case 794:
                    return new Vector3( 407.2454f, -983.9472f, 28.2663f);

                case 773:
                    return new Vector3( -538.8397f, 486.7372f, 102.0298f);

                case 774:
                    return new Vector3( -994.163f, 789.741f, 171.291f);

                case 775:
                    return new Vector3( -1948.359f, 201.3107f, 85.0223f);

                case 776:
                    return new Vector3( 51.8459f, 562.2281f, 179.3021f);

                case 777:
                    return new Vector3( -3081.445f, 222.5399f, 13.0176f);

                case 778:
                    return new Vector3( -815.6112f, -1329.625f, 4.0004f);

                case 779:
                    return new Vector3( -1853.117f, -361.2698f, 48.2661f);

                case 780:
                    return new Vector3( -3234.458f, 948.7831f, 12.2371f);

                case 781:
                    return new Vector3( 1716.731f, 4668.851f, 42.1248f);

                case 782:
                    return new Vector3( -1107.208f, -1049.329f, 1.1504f);

                case 783:
                    return new Vector3( -1162.099f, -1466.775f, 3.3097f);

                case 784:
                    return new Vector3( -1808.288f, -631.7379f, 10.0042f);

                case 785:
                    return new Vector3( 875.6288f, -507.5239f, 56.4763f);

                case 786:
                    return new Vector3( -1562.304f, -288.0245f, 47.2757f);

                case 787:
                    return new Vector3( -1018.469f, -2731.378f, 12.6965f);

                case 788:
                    return new Vector3( -588.9297f, 191.3239f, 70.2865f);

                case 789:
                    return new Vector3( -176.8f, -608.2f, 31.4f);

                case 790:
                    return new Vector3( 140.1f, -726f, 32.1f);

                case 791:
                    return new Vector3( 42.9f, -884.1f, 29.3f); 
            }
            return new Vector3( 0f, 0f, 0f);
        }

        public Vector3 func2364(int iParam0)
        {
            switch (iParam0)
            {
                case 0:
                    return new Vector3( 1899.775f, 4924.601f, 47.778f);
                    break;

                case 1:
                    return new Vector3( 422.9873f, 6473.948f, 34.858f);
                    break;

                case 2:
                    return new Vector3( 157.0447f, 3130.252f, 42.579f);
                    break;

                case 3:
                    return new Vector3( 1439.663f, 6328.44f, 22.899f);
                    break;

                case 4:
                    return new Vector3( 3825.094f, 4473.658f, 4.145f);
                    break;

                case 5:
                    return new Vector3( 1785.479f, 3895.425f, 33.359f);
                    break;

                case 6:
                    return new Vector3( 2371.814f, 4937.272f, 65.763f);
                    break;

                case 7:
                    return new Vector3( 1346.934f, 4389.81f, 43.324f);
                    break;

                case 8:
                    return new Vector3( 1539.132f, 1727.533f, 108.823f);
                    break;

                case 9:
                    return new Vector3( 2259.848f, 3048.661f, 44.668f);
                    break;

                case 10:
                    return new Vector3( -220.3342f, 3657.054f, 63.402f);
                    break;

                case 11:
                    return new Vector3( -1925.977f, 1788.595f, 171.511f);
                    break;

                case 12:
                    return new Vector3( -1004.814f, 4854.116f, 273.601f);
                    break;

                case 13:
                    return new Vector3( 1760.34f, 3333.719f, 40.789f);
                    break;

                case 14:
                    return new Vector3( 172.1678f, 2220.485f, 89.764f);
                    break;

                case 15:
                    return new Vector3( -470.2161f, 6289.86f, 12.591f);
                    break;

                case 16:
                    return new Vector3( 1460.876f, 1042.926f, 113.314f);
                    break;

                case 17:
                    return new Vector3( -2166.679f, 5197.328f, 15.865f);
                    break;

                case 18:
                    return new Vector3( 66.9548f, 6663.253f, 30.762f);
                    break;

                case 19:
                    return new Vector3( 3435.816f, 5171.709f, 6.366f);
                    break;
            }
            return new Vector3( -1602.755f, 4498.812f, 18.201f);
        }

    }
}
