



using Proline.Freemode;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;

namespace Proline.Freemode.Components.CWorld
{
    public class WorldComponent : EngineComponent
    {
        private HandleTracker _ht;
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
            Console.WriteLine("World Component Initialized");
            _ht = HandleTracker.GetInstance();
        }

        protected override void OnStart()
        {
            Console.WriteLine("World Component Started");
        }
        protected override void OnFixedUpdate()
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
