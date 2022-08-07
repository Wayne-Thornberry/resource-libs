using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MWord;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MWorld
{
    public static partial class WorldAPI
    {
        public static string GetNearestBuilding()
        {
            try
            { 
                var resourceData = ResourceFile.Load($"data/world/buildings.json");
                var worldBuildings = JsonConvert.DeserializeObject<string[]>(resourceData.Load());
                var distance = 99999f;
                var entranceString = "";
                var playPos = new Vector2(Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y);
                foreach (var item in worldBuildings)
                { 
                    var resourceData2 = ResourceFile.Load($"data/world/buildings/{item}.json");
                    var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                    var d = API.GetDistanceBetweenCoords(buildingMetaData.WorldPos.X,
                        buildingMetaData.WorldPos.Y, 0, playPos.X, playPos.Y, 0, false);
                    if (d < distance)
                    { 
                        distance = d;
                        entranceString = buildingMetaData.Id;
                    }
                }
                return entranceString;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return null;
        }

        internal static Vector3 GetBuildingPosition(string buildingId)
        {
            try
            { 
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                return new Vector3(buildingMetaData.WorldPos.X, buildingMetaData.WorldPos.Y, 0f);
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }
        public static Vector3 GetBuildingEntrance(string buildingId, int entranceId = 0)
        {
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData3.Load());
                var targetEntryPoint = interiorMetadata.AccessPoints[entranceId];
                return targetEntryPoint.DoorPosition;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static int GetNumOfBuldingEntrances(string buildingId)
        {
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData3.Load());
                return interiorMetadata.AccessPoints.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }


        public static string GetBuildingEntranceString(string buildingId, int entranceId = 0)
        {
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData3.Load());
                var targetEntryPoint = interiorMetadata.AccessPoints[entranceId];
                return targetEntryPoint.Id;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return "";
        }


        public static string GetNearestBuildingEntrance(string building)
        { 
            try
            {
                if(string.IsNullOrEmpty(building))
                 building = GetNearestBuilding(); 
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{building}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                var distance = 99999f;
                var entranceString = "";
                foreach (var item in interiorMetadata.AccessPoints)
                {
                    var newDistance = World.GetDistance(item.DoorPosition, Game.PlayerPed.Position);
                    if (World.GetDistance(item.DoorPosition, Game.PlayerPed.Position) < distance)
                    {
                        distance = newDistance;
                        entranceString = item.Id;
                    }
                }
                return entranceString;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return null;
        }
        public static Vector3 GetBuildingWorldPos(string buildingId)
        {
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load()); 
                return new Vector3(buildingMetaData.WorldPos.X, buildingMetaData.WorldPos.Y, 0);
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static string EnterBuilding(string buildingId, string buildingEntrance)
        { 
            try
            { 
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                var entrance = buildingMetaData.AccessPoints.FirstOrDefault(e => e.Id.Equals(buildingEntrance));
                if (string.IsNullOrEmpty(entrance.Function))
                    return "Apartment";
                return entrance.Function;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return ""; 
        }
        public static Vector3 ExitBuilding(string buildingId, string accessPoint, int type = 0)
        {
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                var entrance = buildingMetaData.AccessPoints.FirstOrDefault(e => e.Id.Equals(accessPoint));
                MDebugAPI.LogDebug(accessPoint);
                MDebugAPI.LogDebug(entrance.Id);
                Vector3 pos = Vector3.One;
                switch (type)
                {
                    case 0: pos = entrance.ExitOnFoot.Position; break;
                    case 1: pos =  entrance.ExitInVehicle.Position; break;
                }
                return pos;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }
    }
}
