using System.Collections.Generic;

namespace Proline.Core.Client.Components.CWorld
{ 
    internal class HandleTracker
    {
        private static HandleTracker _instance;

        private HashSet<int> _trackedHandles;

        public HandleTracker()
        {
            _trackedHandles = new HashSet<int>();
        }

        internal void UntrackEntityHandle(int v)
        {
            _trackedHandles.Remove(v);
        }

        internal bool IsHandleTracked(int v)
        {
            return _trackedHandles.Contains(v);
        }

        internal IEnumerable<int> GetTrackedEntityHandles()
        {
           return _trackedHandles;
        }

        internal void TrackEntityHandle(int v)
        {
            _trackedHandles.Add(v);
        }

        internal static HandleTracker GetInstance()
        {
            if (_instance == null)
                _instance = new HandleTracker();
            return _instance;
        }

    }
}
