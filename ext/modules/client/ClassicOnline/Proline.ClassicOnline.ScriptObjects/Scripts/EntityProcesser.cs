using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MBrain;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Scripts
{
    internal class EntityProcesser : ModuleScript
    {
        private HashSet<int> _trackedHandles;
        private HandleTracker _ht;

        public EntityProcesser()
        {
            _trackedHandles = new HashSet<int>();
            _ht = HandleTracker.GetInstance();
        }

        public override async Task OnUpdate()
        {
            var entityHandles = new Queue<int>(HandleManager.EntityHandles);
            var addedHandles = new List<object>();
            var removedHanldes = new List<object>();

            while (entityHandles.Count > 0)
            {
                var handle = entityHandles.Dequeue();
                if (_trackedHandles.Contains(handle))
                    continue;
                _trackedHandles.Add(handle);
                addedHandles.Add(handle);
                _ht.Add(handle); 
            }

            var combinedHandles = new Queue<int>(_trackedHandles);
            while (combinedHandles.Count > 0)
            {
                var handle = combinedHandles.Dequeue();
                if (API.DoesEntityExist(handle))
                    continue;
                _trackedHandles.Remove(handle);
                removedHanldes.Add(handle);
                _ht.Remove(handle); 
            }
            await Delay(1000);
        }
    }
}
