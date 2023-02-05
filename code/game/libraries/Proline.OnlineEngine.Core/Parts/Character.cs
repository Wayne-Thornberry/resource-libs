using CGameLogic;
using CGameLogic.Data;
using System.Collections.Generic;

namespace Proline.ClassicOnline.Engine.Parts
{

    public static partial class EngineAPI
    { 

        public static void SetPedLooks(int handle, int mother, int father, float v1, float v2)
        {
            var api = new CGameLogicAPI();
            api.SetPedLooks(handle, mother, father, v1, v2);
        }

        public static void SetPedOutfit(string outfitName, int handle)
        {
            var api = new CGameLogicAPI();
            api.SetPedOutfit(outfitName, handle);
        }

        public static void AddValueToBankBalance(object payout)
        {
            var api = new CGameLogicAPI();
            api.AddValueToBankBalance(payout);
        }

        public static CharacterLooks GetPedLooks(int pedHandle)
        {
            var api = new CGameLogicAPI();
            return api.GetPedLooks(pedHandle);
        }

        public static Dictionary<string,object> GetChracterStats()
        {
            var api = new CGameLogicAPI();
            return api.GetCharacterStats();
        }


        public static int CreateCharacter()
        {
            var api = new CGameLogicAPI();
            return api.CreateNewCharacter();
        }

        public static void SetCharacter(int character)
        {
            var api = new CGameLogicAPI();
            api.SetCharacter(character);
        }
        public static bool HasCharacter()
        {
            var api = new CGameLogicAPI();
            return api.HasCharacter();
        }

        public static void SetPedGender(int handle, char gender)
        {
            var api = new CGameLogicAPI();
            api.SetPedGender(handle, gender);
        }
    }
}
