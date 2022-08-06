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

        public static void EnterApartmentProperty(string aptId, string aptString, int entrance = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return;
                var pm = PropertyManager.GetInstance();
                var propertyBuilding = (ApartmentBuilding) pm.GetProperty(aptId);
                if (propertyBuilding == null)
                    throw new Exception("Property building by id: " + aptId + " not found");
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));
                if (property == null)
                    throw new Exception("Property by id: " + aptString + " not found");
                var linkedEntrance = property.LinkedEntrances.ToArray()[entrance]; 


                var im = InteriorManager.GetInstance();
                var propertyInterior = im.GetInterior(property.Interior);
                var entryPoint = propertyInterior.EntrancePoints.FirstOrDefault(e => e.Id.Equals(linkedEntrance.Value));
                 

                Game.PlayerPed.Position = entryPoint.Position; 
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
        }

        public static void EnterProperty(string aptId, string aptString, int entrance = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return;
                var pm = PropertyManager.GetInstance();
                var propertyBuilding = (GarageBuilding)pm.GetProperty(aptId);
                if (propertyBuilding == null)
                    throw new Exception("Property building by id: " + aptId + " not found");
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));
                if (property == null)
                    throw new Exception("Property by id: " + aptString + " not found");
                var linkedEntrance = property.LinkedEntrances.ToArray()[entrance];


                var im = InteriorManager.GetInstance();
                var propertyInterior = im.GetInterior(property.Interior);
                var entryPoint = propertyInterior.EntrancePoints.FirstOrDefault(e => e.Id.Equals(linkedEntrance.Value));


                Game.PlayerPed.Position = entryPoint.Position;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
        }

        public static void ExitApartmentProperty(string aptId, string aptString, int exitId = 0)
        { 
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return;

                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (ApartmentBuilding)instance.GetProperty(aptId);
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));
                var linkedExit = property.LinkedExits.ToArray()[exitId];

                var exitPoint = propertyBuilding.ExitPoints.FirstOrDefault(e => e.Id.Equals(linkedExit.Value));
                Game.PlayerPed.Position = exitPoint.Position; 
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
        }

        public static void ExitProperty(string aptId, string aptString, int exitId = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return;

                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (GarageBuilding)instance.GetProperty(aptId);
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));
                var linkedExit = property.LinkedExits.ToArray()[exitId];

                var exitPoint = propertyBuilding.ExitPoints.FirstOrDefault(e => e.Id.Equals(linkedExit.Value));
                Game.PlayerPed.Position = exitPoint.Position;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
        }

        public static Vector3 GetEntrance(string aptId, int entranceId = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId))
                    return Vector3.One;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (GarageBuilding)instance.GetProperty(aptId);
                return propertyBuilding.Entrances[entranceId].DoorPosition;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static int GetNumOfEntrances(string aptId)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId))
                    return 0;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (GarageBuilding)instance.GetProperty(aptId);
                return propertyBuilding.Entrances.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }

        public static Vector3 GetExit(string aptId, string aptString, int exitId = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return Vector3.One;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (GarageBuilding)instance.GetProperty(aptId);
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));
                var linkedExit = property.LinkedExits.ToArray()[exitId];

                var im = InteriorManager.GetInstance();
                var propertyInterior = im.GetInterior(property.Interior);
                var entryPoint = propertyInterior.Exits[exitId];

                return entryPoint.DoorPosition;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static int GetNumOfExits(string aptId, string aptString)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return 0;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (GarageBuilding)instance.GetProperty(aptId);
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));


                var im = InteriorManager.GetInstance();
                var propertyInterior = im.GetInterior(property.Interior);

                return propertyInterior.Exits.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }


        public static Vector3 GetAptEntrance(string aptId, int entranceId = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId))
                    return Vector3.One;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (ApartmentBuilding)instance.GetProperty(aptId);  
                return propertyBuilding.Entrances[entranceId].DoorPosition;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static int GetAptNumOfEntrances(string aptId)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId))
                    return 0;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (ApartmentBuilding)instance.GetProperty(aptId);  
                return propertyBuilding.Entrances.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }

        public static Vector3 GetAptExit(string aptId, string aptString, int exitId = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return Vector3.One;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (ApartmentBuilding)instance.GetProperty(aptId);
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));
                var linkedExit = property.LinkedExits.ToArray()[exitId];

                var im = InteriorManager.GetInstance();
                var propertyInterior = im.GetInterior(property.Interior);
                var entryPoint = propertyInterior.Exits[exitId];

                return entryPoint.DoorPosition;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static int GetAptNumOfExits(string aptId, string aptString)
        {
            try
            {
                if (string.IsNullOrEmpty(aptId) || string.IsNullOrEmpty(aptString))
                    return 0;
                var instance = PropertyManager.GetInstance();
                var propertyBuilding = (ApartmentBuilding)instance.GetProperty(aptId);
                var property = propertyBuilding.Properties.FirstOrDefault(e => e.Id.Equals(aptString));


                var im = InteriorManager.GetInstance();
                var propertyInterior = im.GetInterior(property.Interior); 

                return propertyInterior.Exits.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }
    }
}
