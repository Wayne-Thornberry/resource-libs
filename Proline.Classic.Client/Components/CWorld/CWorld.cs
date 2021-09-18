using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core.Native;
using Proline.Classic.Managers;
using Proline.Engine.Componentry;
using Proline.Engine.Data;

namespace Proline.Classic.Components
{
    public class CWorld : ClientComponent
    {
        private HandleTracker _ht;
        
        protected override void OnInitialized()
        {
            if (EngineConfiguration.IsClient)
            { 
                _ht = HandleTracker.GetInstance();
            }
        }

        protected override void OnStart()
        {

        }

        //protected override void OnFixedUpdate()
        //{
        //    if (EngineConfiguration.IsClient)
        //    {
        //        var handles = new HashSet<int>();

        //        int entHandle = -1;
        //        int handle;

        //        handle = API.FindFirstPed(ref entHandle);
        //        handles.Add(entHandle);
        //        entHandle = -1;
        //        while (API.FindNextPed(handle, ref entHandle))
        //        {
        //            handles.Add(entHandle);
        //            entHandle = -1;
        //        }
        //        API.EndFindPed(handle);

        //        handle = API.FindFirstPickup(ref entHandle);
        //        handles.Add(entHandle);
        //        entHandle = -1;
        //        while (API.FindNextPickup(handle, ref entHandle))
        //        {
        //            handles.Add(entHandle);
        //            entHandle = -1;
        //        }
        //        API.EndFindPickup(handle);

        //        handle = API.FindFirstObject(ref entHandle);
        //        handles.Add(entHandle);
        //        entHandle = -1;
        //        while (API.FindNextObject(handle, ref entHandle))
        //        {
        //            handles.Add(entHandle);
        //            entHandle = -1;
        //        }
        //        API.EndFindObject(handle);

        //        handle = API.FindFirstVehicle(ref entHandle);
        //        handles.Add(handle);
        //        entHandle = -1;
        //        while (API.FindNextVehicle(handle, ref entHandle))
        //        {
        //            handles.Add(entHandle);
        //            entHandle = -1;
        //        }
        //        API.EndFindVehicle(handle);

        //        var newEntities = false;

        //        foreach (var item in _ht.Get())
        //        {
        //            if (!API.DoesEntityExist(item))
        //            {
        //                TriggerComponentEvent("EntityUntracked", item);
        //                newEntities = true;
        //            }
        //        }

        //        foreach (var item in handles)
        //        {
        //            if (!_ht.IsHandleTracked(item) && API.DoesEntityExist(item))
        //            {
        //                TriggerComponentEvent("EntityTracked", item);
        //                newEntities = true;
        //            }
        //        }

        //        if (newEntities)
        //        {
        //            _ht.Set(handles);
        //            TriggerComponentEvent("entitiesInWorld", handles.ToArray());
        //        }
        //    }
        //}

    }
}
