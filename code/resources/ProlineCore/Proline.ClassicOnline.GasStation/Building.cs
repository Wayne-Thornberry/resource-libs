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

        public static string GetNearestBuildingEntrance()
        { 
            try
            {
                var building = GetNearestBuilding(); 
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{building}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                var distance = 99999f;
                var entranceString = "";
                foreach (var item in interiorMetadata.Entrances)
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

        public static string EnterBuilding(string buildingId, string buildingEntrance)
        { 
            try
            { 
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                var entrance = buildingMetaData.Entrances.FirstOrDefault(e => e.Id.Equals(buildingEntrance));
                return entrance.Function;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return "";

        }
    }
}
