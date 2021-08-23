using System;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

using Proline.Engine; 

using Proline.Core.Client;
using CitizenFX.Core.Native;
using Proline.Core.Client;
using Proline.Engine;
using CitizenFX.Core;
using System.Linq;

namespace Proline.Core.Client.Components.CScriptObjects
{
    public class X
    {
        public string ScriptName { get; set; }
        public float ActivationRange { get; set; }
        public bool ExecutedScript { get; set; }
        public int ScriptHandle { get; internal set; }
    }

    public class TrackedObject
    {
        public List<X> Scripts { get; set; }
        public int Handle { get; internal set; }
    }

    public class ScriptObjectScript : ComponentScript
    {
        private TrackedObjectsManager _tom;

        public override void Start()
        {
            _tom = TrackedObjectsManager.GetInstance();
        }
        public override void Update()
        {

        }

        public override void OnEngineEvent(string eventName, params object[] args)
        {
            if (eventName.Equals("entityTracked"))
            {
                var handle = (int)args[0];
                var hash = API.GetEntityModel(handle);
                var item = ScriptObjectsManager.GetScriptObjectPair(hash);
                if (item == null) return;
                if (hash != 0 && (hash == item.Hash))
                {
                    _tom.Add(handle, item);
                }
                //Debugger.LogDebug(handle);
            }
            else if (eventName.Equals("entityUntracked"))
            {
                var handle = (int)args[0];
                _tom.Remove(handle);
                //Debugger.LogDebug(handle);
            }
        }

        public override void FixedUpdate()
        {
            foreach (var trackedObject in _tom.GetTrackedObjects().ToArray())
            {
                var entity = Entity.FromHandle(trackedObject.Handle);
                if (entity == null)
                {
                    _tom.Remove(trackedObject.Handle);
                    continue;
                }

                if (entity.Exists())
                {
                    foreach (var item in trackedObject.Scripts)
                    {
                        var distance = World.GetDistance(Game.PlayerPed.Position, entity.Position);
                        var inRange = distance < item.ActivationRange;
                        //Debugger.LogDebug("Looking through the scripts " + distance);
                        //Debugger.LogDebug("Looking through the scripts " + inRange);
                        //Debugger.LogDebug("Looking through the scripts " + item.ExecutedScript);
                        if (!item.ExecutedScript && inRange)
                        {
                            Debugger.LogDebug("Object within activation range and has not executed the script, starting a script");
                            var handle = EngineAccess.StartNewScript(item.ScriptName, new object[1] { trackedObject.Handle });
                            item.ExecutedScript = true;
                            item.ScriptHandle = handle;
                        }
                    }
                }
            }
        }
    }
}
