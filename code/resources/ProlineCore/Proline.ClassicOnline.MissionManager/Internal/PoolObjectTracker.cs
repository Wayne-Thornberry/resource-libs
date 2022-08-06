using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MissionManager.Internal
{
    internal class PoolObjectTracker
    {
        private static PoolObjectTracker _instance;
        private List<PoolObject> _list;

        public PoolObjectTracker()
        { 
            if (_list == null)
                _list = new List<PoolObject>();
        }

        internal bool ContainsPoolObject(PoolObject obj)
        {
            if (_list == null)
                return false;
            return _list.Contains(obj);
        }

        public static PoolObjectTracker GetInstance()
        {
            if (_instance == null)
                _instance = new PoolObjectTracker();
            return _instance;
        }

        internal void DeleteAllPoolObjects()
        {
            if (_list == null)
                return;
            foreach (var poolObj in GetPoolObjects())
            {
                poolObj.Delete();
            }
        }

        internal void TrackPoolObject(PoolObject obj)
        {
            if (_list == null)
                _list = new List<PoolObject>();
            if (_list.Contains(obj))
                return;
            _list.Add(obj);
        }
        
        internal IEnumerable<PoolObject> GetPoolObjects()
        {
            if (_list == null)
                return null;
            return _list;
        }

        internal bool IsTrackingObjects()
        {
            if (_list == null)
                return false;
            return _list.Count > 0;
        }

        internal void ClearPoolObjects()
        {
            if (_list == null)
                return;
            _list.Clear();
        }
    }
}
