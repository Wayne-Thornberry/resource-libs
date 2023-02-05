using CitizenFX.Core;

namespace CWorldObjects
{
    public interface ICWorldObjectsAPI
    {
        int CreateMarker(Vector3 position, float activationRange = 2);
        void DeleteMarker(int handle);
        void DrawMarker(int handle);
        string EnterBuilding(string buildingId, string buildingEntrance);
        Vector3 EnterInterior(string interiorId, string entranceId);
        Vector3 EnterProperty(string propertyId, string propertyPart, string entranceId);
        Vector3 ExitBuilding(string buildingId, string accessPoint, int type = 0);
        string ExitInterior(string interiorId, string exitId);
        string ExitProperty(string propertyId, string propertyPart, string exitId);
        Vector3 GetBuildingEntrance(string buildingId, int entranceId = 0);
        string GetBuildingEntranceString(string buildingId, int entranceId = 0);
        Vector3 GetBuildingPosition(string buildingId);
        Vector3 GetBuildingWorldPos(object neariestBulding);
        Vector3 GetBuildingWorldPos(string buildingId);
        Vector3 GetInteriorExit(string interiorId, int exitId = 0);
        string GetInteriorExitString(string interiorId, int exitId = 0);
        string GetNearestBuilding();
        string GetNearestBuildingEntrance(string building);
        string GetNearestInterior();
        string GetNearestInteriorExit(string interiorId = null);
        int GetNumOfBuldingEntrances(string buildingId);
        int GetNumOfInteriorExits(string interiorId);
        string GetPropertyApartment(string propertyId);
        string GetPropertyBuilding(string propertyId);

        Vector3 GetBuildingInterior(string buildingId);
        string GetPropertyEntry(string propertyPart, string functionType, string entranceString);
        string GetPropertyExit(string propertyPart, string functionType, string neariestExit);
        string GetPropertyGarage(string propertyId);
        string GetPropertyGarageLayout(string propertyId);
        int GetPropertyGarageLimit(string propertyId);
        string GetPropertyInterior(string propertyId, string propertyType);
        string GetPropertyPartType(string partName);
        bool IsInMarker(int handle, int obj);
        void PlaceVehicleInGarageSlot(string propertyId, int index, Entity vehicle);
    }
}