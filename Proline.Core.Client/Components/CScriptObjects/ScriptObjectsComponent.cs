
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
    public class ScriptObjectsComponent : EngineComponent
    {
        private TrackedObjectsManager _tom;

        [Client]
        [ComponentAPI]
        public void StartNewEntityScript(string scriptName, int handle, params object[] param)
        {
            var args = new List<object>(param);
            args.Add(handle);
            args.AddRange(param);
            StartNewScript(scriptName, args.ToArray());
        }

        [Client]
        [ComponentAPI]
        public bool IsEntityInActivationRange(int entityHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            if (tracted == null) return false;
            return tracted.Scripts.Select(e => e.ActivationRange).First() < 10f;
        }

        [Client]
        [ComponentAPI]
        public bool IsEntityScripted(int entityHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            return tracted != null;
        }

        [Client]
        [ComponentAPI]
        public int[] GetScriptHandlesFromEntity(int entityHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            if (tracted == null) return new int[0];
            return tracted.Scripts.Select(e => e.ScriptHandle).ToArray();
        }

        protected override void OnInitialized()
        {

            var data = ResourceFile.Load( API.GetCurrentResourceName(), "data/scriptobjects.json");
            //LogDebug(data);
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
            //LogDebug(handle);
        }
        [ComponentEvent("entityUntracked")]
        public void EntityUnTrackedHandler(params object[] args)
        {
            var handle = (int)args[0];
            _tom.Remove(handle);
            //LogDebug(handle); 
        }

        protected override void OnFixedUpdate()
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
                        //LogDebug("Looking through the scripts " + distance);
                        //LogDebug("Looking through the scripts " + inRange);
                        //LogDebug("Looking through the scripts " + item.ExecutedScript);
                        if (!item.ExecutedScript && inRange)
                        {
                            LogDebug("Object within activation range and has not executed the script, starting a script");
                            StartNewScript(item.ScriptName, new object[1] { trackedObject.Handle });
                            item.ExecutedScript = true;
                            item.ScriptHandle = 2;
                        }
                    }
                }
            }
        }

        protected override void OnStart()
        { 
            _tom = TrackedObjectsManager.GetInstance(); 
        } 
    }
}