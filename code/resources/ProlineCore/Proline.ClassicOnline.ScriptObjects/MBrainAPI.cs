using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.GScripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain
{
    public static class MBrainAPI
    {
        public static int[] GetEntityHandlesByTypes(EntityType type)
        {
            try
            {
                var array = new List<int>();
                int entHandle = -1;
                int handle;
                switch (type)
                {
                    case EntityType.PROP:
                        handle = API.FindFirstObject(ref entHandle);
                        array.Add(entHandle);
                        entHandle = -1;
                        while (API.FindNextObject(handle, ref entHandle))
                        {
                            array.Add(entHandle);
                            entHandle = -1;
                        }
                        API.EndFindObject(handle);
                        break;
                    case EntityType.PED:
                        handle = API.FindFirstPed(ref entHandle);
                        array.Add(entHandle);
                        entHandle = -1;
                        while (API.FindNextPed(handle, ref entHandle))
                        {
                            array.Add(entHandle);
                            entHandle = -1;
                        }

                        API.EndFindPed(handle);
                        break;
                    case EntityType.VEHICLE:
                        handle = API.FindFirstVehicle(ref entHandle);
                        array.Add(entHandle);
                        entHandle = -1;
                        while (API.FindNextVehicle(handle, ref entHandle))
                        {
                            array.Add(entHandle);
                            entHandle = -1;
                        }

                        API.EndFindVehicle(handle);
                        break;
                }
                return array.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        public static CitizenFX.Core.Entity GetNeariestEntity(EntityType type)
        {
            try
            {
                CitizenFX.Core.Entity _entity = null;
                var _closestDistance = 99999f;
                var handles = GetEntityHandlesByTypes(type);
                foreach (var item in handles)
                {
                    var entity = CitizenFX.Core.Entity.FromHandle(item);
                    var distance = World.GetDistance(entity.Position, Game.PlayerPed.Position);
                    if (distance < _closestDistance)
                    { 
                        MDebug.MDebugAPI.LogDebug("Found a vehicle");
                        _entity = entity;
                        _closestDistance = distance;
                    }
                }
                return _entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
