using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class StartIntro
    {
        string[] sub_b0b5 = new[]{
                "MP_Plane_Passenger_1",
                "MP_Plane_Passenger_2",
                "MP_Plane_Passenger_3",
                "MP_Plane_Passenger_4",
                "MP_Plane_Passenger_5",
                "MP_Plane_Passenger_6",
                "MP_Plane_Passenger_7"
            };

        public void sub_b747(int ped, int a_1)
        {
            if (a_1 == 0)
            {
                API.SetPedComponentVariation(ped, 0, 21, 0, 0);
                API.SetPedComponentVariation(ped, 1, 0, 0, 0);
                API.SetPedComponentVariation(ped, 2, 9, 0, 0);
                API.SetPedComponentVariation(ped, 3, 1, 0, 0);
                API.SetPedComponentVariation(ped, 4, 9, 0, 0);
                API.SetPedComponentVariation(ped, 5, 0, 0, 0);
                API.SetPedComponentVariation(ped, 6, 4, 8, 0);
                API.SetPedComponentVariation(ped, 7, 0, 0, 0);
                API.SetPedComponentVariation(ped, 8, 15, 0, 0);
                API.SetPedComponentVariation(ped, 9, 0, 0, 0);
                API.SetPedComponentVariation(ped, 10, 0, 0, 0);
                API.SetPedComponentVariation(ped, 11, 10, 0, 0);
                API.ClearPedProp(ped, 0);
                API.ClearPedProp(ped, 1);
                API.ClearPedProp(ped, 2);
                API.ClearPedProp(ped, 3);
                API.ClearPedProp(ped, 4);
                API.ClearPedProp(ped, 5);
                API.ClearPedProp(ped, 6);
                API.ClearPedProp(ped, 7);
                API.ClearPedProp(ped, 8);
            }
            else if (a_1 == 1)
            {
                API.SetPedComponentVariation(ped, 0, 13, 0, 0);
                API.SetPedComponentVariation(ped, 1, 0, 0, 0);
                API.SetPedComponentVariation(ped, 2, 5, 4, 0);
                API.SetPedComponentVariation(ped, 3, 1, 0, 0);
                API.SetPedComponentVariation(ped, 4, 10, 0, 0);
                API.SetPedComponentVariation(ped, 5, 0, 0, 0);
                API.SetPedComponentVariation(ped, 6, 10, 0, 0);
                API.SetPedComponentVariation(ped, 7, 11, 2, 0);
                API.SetPedComponentVariation(ped, 8, 13, 6, 0);
                API.SetPedComponentVariation(ped, 9, 0, 0, 0);
                API.SetPedComponentVariation(ped, 10, 0, 0, 0);
                API.SetPedComponentVariation(ped, 11, 10, 0, 0);
                API.ClearPedProp(ped, 0);
                API.ClearPedProp(ped, 1);
                API.ClearPedProp(ped, 2);
                API.ClearPedProp(ped, 3);
                API.ClearPedProp(ped, 4);
                API.ClearPedProp(ped, 5);
                API.ClearPedProp(ped, 6);
                API.ClearPedProp(ped, 7);
                API.ClearPedProp(ped, 8);
            }
            else if (a_1 == 2)
            {
                API.SetPedComponentVariation(ped, 0, 15, 0, 0);
                API.SetPedComponentVariation(ped, 1, 0, 0, 0);
                API.SetPedComponentVariation(ped, 2, 1, 4, 0);
                API.SetPedComponentVariation(ped, 3, 1, 0, 0);
                API.SetPedComponentVariation(ped, 4, 0, 1, 0);
                API.SetPedComponentVariation(ped, 5, 0, 0, 0);
                API.SetPedComponentVariation(ped, 6, 1, 7, 0);
                API.SetPedComponentVariation(ped, 7, 0, 0, 0);
                API.SetPedComponentVariation(ped, 8, 2, 9, 0);
                API.SetPedComponentVariation(ped, 9, 0, 0, 0);
                API.SetPedComponentVariation(ped, 10, 0, 0, 0);
                API.SetPedComponentVariation(ped, 11, 6, 0, 0);
                API.ClearPedProp(ped, 0);
                API.ClearPedProp(ped, 1);
                API.ClearPedProp(ped, 2);
                API.ClearPedProp(ped, 3);
                API.ClearPedProp(ped, 4);
                API.ClearPedProp(ped, 5);
                API.ClearPedProp(ped, 6);
                API.ClearPedProp(ped, 7);
                API.ClearPedProp(ped, 8);
            }
            else if (a_1 == 3)
            {
                API.SetPedComponentVariation(ped, 0, 14, 0, 0);
                API.SetPedComponentVariation(ped, 1, 0, 0, 0);
                API.SetPedComponentVariation(ped, 2, 5, 3, 0);
                API.SetPedComponentVariation(ped, 3, 3, 0, 0);
                API.SetPedComponentVariation(ped, 4, 1, 6, 0);
                API.SetPedComponentVariation(ped, 5, 0, 0, 0);
                API.SetPedComponentVariation(ped, 6, 11, 5, 0);
                API.SetPedComponentVariation(ped, 7, 0, 0, 0);
                API.SetPedComponentVariation(ped, 8, 2, 0, 0);
                API.SetPedComponentVariation(ped, 9, 0, 0, 0);
                API.SetPedComponentVariation(ped, 10, 0, 0, 0);
                API.SetPedComponentVariation(ped, 11, 3, 12, 0);
                API.ClearPedProp(ped, 0);
                API.ClearPedProp(ped, 1);
                API.ClearPedProp(ped, 2);
                API.ClearPedProp(ped, 3);
                API.ClearPedProp(ped, 4);
                API.ClearPedProp(ped, 5);
                API.ClearPedProp(ped, 6);
                API.ClearPedProp(ped, 7);
                API.ClearPedProp(ped, 8);
            }
            else if (a_1 == 4)
            {
                API.SetPedComponentVariation(ped, 0, 18, 0, 0);
                API.SetPedComponentVariation(ped, 1, 0, 0, 0);
                API.SetPedComponentVariation(ped, 2, 15, 3, 0);
                API.SetPedComponentVariation(ped, 3, 15, 0, 0);
                API.SetPedComponentVariation(ped, 4, 2, 5, 0);
                API.SetPedComponentVariation(ped, 5, 0, 0, 0);
                API.SetPedComponentVariation(ped, 6, 4, 6, 0);
                API.SetPedComponentVariation(ped, 7, 4, 0, 0);
                API.SetPedComponentVariation(ped, 8, 3, 0, 0);
                API.SetPedComponentVariation(ped, 9, 0, 0, 0);
                API.SetPedComponentVariation(ped, 10, 0, 0, 0);
                API.SetPedComponentVariation(ped, 11, 4, 0, 0);
                API.ClearPedProp(ped, 0);
                API.ClearPedProp(ped, 1);
                API.ClearPedProp(ped, 2);
                API.ClearPedProp(ped, 3);
                API.ClearPedProp(ped, 4);
                API.ClearPedProp(ped, 5);
                API.ClearPedProp(ped, 6);
                API.ClearPedProp(ped, 7);
                API.ClearPedProp(ped, 8);
            }
            else if (a_1 == 5)
            {
                API.SetPedComponentVariation(ped, 0, 27, 0, 0);
                API.SetPedComponentVariation(ped, 1, 0, 0, 0);
                API.SetPedComponentVariation(ped, 2, 7, 3, 0);
                API.SetPedComponentVariation(ped, 3, 11, 0, 0);
                API.SetPedComponentVariation(ped, 4, 4, 8, 0);
                API.SetPedComponentVariation(ped, 5, 0, 0, 0);
                API.SetPedComponentVariation(ped, 6, 13, 14, 0);
                API.SetPedComponentVariation(ped, 7, 5, 3, 0);
                API.SetPedComponentVariation(ped, 8, 3, 0, 0);
                API.SetPedComponentVariation(ped, 9, 0, 0, 0);
                API.SetPedComponentVariation(ped, 10, 0, 0, 0);
                API.SetPedComponentVariation(ped, 11, 2, 7, 0);
                API.ClearPedProp(ped, 0);
                API.ClearPedProp(ped, 1);
                API.ClearPedProp(ped, 2);
                API.ClearPedProp(ped, 3);
                API.ClearPedProp(ped, 4);
                API.ClearPedProp(ped, 5);
                API.ClearPedProp(ped, 6);
                API.ClearPedProp(ped, 7);
                API.ClearPedProp(ped, 8);
            }
            else if (a_1 == 6)
            {
                API.SetPedComponentVariation(ped, 0, 16, 0, 0);
                API.SetPedComponentVariation(ped, 1, 0, 0, 0);
                API.SetPedComponentVariation(ped, 2, 15, 1, 0);
                API.SetPedComponentVariation(ped, 3, 3, 0, 0);
                API.SetPedComponentVariation(ped, 4, 5, 6, 0);
                API.SetPedComponentVariation(ped, 5, 0, 0, 0);
                API.SetPedComponentVariation(ped, 6, 2, 8, 0);
                API.SetPedComponentVariation(ped, 7, 0, 0, 0);
                API.SetPedComponentVariation(ped, 8, 2, 0, 0);
                API.SetPedComponentVariation(ped, 9, 0, 0, 0);
                API.SetPedComponentVariation(ped, 10, 0, 0, 0);
                API.SetPedComponentVariation(ped, 11, 3, 7, 0);
                API.ClearPedProp(ped, 0);
                API.ClearPedProp(ped, 1);
                API.ClearPedProp(ped, 2);
                API.ClearPedProp(ped, 3);
                API.ClearPedProp(ped, 4);
                API.ClearPedProp(ped, 5);
                API.ClearPedProp(ped, 6);
                API.ClearPedProp(ped, 7);
                API.ClearPedProp(ped, 8);
            }
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            while (Screen.Fading.IsFadingOut)
            {
                await BaseScript.Delay(0);
            }

            if (Screen.Fading.IsFadedOut)
                Screen.Fading.FadeIn(500);

            while (Screen.Fading.IsFadingIn)
            {
                await BaseScript.Delay(0);
            }

            API.PrepareMusicEvent("FM_INTRO_START");//FM_INTRO_START
            API.TriggerMusicEvent("FM_INTRO_START");//FM_INTRO_START
            var plyrId = API.PlayerPedId();// PLAYER ID

            if (IsMale(plyrId))
            {
                API.RequestCutsceneWithPlaybackList("MP_INTRO_CONCAT", 31, 1);
            }
            else
            {
                API.RequestCutsceneWithPlaybackList("MP_INTRO_CONCAT", 103, 1);
            }

            while (!API.HasCutsceneLoaded())
                await BaseScript.Delay(10);// - waiting for the cutscene to load

            if (IsMale(plyrId))
            {
                API.RegisterEntityForCutscene(0, "MP_Male_Character", 3, (uint)API.GetEntityModel(Game.PlayerPed.Model.Hash), 0);
                API.RegisterEntityForCutscene(API.PlayerPedId(), "MP_Male_Character", 0, 0, 0);
                API.SetCutsceneEntityStreamingFlags("MP_Male_Character", 0, 1);
                API.RegisterEntityForCutscene(0, "MP_Female_Character", 3, 0, 64);
                API.NetworkSetEntityInvisibleToNetwork(Game.PlayerPed.Handle, true);
            }
            else
            {
                API.RegisterEntityForCutscene(0, "MP_Female_Character", 3, (uint)API.GetEntityModel(API.PlayerPedId()), 0);
                API.RegisterEntityForCutscene(API.PlayerPedId(), "MP_Female_Character", 0, 0, 0);
                API.SetCutsceneEntityStreamingFlags("MP_Female_Character", 0, 1);
                API.RegisterEntityForCutscene(0, "MP_Male_Character", 3, 0, 64);
                API.NetworkSetEntityInvisibleToNetwork(Game.PlayerPed.Handle, true);
            }

            var ped = new int[10];
            for (int i = 0; i < 6; i++)
            {

                if (i == 1 || i == 2 || i == 4 || i == 6)
                {
                    ped[i] = API.CreatePed(26, (uint)API.GetHashKey("mp_f_freemode_01"), -1117.77783203125f, -1557.6248779296875f, 3.3819f, 0.0f, false, false);
                }

                else
                {
                    ped[i] = API.CreatePed(26, (uint)API.GetHashKey("mp_m_freemode_01"), -1117.77783203125f, -1557.6248779296875f, 3.3819f, 0.0f, false, false);
                }

                if (!API.IsEntityDead(ped[i]))
                {
                    sub_b747(ped[i], i);

                    API.FinalizeHeadBlend(ped[i]);

                    API.RegisterEntityForCutscene(ped[i], sub_b0b5[i], 0, 0, 64);

                }
            }


            API.NewLoadSceneStartSphere(-1212.79f, -1673.52f, 7, 1000, 0);//// - avoid texture bugs

            //////////////////////////////////////////////-
            API.SetWeatherTypeNow("EXTRASUNNY");//// SUN TIME

            API.StartCutscene(4);// - START the custscene 

            await BaseScript.Delay(31520);// - custscene time
            for (int i = 0; i < 6; i++)
            {
                API.DeleteEntity(ref ped[i]);
            }

            API.PrepareMusicEvent("AC_STOP");
            API.TriggerMusicEvent("AC_STOP");

        }


        public bool IsMale(int pedIOd)
        {
            return API.IsPedModel(pedIOd, (uint)API.GetHashKey("mp_m_freemode_01"));

        }

    }
}
