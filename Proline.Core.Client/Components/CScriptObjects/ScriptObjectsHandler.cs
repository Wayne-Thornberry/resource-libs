
using Newtonsoft.Json;

using System;
using System.Threading.Tasks;
using System.Reflection;
using Proline.Engine;

using System.Collections.Generic; 
using Proline.Engine;
using CitizenFX.Core.Native;
using CitizenFX.Core;
using System.Linq;

namespace Proline.Freemode.Components.CScriptObjects
{
    public class ScriptObjectsHandler : ComponentHandler
    {
        private TrackedObjectsManager _tom;

        public override void OnInitialized()
        {

            var data = ResourceFile.Load( API.GetCurrentResourceName(), "data/scriptobjects.json");
            //Debugger.LogDebug(data);
            var scriptObjects = JsonConvert.DeserializeObject<ScriptObj>(data);

            ScriptObjectsManager.AddScriptObjectPairs(scriptObjects.scriptObjectPairs);

        }

        [ComponentEvent("entityTracked")]
        public void EntityTrackedHandler(params object[] args)
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
        [ComponentEvent("entityUntracked")]
        public void EntityUnTrackedHandler(params object[] args)
        {
            var handle = (int)args[0];
            _tom.Remove(handle);
            //Debugger.LogDebug(handle); 
        }

        public override void OnFixedUpdate()
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
                            var handle = StartNewScript(item.ScriptName, new object[1] { trackedObject.Handle });
                            item.ExecutedScript = true;
                            item.ScriptHandle = handle;
                        }
                    }
                }
            }
        }

        public override void OnStart()
        { 
            _tom = TrackedObjectsManager.GetInstance(); 
        } 
    }
}