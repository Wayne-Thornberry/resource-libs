using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MWord;
using Proline.ClassicOnline.MWorld.Internal;
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
        // Properties -> Linkage (Building/Garage) -> Building & Interior -> Entrances/Exit & Points
        // apt_dpheights_he_01 apt_dpheights_01 apt_dpheights_ped_frt_ent_01
        // apt_dpheights_he_01 gar_dpheights_01 gar_dpheights_veh_frt_ent_01
        public static Vector3 EnterProperty(string propertyId, string propertyPart, string entranceId)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyId))
                    return Vector3.One;
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                var type = property.Type;
                var garageString = property.Garage;
                var aptString = property.Apartment; 

                ResourceFile resourceData1 = null;
                ResourceFile resourceData2 = null; 
                 
                var folderName = GetPropertyPartType(propertyPart);
                MDebugAPI.LogDebug(folderName);
                MDebugAPI.LogDebug(propertyPart);

                // Load the link file
                resourceData1 = ResourceFile.Load($"data/world/{folderName}/{propertyPart}.json");
                var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load()); 
                var targetEntryPointString = buildingInteriorLink.ExteriorEntrances[entranceId];

                MDebugAPI.LogDebug(folderName);
                MDebugAPI.LogDebug(buildingInteriorLink.Interior);
                // Load interior file
                resourceData2 = ResourceFile.Load($"data/world/interiors/{buildingInteriorLink.Interior}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load()); 
                var targetEntryPoint = interiorMetadata.EntrancePoints.FirstOrDefault(e => e.Id.Equals(targetEntryPointString));
                Game.PlayerPed.Position = targetEntryPoint.Position;
                return targetEntryPoint.Position;

            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static string GetPropertyInterior(string propertyId, int propertyPart = 0)
        {
            try
            {
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                string x = "";
                switch (propertyPart)
                {
                    case 0: x = property.Garage; break;
                    case 1: x = property.Apartment; break;
                }
                var abc = GetPropertyPartType(x);
                var resourceData1 = ResourceFile.Load($"data/world/{abc}/{x}.json");
                var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load());
                return buildingInteriorLink.Interior;

            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return "";
        }


        public static string GetPropertyPartType(string partName)
        {
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
                MDebugAPI.LogError(e);
            }
            return "";
        }
        //apt_high_pier_01_ext_ped_01
        public static Vector3 ExitProperty(string propertyId, string propertyPart, string exitId)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyId))
                    return Vector3.One;
                var pm = PropertyManager.GetInstance();
                var property = pm.GetProperty(propertyId);
                var type = property.Type;
                var garageString = property.Garage;
                var aptString = property.Apartment; 

                ResourceFile resourceData1 = null;
                ResourceFile resourceData2 = null; 

                var propertyType = propertyPart.Split('_')[0];
                var abc = GetPropertyPartType(propertyPart);
                 
                MDebugAPI.LogDebug("tes");
                MDebugAPI.LogDebug(exitId);

                resourceData1 = ResourceFile.Load($"data/world/{abc}/{propertyPart}.json");
                var buildingInteriorLink = JsonConvert.DeserializeObject<BuildingInteriorLink>(resourceData1.Load()); 
                var targetEntryPointString = buildingInteriorLink.InteriorExits[exitId];


                resourceData2 = ResourceFile.Load($"data/world/buildings/{buildingInteriorLink.Building}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData2.Load()); 
                var targetEntryPoint = interiorMetadata.ExitPoints.FirstOrDefault(e => e.Id.Equals(targetEntryPointString));

                MDebugAPI.LogDebug("tes");
                MDebugAPI.LogDebug(propertyPart);

                Game.PlayerPed.Position = targetEntryPoint.Position;
                return targetEntryPoint.Position;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static string GetPropertyBuilding(string propertyId)
        {
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
                MDebugAPI.LogError(e);
            }
            return "";
        }

        public static string GetInteriorExitString(string interiorId, int exitId = 0)
        {
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData3.Load());
                var targetEntryPoint = interiorMetadata.Exits[exitId];
                return targetEntryPoint.Id;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return "";
        }

        public static Vector3 GetInteriorExit(string interiorId, int exitId = 0)
        {
            try
            {   
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData3.Load()); 
                var targetEntryPoint = interiorMetadata.Exits[exitId]; 
                return targetEntryPoint.DoorPosition;
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
                var targetEntryPoint = interiorMetadata.Entrances[entranceId];
                return targetEntryPoint.DoorPosition;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static string GetBuildingEntranceString(string buildingId, int entranceId = 0)
        {
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData3.Load());
                var targetEntryPoint = interiorMetadata.Entrances[entranceId];
                return targetEntryPoint.Id;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return "";
        }

         

        public static int GetNumOfBuldingEntrances(string buildingId)
        {
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/buildings/{buildingId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<BuildingMetadata>(resourceData3.Load()); 
                return interiorMetadata.Entrances.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }

       

        public static int GetNumOfInteriorExits(string interiorId)
        {
            try
            {
                ResourceFile resourceData3 = null;
                resourceData3 = ResourceFile.Load($"data/world/interiors/{interiorId}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData3.Load()); 
                return interiorMetadata.Exits.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }


    }
}
