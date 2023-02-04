using CDebugActions;
using CGameLogic.Data;
using CitizenFX.Core;
using CitizenFX.Core.Native;

using System;
using System.Collections.Generic;
using Console = Proline.Resource.Console;

namespace CScriptObjs
{
    public class CCScriptObjsAPI : ICScriptObjsAPI
    {
        public int[] GetEntityHandlesByTypes(EntityType type)
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

        public Entity GetNeariestEntity(EntityType type)
        {
            var api = new CDebugActionsAPI();
            try
            {
                Entity _entity = null;
                var _closestDistance = 99999f;
                var handles = GetEntityHandlesByTypes(type);
                foreach (var item in handles)
                {
                    var entity = Entity.FromHandle(item);
                    var distance = World.GetDistance(entity.Position, Game.PlayerPed.Position);
                    if (distance < _closestDistance)
                    {
                        api.LogDebug("Found a vehicle");
                        _entity = entity;
                        _closestDistance = distance;
                    }
                }
                return _entity;
            }
            catch (Exception e)
            {
                api.LogDebug(e);
            }
            return null;
        }
    }
}
