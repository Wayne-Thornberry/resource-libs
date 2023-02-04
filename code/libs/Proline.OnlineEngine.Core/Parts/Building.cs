using CitizenFX.Core;
using CWorldObjects;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static string GetNearestBuilding()
        {
            var api = new CWorldObjectsAPI();
            return api.GetNearestBuilding();
        }

        public static Vector3 GetBuildingPosition(string buildingId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetBuildingPosition(buildingId);

        }

        public static Vector3 GetBuildingEntrance(string buildingId, int entranceId = 0)
        {
            var api = new CWorldObjectsAPI();
            return api.GetBuildingEntrance(buildingId, entranceId);

        }

        public static int GetNumOfBuldingEntrances(string buildingId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetNumOfBuldingEntrances(buildingId);

        }

        public static string GetBuildingEntranceString(string buildingId, int entranceId = 0)
        {
            var api = new CWorldObjectsAPI();
            return api.GetBuildingEntranceString(buildingId, entranceId);

        }


        public static string GetNearestBuildingEntrance(string building)
        {
            var api = new CWorldObjectsAPI();
            return api.GetNearestBuildingEntrance(building);

        }
        public static Vector3 GetBuildingWorldPos(string buildingId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetBuildingWorldPos(buildingId);

        }

        public static string EnterBuilding(string buildingId, string buildingEntrance)
        {
            var api = new CWorldObjectsAPI();
            return api.EnterBuilding(buildingId, buildingEntrance);

        }
        public static Vector3 ExitBuilding(string buildingId, string accessPoint, int type = 0)
        {
            var api = new CWorldObjectsAPI();
            return api.ExitBuilding(buildingId, accessPoint, type);

        }
    }
}
