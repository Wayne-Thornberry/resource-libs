using CPoolObjects.Internal;
using Proline.ClassicOnline.EventQueue;
using Proline.Resource.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPoolObjects.Tasks
{
    public class PoolObjectTracker
    {
        private static Log _log = new Log();

        public PoolObjectTracker()
        {
            _trackedHandles = new HashSet<int>();
        }
        private HashSet<int> _trackedHandles;

        public async Task Execute()
        {
            var oldHandles = _trackedHandles;
            _trackedHandles = new HashSet<int>();
            var currentHandles = ProcessWorldPoolItems();

            var addedHandles = currentHandles.Except(oldHandles);
            var removedHandles = oldHandles.Except(currentHandles);

            ProcessAddedHandles(addedHandles);
            ProcessOldHandles(removedHandles);
            var x = addedHandles.Count();
            var y = removedHandles.Count();
            //if(x != 0 || y != 0)
            //    CDebugActions.CDebugActionsAPI.LogDebug($"Added Handles {x} Removed Handles {y}");

            _trackedHandles = currentHandles;
            PoolObjectManager.TrackedHandles = _trackedHandles.ToArray();
        }

        private void ProcessAddedHandles(IEnumerable<int> handles)
        {
            foreach (var item in handles)
            {
                ComponentEvent.InvokeEvent("CEventEntityBeginTracking", item);
                //CDebugActions.CDebugActionsAPI.LogDebug($"Added Handle {item} Found, Exists: {API.DoesEntityExist(item)}");
            }
        }

        private void ProcessOldHandles(IEnumerable<int> handles)
        {
            foreach (var item in handles)
            {
                ComponentEvent.InvokeEvent("CEventEntityEndTracking", item);
                //CDebugActions.CDebugActionsAPI.LogDebug($"Removed Handle {item} Found, Exists: {API.DoesEntityExist(item)}");
            }
        }

        private HashSet<int> ProcessWorldPoolItems()
        {
            var pickupHanldeFinder = new PickupHandleFinder();
            var objectHanldeFinder = new ObjHandleFinder();
            var vehicleHanldeFinder = new VehicleHandleFinder();
            var pedHanldeFinder = new PedHandleFinder();
            var handles = new HashSet<int>();

            while (pickupHanldeFinder.FindNextProp(out int handle))
            {
                handles.Add(handle);
            }

            while (objectHanldeFinder.FindNextProp(out int handle))
            {
                handles.Add(handle);
            }

            while (vehicleHanldeFinder.FindNextProp(out int handle))
            {
                handles.Add(handle);
            }

            while (pedHanldeFinder.FindNextProp(out int handle))
            {
                handles.Add(handle);
            }
            return handles;
        }
    }
}