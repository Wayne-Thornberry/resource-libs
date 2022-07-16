using CitizenFX.Core.Native;
using Proline.ClassicOnline.GCharacter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MGame
{ 

    public static partial class MGameAPI
    {

        public static void SetPedLooks(int pedHandle, CharacterLooks looks)
        {
            try
            {
                API.SetPedHeadBlendData(pedHandle, looks.Father, looks.Mother, 0, looks.Father, looks.Mother, 0, looks.Resemblence, looks.SkinTone, 0, true);

                if(looks.Hair != null)
                    API.SetPedHairColor(pedHandle, looks.Hair.Color, looks.Hair.HighlightColor);
                API.SetPedEyeColor(pedHandle, looks.EyeColor);

                if(looks.Overlays != null)
                { 
                    for (int i = 0; i < looks.Overlays.Length; i++)
                    {
                        API.SetPedHeadOverlay(pedHandle, i, looks.Overlays[i].Index, looks.Overlays[i].Opacity);
                        API.SetPedHeadOverlayColor(pedHandle, i, looks.Overlays[i].ColorType, looks.Overlays[i].PrimaryColor, looks.Overlays[i].SecondaryColor);
                    }

                }
                if (looks.Features != null)
                { 
                    for (int i = 0; i < looks.Features.Length; i++)
                    {
                        API.SetPedFaceFeature(pedHandle, i, looks.Features[i].Value);
                    }
                }
            }
            catch (Exception e)
            {
                MDebug.MDebugAPI.LogError(e);
            }
        }

        public static CharacterLooks GetPedLooks(int pedHandle)
        {
            try
            {
                int x = 0;
                API.GetPedHeadBlendData(pedHandle,ref x);
                MDebug.MDebugAPI.LogDebug(x);
                return new CharacterLooks()
                {
                    Mother = 0,
                    Father = 0,
                    Resemblence = 0.5f,
                    SkinTone = 0.5f,
                };
            }
            catch (Exception e)
            {
                MDebug.MDebugAPI.LogError(e);
            }
            return null;
        }

        public static void SetPedGender(int handle, char gender)
        {
            try
            {

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
