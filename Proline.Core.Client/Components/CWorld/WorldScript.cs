using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CWorld
{
    public class WorldScript : ComponentScript
    {
        private HandleTracker _ht;

        public override void Start()
        {
            _ht = HandleTracker.GetInstance();
        }

        public override void FixedUpdate()
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
                    EngineAccess.TriggerEngineEvent("entityUntracked", item);
                    newEntities = true;
                }
            }

            foreach (var item in handles)
            {
                if (!_ht.IsHandleTracked(item) && API.DoesEntityExist(item))
                { 
                    EngineAccess.TriggerEngineEvent("entityTracked", item);
                    newEntities = true;
                }
            }

            if (newEntities)
            {
                _ht.Set(handles);
                EngineAccess.TriggerEngineEvent("entitiesInWorld", handles.ToArray()); 
            }
        }
    }
}
