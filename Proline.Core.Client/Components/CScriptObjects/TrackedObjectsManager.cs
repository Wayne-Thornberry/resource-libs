using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CScriptObjects
{
    internal class TrackedObjectsManager
    {
        private static TrackedObjectsManager _instance;
        private Dictionary<int, TrackedObject> _trackedObjects;

        TrackedObjectsManager()
        {
            _trackedObjects = new Dictionary<int, TrackedObject>();
        }

        internal static TrackedObjectsManager GetInstance()
        {
            if (_instance == null)
                _instance = new TrackedObjectsManager();
            return _instance;
        }

        internal void Add(int handle, SOP item)
        {
            Debugger.LogDebug("Found an object that should start a script, monitoring it");
            var x = new List<X>();
            foreach (var i in item.Pairs)
            {
                Debugger.LogDebug("ddsadadad it");
                x.Add(new X() { ActivationRange = 5f, ExecutedScript = false, ScriptHandle = -1, ScriptName = i.ScriptName });
            }
            _trackedObjects.Add(handle, new TrackedObject() { Handle = handle, Scripts = x });
        }

        internal TrackedObject Get(int handle)
        {
            if (_trackedObjects.ContainsKey(handle))
            {
                return _trackedObjects[handle];
            }
            return null;
        }

        internal void Remove(int handle)
        {
            if (_trackedObjects.ContainsKey(handle))
            {
                Debugger.LogDebug("tracked object no longer exists, this object should be untracked");
                _trackedObjects.Remove(handle);
            }
        }

        internal IEnumerable<TrackedObject> GetTrackedObjects()
        {
            return _trackedObjects.Values;
        }
    }
}
