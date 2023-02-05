using CitizenFX.Core;
using CWorldObjects;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static Vector3 EnterProperty(string propertyId, string propertyPart, string entranceId)
        {
            var api = new CWorldObjectsAPI();
            return api.EnterProperty(propertyId, propertyPart, entranceId);

        }

        public static string GetPropertyExit(string propertyPart, string functionType, string neariestExit)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyExit(propertyPart, functionType, neariestExit);

        }

        public static string GetPropertyEntry(string propertyPart, string functionType, string entranceString)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyEntry(propertyPart, functionType, entranceString);

        }

        public static string GetPropertyApartment(string propertyId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyApartment(propertyId);

        }

        public static string GetPropertyGarage(string propertyId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyGarage(propertyId);

        }

        public static string GetPropertyGarageLayout(string propertyId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyGarageLayout(propertyId);


        }

        public static string GetPropertyInterior(string propertyId, string propertyType)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyInterior(propertyId, propertyType);

        }


        public static string GetPropertyPartType(string partName)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyPartType(partName);

        }
        public static string ExitProperty(string propertyId, string propertyPart, string exitId)
        {
            var api = new CWorldObjectsAPI();
            return api.ExitProperty(propertyId, propertyPart, exitId);

        }

        public static string GetPropertyBuilding(string propertyId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetPropertyBuilding(propertyId);

        }

        public static string GetInteriorExitString(string interiorId, int exitId = 0)
        {
            var api = new CWorldObjectsAPI();
            return api.GetInteriorExitString(interiorId, exitId);

        }

        public static Vector3 GetInteriorExit(string interiorId, int exitId = 0)
        {
            var api = new CWorldObjectsAPI();
            return api.GetInteriorExit(interiorId, exitId);
        }


        public static int GetNumOfInteriorExits(string interiorId)
        {
            var api = new CWorldObjectsAPI();
            return api.GetNumOfInteriorExits(interiorId);
        }


    }
}
