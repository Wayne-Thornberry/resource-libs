using CGameLogic;
using CitizenFX.Core; 

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static bool IsInPersonalVehicle()
        {
            var api = new CGameLogicAPI();
            return api.IsInPersonalVehicle();

        }
        public static Entity GetPersonalVehicle()
        {
            var api = new CGameLogicAPI();
            return api.GetPersonalVehicle();
        }

        public static void DeletePersonalVehicle()
        {
            var api = new CGameLogicAPI();
            api.DeletePersonalVehicle();
        }


        public static void SetCharacterPersonalVehicle(int handle)
        { 
            var api = new CGameLogicAPI();
            api.SetCharacterPersonalVehicle(handle);
        }
    }
}
