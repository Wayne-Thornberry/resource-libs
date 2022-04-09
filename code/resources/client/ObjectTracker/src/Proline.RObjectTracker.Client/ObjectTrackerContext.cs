using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Classic.Client.Engine.Components.CObjectTracker;
using Proline.Classic.Engine.Components.CScriptObjects;
using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Client.Engine.Components.CBrain
{
    public class ObjectTrackerContext : ComponentContext
    {
        private HashSet<int> _trackedHandles;

        public ObjectTrackerContext()
        {
            _trackedHandles = new HashSet<int>();
        }

        public override async Task OnTick()
        {
            var entityHandles = new Queue<int>(HandleManager.EntityHandles);
            var addedHandles = new List<object>();
            var removedHanldes = new List<object>();

            while(entityHandles.Count > 0)
            {
                var handle = entityHandles.Dequeue();
                if (_trackedHandles.Contains(handle))
                    continue;
                _trackedHandles.Add(handle);
                addedHandles.Add(handle);
               // EventManager.InvokeEventV2("EntityHandleTracked", handle);
            }

            var combinedHandles = new Queue<int>(_trackedHandles); 
            while (combinedHandles.Count > 0)
            {
                var handle = combinedHandles.Dequeue();
                if (API.DoesEntityExist(handle)) 
                    continue;
                _trackedHandles.Remove(handle);
                removedHanldes.Add(handle);
                //EventManager.InvokeEventV2("EntityHandleUnTracked", handle);
            }
            EventManager.InvokeEventV2("EntityHandlesTracked", addedHandles);
            await BaseScript.Delay(100);
            EventManager.InvokeEventV2("EntityHandlesUnTracked", removedHanldes);
            await BaseScript.Delay(900);
        }
    }
}
