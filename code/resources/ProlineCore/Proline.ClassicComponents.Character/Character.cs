using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.GCharacter.Data;
using Proline.Resource.IO;
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
                if (CharacterGlobals.Character != null)
                    CharacterGlobals.Character.Looks = looks;
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

        public static void SetPedOutfit(string outfitName, int handle)
        {
            try
            {
                var outfitJson = ResourceFile.Load($"data/character/outfits/{outfitName}.json");
                var characterPedOutfitM = JsonConvert.DeserializeObject<CharacterOutfit>(outfitJson.GetData());
                var components = characterPedOutfitM.Components;
                for (int i = 0; i < components.Length; i++)
                {
                    var component = components[i];
                    API.SetPedComponentVariation(handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);
                }
            }
            catch (Exception e)
            {
                MDebug.MDebugAPI.LogError(e);
            } 
        }

        public static void AddValueToBankBalance(object payout)
        {
            throw new NotImplementedException();
        }

        public static CharacterLooks GetPedLooks(int pedHandle)
        {
            try
            {
                //int x = 0;
                //API.GetPedHeadBlendData(pedHandle,ref x);
                //MDebug.MDebugAPI.LogDebug(x);
                if (CharacterGlobals.Character != null)
                    return CharacterGlobals.Character.Looks;
                else
                    return null;
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
