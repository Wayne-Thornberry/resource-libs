using CDebugActions;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CWorldObjects.Data;
using CWorldObjects.Data.Ownership;
using CWorldObjects.Internal;
using Newtonsoft.Json;

using Proline.Resource.IO;
using System;
using System.Linq;
using Console = Proline.Resource.Console;

namespace CWorldObjects
{
    public partial class CWorldObjectsAPI : ICWorldObjectsAPI
    {
        public string GetNearestBuilding()
        {
            var api = new CDebugActionsAPI();
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
                api.LogError(e);
            }
            return null;
        }

        public Vector3 GetBuildingPosition(string buildingId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                return new Vector3(buildingMetaData.WorldPos.X, buildingMetaData.WorldPos.Y, 0f);
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return Vector3.One;
        }
        public Vector3 GetBuildingEntrance(string buildingId, int entranceId = 0)
        {
            var api = new CDebugActionsAPI();
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
                api.LogError(e);
            }
            return Vector3.One;
        }

        public int GetNumOfBuldingEntrances(string buildingId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData3.Load());
                return interiorMetadata.AccessPoints.Count;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return 0;
        }


        public string GetBuildingEntranceString(string buildingId, int entranceId = 0)
        {
            var api = new CDebugActionsAPI();
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
                api.LogError(e);
            }
            return "";
        }


        public string GetNearestBuildingEntrance(string building)
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (string.IsNullOrEmpty(building))
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
                api.LogError(e);
            }
            return null;
        }
        public Vector3 GetBuildingWorldPos(string buildingId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                return new Vector3(buildingMetaData.WorldPos.X, buildingMetaData.WorldPos.Y, 0);
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return Vector3.One;
        }

        public string EnterBuilding(string buildingId, string buildingEntrance)
        {
            var api = new CDebugActionsAPI();
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
                api.LogError(e);
            }
            return "";
        }
        public Vector3 ExitBuilding(string buildingId, string accessPoint, int type = 0)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load());
                var entrance = buildingMetaData.AccessPoints.FirstOrDefault(e => e.Id.Equals(accessPoint));
                api.LogDebug(accessPoint);
                api.LogDebug(entrance.Id);
                Vector3 pos = Vector3.One;
                switch (type)
                {
                    case 0: pos = entrance.ExitOnFoot.Position; break;
                    case 1: pos = entrance.ExitInVehicle.Position; break;
                }
                return pos;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return Vector3.One;
        }

        public void PlaceVehicleInGarageSlot(string propertyId, int index, Entity vehicle)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var garage = GetPropertyGarage(propertyId);
                var layout = GetPropertyGarageLayout(propertyId);
                var limit = GetPropertyGarageLimit(propertyId);
                if (index > limit)
                    return;
                var resourceData1 = ResourceFile.Load($"data/world/garages/{garage}.json");
                var garageInterior = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load());

                var resourceData2 = ResourceFile.Load($"data/world/interiors/{garageInterior.Interior}.json");
                var interior = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());


                var resourceData3 = ResourceFile.Load($"data/world/garages/layouts/{layout}.json");
                var garageLayout = JsonConvert.DeserializeObject<GarageLayout>(resourceData3.Load());


                api.LogDebug(garageLayout.VehicleSlots.Count());
                api.LogDebug(index);
                var slot = garageLayout.VehicleSlots[index];
                if (slot == null)
                    throw new Exception($"Slot not found");
                vehicle.Position = slot.Position;
                vehicle.Heading = slot.Heading;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        public int GetPropertyGarageLimit(string propertyId)
        {
            if (string.IsNullOrEmpty(propertyId))
                return 0;
            var pm = PropertyManager.GetInstance();
            var property = pm.GetProperty(propertyId);
            return property.VehicleCap;
        }

        public Vector3 GetBuildingWorldPos(object neariestBulding)
        {
            throw new NotImplementedException();
        }


        public Vector3 EnterInterior(string interiorId, string entranceId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
                var entryPoint = buildingMetaData.AccessPoints.FirstOrDefault(e => e.Id.Equals(entranceId));
                return entryPoint.OnFoot.Position;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return Vector3.One;
        }

        public string ExitInterior(string interiorId, string exitId)
        {
            var resourceData2 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
            var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
            var targetEntryPoint = interiorMetadata.AccessPoints.FirstOrDefault(e => e.Id.Equals(exitId));
            return targetEntryPoint.Tag;
        }


        public string GetNearestInterior()
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData = ResourceFile.Load($"data/world/interiors.json");
                var worldBuildings = JsonConvert.DeserializeObject<string[]>(resourceData.Load());
                var distance = 99999f;
                var entranceString = "";
                var playPos = new Vector2(Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y);
                foreach (var item in worldBuildings)
                {
                    var resourceData2 = ResourceFile.Load($"data/world/interiors/{item}.json");
                    var buildingMetaData = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
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
                api.LogError(e);
            }
            return null;
        }

        public Vector3 GetBuildingInterior(string buildingId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/interiors/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
                return new Vector3(buildingMetaData.WorldPos.X, buildingMetaData.WorldPos.Y, 0f);
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return Vector3.One;
        }
        public string GetNearestInteriorExit(string interiorId = null)
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (string.IsNullOrEmpty(interiorId))
                    interiorId = GetNearestInterior();
                var resourceData2 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
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
                api.LogError(e);
            }
            return null;
        }

        public int CreateMarker(Vector3 position, float activationRange = 2f)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var instance = MarkerManager.GetInstance();
                var marker = new Marker(position, new Vector3(0, 0, 0), Vector3.One, MarkerType.DebugSphere, System.Drawing.Color.FromArgb(150, 255, 255, 255));
                marker.ActivationRange = activationRange;
                return instance.AddMarker(marker);
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return 0;
        }

        public void DrawMarker(int handle)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var instance = MarkerManager.GetInstance();
                var marker = instance.GetMarker(handle);
                marker.Draw();
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        public bool IsInMarker(int handle, int obj)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var instance = MarkerManager.GetInstance();
                var marker = instance.GetMarker(handle);
                var prop = Entity.FromHandle(obj);
                return World.GetDistance(prop.Position, marker.Position) < marker.ActivationRange;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return false;
        }

        public void DeleteMarker(int handle)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var instance = MarkerManager.GetInstance();
                var marker = instance.GetMarker(handle);
                instance.RemoveMarker(handle);
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        // Properties -> Linkage (Building/Garage) -> Building & Interior -> Entrances/Exit & Points
        // apt_dpheights_he_01 apt_dpheights_01 apt_dpheights_ped_frt_ent_01
        // apt_dpheights_he_01 gar_dpheights_01 gar_dpheights_veh_frt_ent_01
        public Vector3 EnterProperty(string propertyId, string propertyPart, string entranceId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (string.IsNullOrEmpty(propertyId))
                    return Vector3.One;
                //var pm = PropertyManager.GetInstance();
                //var property = pm.GetProperty(propertyId);
                //var type = property.Type;
                //var garageString = property.Garage;
                //var aptString = property.Apartment; 

                //ResourceFile resourceData1 = null;
                //ResourceFile resourceData2 = null; 

                //var folderName = GetPropertyPartType(propertyPart);
                //api.LogDebug(folderName);
                //api.LogDebug(propertyPart);

                //// Load the link file
                //resourceData1 = ResourceFile.Load($"data/world/{folderName}/{propertyPart}.json");
                //var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load()); 
                //var targetEntryPointString = buildingInteriorLink.ExteriorEntrances[entranceId];

                //api.LogDebug(folderName);
                //api.LogDebug(buildingInteriorLink.Interior);
                //// Load interior file
                //resourceData2 = ResourceFile.Load($"data/world/interiors/{buildingInteriorLink.Interior}.json");
                //var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load()); 
                //var targetEntryPoint = interiorMetadata.EntrancePoints.FirstOrDefault(e => e.Id.Equals(targetEntryPointString));
                return Vector3.One;

            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return Vector3.One;
        }



        public string GetPropertyExit(string propertyPart, string functionType, string neariestExit)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData1 = ResourceFile.Load($"data/world/{functionType}/{propertyPart}.json");
                var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load());
                var targetEntryPointString = buildingInteriorLink.InteriorExits[neariestExit];
                return targetEntryPointString;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";
        }

        public string GetPropertyEntry(string propertyPart, string functionType, string entranceString)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData1 = ResourceFile.Load($"data/world/{functionType}/{propertyPart}.json");
                var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load());
                var targetEntryPointString = buildingInteriorLink.ExteriorEntrances[entranceString];
                return targetEntryPointString;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";

        }

        public string GetPropertyApartment(string propertyId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (string.IsNullOrEmpty(propertyId))
                    return "";
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                return property.Apartment;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";

        }

        public string GetPropertyGarage(string propertyId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (string.IsNullOrEmpty(propertyId))
                    return "";
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                return property.Garage;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";

        }

        public string GetPropertyGarageLayout(string propertyId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (string.IsNullOrEmpty(propertyId))
                    return "";
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                return property.Layout;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";

        }

        public string GetPropertyInterior(string propertyId, string propertyType)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                var resourceData1 = ResourceFile.Load($"data/world/{propertyType}/{propertyId}.json");
                var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load());
                return buildingInteriorLink.Interior;

            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";
        }


        public string GetPropertyPartType(string partName)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var propertyType = partName.Split('_')[0];
                switch (propertyType)
                {
                    case "apt":
                        return "apartments";
                        break;
                    case "gar":
                        return "garages";
                        break;
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";
        }
        //apt_high_pier_01_ext_ped_01
        public string ExitProperty(string propertyId, string propertyPart, string exitId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                //if (string.IsNullOrEmpty(propertyId))
                //    return "";
                //var pm = PropertyManager.GetInstance();
                //var property = pm.GetProperty(propertyId);
                //var type = property.Type;
                //var garageString = property.Garage;
                //var aptString = property.Apartment; 

                //ResourceFile resourceData1 = null;
                //ResourceFile resourceData2 = null; 

                //var propertyType = propertyPart.Split('_')[0];
                //var abc = GetPropertyPartType(propertyPart);

                //api.LogDebug("tes");
                //api.LogDebug(exitId);

                //resourceData1 = ResourceFile.Load($"data/world/{abc}/{propertyPart}.json");
                //var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load()); 
                //var targetEntryPointString = buildingInteriorLink.InteriorExits[exitId];


                //resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingInteriorLink.Building}.json");
                //var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load()); 
                //var targetEntryPoint = interiorMetadata.ExitPoints.FirstOrDefault(e => e.Id.Equals(targetEntryPointString));

                //api.LogDebug("tes");
                //api.LogDebug(propertyPart);

                //Game.PlayerPed.Position = targetEntryPoint.Position;
                //return targetEntryPoint.Id;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";
        }

        public string GetPropertyBuilding(string propertyId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (string.IsNullOrEmpty(propertyId))
                    return "";
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                return property.Building;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";
        }

        public string GetInteriorExitString(string interiorId, int exitId = 0)
        {
            var api = new CDebugActionsAPI();
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData3.Load());
                var targetEntryPoint = interiorMetadata.AccessPoints[exitId];
                return targetEntryPoint.Id;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return "";
        }

        public Vector3 GetInteriorExit(string interiorId, int exitId = 0)
        {
            var api = new CDebugActionsAPI();
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData3.Load());
                var targetEntryPoint = interiorMetadata.AccessPoints[exitId];
                return targetEntryPoint.DoorPosition;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return Vector3.One;
        }





        public int GetNumOfInteriorExits(string interiorId)
        {
            var api = new CDebugActionsAPI();
            try
            {
                ResourceFile resourceData = null;
                resourceData = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData.Load());
                return interiorMetadata.AccessPoints.Count;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return 0;
        }

    }
}
