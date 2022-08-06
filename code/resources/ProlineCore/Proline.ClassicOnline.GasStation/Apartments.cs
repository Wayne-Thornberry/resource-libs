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

        public static void TeleportToAptInterior(string id, int exit = 0)
        {
            //{"X":-1409.06177,"Y":-557.61676,"Z":29.6644669,"IsNormalized":false,"IsZero":false}
            try
            {
                var instance =  InteriorManager.GetInstance();
                var propertyInterior = instance.GetInterior(id);
                if(propertyInterior == null)
                { 
                    MDebugAPI.LogError("No interior property matching id: " + id);
                    return;
                }

                var interiorJson = ResourceFile.Load("data/world/interiors/apartments.json");
                if (interiorJson == null)
                {
                    MDebugAPI.LogError("No interior data matching id: " + id);
                    return;
                }
                var interiors  = JsonConvert.DeserializeObject<List<Interior>>(interiorJson.Load());
                if (interiors == null)
                {
                    MDebugAPI.LogError("unable to load interiors: " + id);
                    return;
                }
                var interior = interiors.FirstOrDefault(e => e.Id.Equals(propertyInterior.Interior));
                if (interior == null)
                {
                    MDebugAPI.LogError("No could not find matching interior with id: " + propertyInterior.Interior);
                    return;
                }
                Game.PlayerPed.Position = interior.Exits[exit];
                CharacterGlobals.ActiveInterior = interior.Id;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
        }

        public static void TeleportToAptExterior(string id, int entrance = 0)
        {
            //{"X":-1409.06177,"Y":-557.61676,"Z":29.6644669,"IsNormalized":false,"IsZero":false}
            try
            {
                var instance = InteriorManager.GetInstance();
                var propertyInterior = instance.GetInterior(id);
                if (propertyInterior == null)
                {
                    MDebugAPI.LogError("No interior property matching id: " + id);
                    return;
                }

                var interiorJson = ResourceFile.Load("data/world/interiors/apartments.json");
                if (interiorJson == null)
                {
                    MDebugAPI.LogError("No interior data matching id: " + id);
                    return;
                }
                var interiors = JsonConvert.DeserializeObject<List<Interior>>(interiorJson.Load());
                if (interiors == null)
                {
                    MDebugAPI.LogError("unable to load interiors: " + id);
                    return;
                }
                var interior = interiors.FirstOrDefault(e => e.Id.Equals(propertyInterior.Interior));
                if (interior == null)
                {
                    MDebugAPI.LogError("No could not find matching interior with id: " + propertyInterior.Interior);
                    return;
                }
                Game.PlayerPed.Position = interior.Entrances[entrance];
                CharacterGlobals.ActiveInterior = null;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
        }
        public static Vector3 GetAptExit(int exit = 0)
        {
            try
            {
                var interiorJson = ResourceFile.Load("data/world/interiors/apartments.json");
                var interiors = JsonConvert.DeserializeObject<List<Interior>>(interiorJson.Load());
                var interior = interiors.FirstOrDefault(e => e.Id.Equals(CharacterGlobals.ActiveInterior));
                return interior.Exits[exit];
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }

        public static int GetAptNumOfExits()
        {
            try
            {
                var interiorJson = ResourceFile.Load("data/world/interiors/apartments.json");
                var interiors = JsonConvert.DeserializeObject<List<Interior>>(interiorJson.Load());
                var interior = interiors.FirstOrDefault(e => e.Id.Equals(CharacterGlobals.ActiveInterior));
                return interior.Exits.Count;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return 0;
        }
    }
}
