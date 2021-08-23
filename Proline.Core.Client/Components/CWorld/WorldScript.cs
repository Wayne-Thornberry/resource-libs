using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine;
using Proline.Framework;
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
            HashSet<int> handles = new HashSet<int>();


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


            //foreach (var item in _ht.GetTrackedEntityHandles().ToArray())
            //{
            //    if (!API.DoesEntityExist(item))
            //    {
            //        _ht.UntrackEntityHandle(item);
            //        EngineAccess.TriggerEngineEvent("entityUntracked", true, item);
            //    }
            //}

            //foreach (var item in handles)
            //{
            //    if (!_ht.IsHandleTracked(item) && API.DoesEntityExist(item))
            //    {
            //       _ht.TrackEntityHandle(item);
            //        EngineAccess.TriggerEngineEvent("entityTracked", false, item);
            //    }
            //}
        }
    }
}
