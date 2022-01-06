using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Classic.Client.Engine.Components.CObjectTracker;
using Proline.Classic.Engine.Components.CScriptObjects;
using Proline.Component.Framework.Client.Access;
using Proline.Resource.Client.Component;
using Proline.Resource.Client.Eventing;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Memory;
using Proline.Resource.Client.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Client.Engine.Components.CBrain
{
    public class ObjectTrackerContext : ComponentContext
    {
        private List<int> _trackedObjects;

        public ObjectTrackerContext()
        {
            _trackedObjects = new List<int>();
        }

        public override void OnLoad()
        {
            MemoryCache.Cache("TrackedHandles", _trackedObjects);

        }

        public void StartNewScript(string name, params object[] args)
        {
            var exports = ExportManager.GetExports();
            _log.Debug(name);
            exports["Proline.Component.CScripting"].StartScript("Test");
        }


        public override async Task OnTick()
        {
            var oldHandles = new List<int>(_trackedObjects);
            var newHandles = new List<int>(HandleManager.EntityHandles);

            //StartNewScript("x"); 

            foreach (var handle in oldHandles)
            {
                // We know the object tracked and still exists
                if (newHandles.Contains(handle))
                {
                    newHandles.Remove(handle); // We subtrack the new handle that is already tracked, what we will have left at the end is new items that are untracked
                    //_log.Debug(handle + " Handle already tracked");
                    continue;
                }

                // We know the object is not tracked and doesnt exist anymore
                if (!API.DoesEntityExist(handle))
                {
                    _trackedObjects.Remove(handle);
                    //_log.Debug(handle + " Entity of handle does not exist anymore");
                    BaseScript.TriggerEvent("EntityHandleUnTracked", handle);
                    //_entityHandleUnTrackedHandler.Invoke(handle);
                }
            }

            foreach (var handle in newHandles)
            {
                _trackedObjects.Add(handle);
                // _log.Debug(handle + " New handle not tracked");
                BaseScript.TriggerEvent("EntityHandleTracked", handle);
               // _entityHandleTrackedHandler.Invoke(handle);
            }
            await BaseScript.Delay(1000); 
        }
    }
}
