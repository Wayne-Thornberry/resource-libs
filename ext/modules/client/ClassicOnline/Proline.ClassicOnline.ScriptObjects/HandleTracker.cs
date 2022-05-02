using System.Collections.Generic;

namespace Proline.ClassicOnline.MBrain
{
    internal class HandleTracker
    {
        private static HandleTracker _instance;

        private HashSet<int> _trackedHandles;

        public HandleTracker()
        {
            _trackedHandles = new HashSet<int>();
        }

        internal bool IsHandleTracked(int v)
        {
            return _trackedHandles.Contains(v);
        }

        internal IEnumerable<int> Get()
        {
            return _trackedHandles;
        }
        internal void Set(HashSet<int> handles)
        {
            _trackedHandles = handles;
        }

        internal static HandleTracker GetInstance()
        {
            if (_instance == null)
                _instance = new HandleTracker();
            return _instance;
        }

    }
}
