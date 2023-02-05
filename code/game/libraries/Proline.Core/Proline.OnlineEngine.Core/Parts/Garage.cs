using CitizenFX.Core;
using CWorldObjects;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static void PlaceVehicleInGarageSlot(string propertyId, int index, Entity vehicle)
        {
            var api = new CWorldObjectsAPI();
            api.PlaceVehicleInGarageSlot(propertyId, index, vehicle);
        }

        public static int GetPropertyGarageLimit(string propertyId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyGarageLimit(propertyId);
        }

        public static Vector3 GetBuildingWorldPos(object neariestBulding)
        {
            var api = new CWorldObjectsAPI();
            return api.GetBuildingWorldPos(neariestBulding);
        }
    }
}
