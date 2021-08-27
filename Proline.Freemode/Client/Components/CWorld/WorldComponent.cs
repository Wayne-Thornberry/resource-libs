
extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;


using Proline.Freemode;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CWorld
{
    public class WorldComponent : EngineComponent
    {
        private HandleTracker _ht;


        [Client]
        [ComponentAPI]
        public void SetPlayerAsPartOfPoliceGroup()
        {
            var vehicles = World.GetAllPeds();
            var entity = World.GetClosest<Ped>(Game.PlayerPed.Position, vehicles.ToArray());
            LogDebug(entity.PedGroup);
            var id = API.GetPedGroupIndex(entity.Handle);
            LogDebug(id);
            API.SetPedAsGroupMember(Game.PlayerPed.Handle, id);
            LogDebug("ids");
        }

        [Client]
        [ComponentAPI]
        public void UnlockNeareastVehicle()
        {
            var vehicles = World.GetAllVehicles();
            var entity = World.GetClosest<Vehicle>(Game.PlayerPed.Position, vehicles.ToArray());
            LogDebug(entity.Handle);

            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_NEEDS_TO_BE_HOTWIRED, entity.Handle, false );
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_IS_WANTED, entity.Handle, false );
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_IS_STOLEN, entity.Handle, false );
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_DOORS_LOCKED, entity.Handle, 1 );
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_DOORS_LOCKED_FOR_ALL_PLAYERS, entity.Handle, false );
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_HAS_BEEN_DRIVEN_FLAG, entity.Handle, true );
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_HAS_BEEN_OWNED_BY_PLAYER, entity.Handle, true );
            LogDebug("Donedsad");
        }

        [Client]
        [ComponentAPI]
        public void GetNearbyEntities(out int[] entities)
        {
            var _ht = HandleTracker.GetInstance();
            entities = _ht.Get().ToArray();
        }
        [ComponentCommand("DoSomething")]
        public void DoSomething()
        {
            LogDebug("It worked!");
        }

        protected override void OnInitialized()
        {
            if (EngineService.IsClient)
            { 
                _ht = HandleTracker.GetInstance();
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnFixedUpdate()
        {
            if (EngineService.IsClient)
            {
                var handles = new HashSet<int>();

                int entHandle = -1;
                int handle;

                handle = API.FindFirstPed(ref entHandle);
                handles.Add(entHandle);
                entHandle = -1;
                while (API.FindNextPed(handle, ref entHandle))
                {
                    handles.Add(entHandle);
                    entHandle = -1;
                }
                API.EndFindPed(handle);

                handle = API.FindFirstPickup(ref entHandle);
                handles.Add(entHandle);
                entHandle = -1;
                while (API.FindNextPickup(handle, ref entHandle))
                {
                    handles.Add(entHandle);
                    entHandle = -1;
                }
                API.EndFindPickup(handle);

                handle = API.FindFirstObject(ref entHandle);
                handles.Add(entHandle);
                entHandle = -1;
                while (API.FindNextObject(handle, ref entHandle))
                {
                    handles.Add(entHandle);
                    entHandle = -1;
                }
                API.EndFindObject(handle);

                handle = API.FindFirstVehicle(ref entHandle);
                handles.Add(handle);
                entHandle = -1;
                while (API.FindNextVehicle(handle, ref entHandle))
                {
                    handles.Add(entHandle);
                    entHandle = -1;
                }
                API.EndFindVehicle(handle);

                var newEntities = false;

                foreach (var item in _ht.Get())
                {
                    if (!API.DoesEntityExist(item))
                    {
                        TriggerComponentEvent("EntityUntracked", item);
                        newEntities = true;
                    }
                }

                foreach (var item in handles)
                {
                    if (!_ht.IsHandleTracked(item) && API.DoesEntityExist(item))
                    {
                        TriggerComponentEvent("EntityTracked", item);
                        newEntities = true;
                    }
                }

                if (newEntities)
                {
                    _ht.Set(handles);
                    TriggerComponentEvent("entitiesInWorld", handles.ToArray());
                }
            }
        }

    }
}
