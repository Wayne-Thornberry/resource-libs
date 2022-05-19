using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MBrain.Entity
{
    internal class HandleTracker
    {
        private static HandleTracker _instance;

        private HashSet<int> _trackedHandles;

        public delegate void EntityHandleTrackedDelegate(int handle);
        public delegate void EntityHandleUnTrackedDelegate(int handle);
        public event EntityHandleTrackedDelegate EntityHandleTracked;
        public event EntityHandleUnTrackedDelegate EntityHandleUntracked;

        public HandleTracker()
        {
            _trackedHandles = new HashSet<int>();
        }

        internal bool IsHandleTracked(int v)
        {
            return _trackedHandles.Contains(v);
        }

        internal void Add(int handl)
        {
            _trackedHandles.Add(handl);
        }

        internal void Remove(int handl)
        {
            _trackedHandles.Remove(handl);
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
