using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Classic.Data;
using Proline.Classic.Managers;
using Proline.Engine;
using Proline.Engine.Componentry;
using Proline.Engine.Data;
using Proline.Engine.Eventing;

namespace Proline.Classic.Components
{
    public class CScriptObjects : ClientComponent
    {
        private TrackedObjectsManager _tom;


        protected override void OnInitialized()
        {

            if (EngineConfiguration.IsClient)
            {
                var data = Resources.LoadFile(API.GetCurrentResourceName(), "data/scriptobjects.json");
                //LogDebug(data);
                var scriptObjects = JsonConvert.DeserializeObject<ScriptObj>(data);

                ScriptObjectsManager.AddScriptObjectPairs(scriptObjects.scriptObjectPairs);
            }

        }

        //[ComponentEvent("entityTracked")]
        //public void EntityTracked(int handle)
        //{

        //    var hash = API.GetEntityModel(handle);
        //    var item = ScriptObjectsManager.GetScriptObjectPair(hash);
        //    if (item == null) return;
        //    if (hash != 0 && (hash == item.Hash))
        //    {
        //        _tom.Add(handle, item);
        //    }
        //    //LogDebug(handle);
        //}
        //[ComponentEvent("entityUntracked")]
        //public void EntityUntracked(int handle)
        //{
        //    _tom.Remove(handle);
        //    //LogDebug(handle); 
        //}

        //protected override void OnFixedUpdate()
        //{
        //    if (EngineConfiguration.IsClient)
        //    {
        //        foreach (var trackedObject in _tom.GetTrackedObjects().ToArray())
        //        {
        //            var entity = Entity.FromHandle(trackedObject.Handle);
        //            if (entity == null)
        //            {
        //                _tom.Remove(trackedObject.Handle);
        //                continue;
        //            }

        //            if (entity.Exists())
        //            {
        //                foreach (var item in trackedObject.Scripts)
        //                {
        //                    var distance = World.GetDistance(Game.PlayerPed.Position, entity.Position);
        //                    var inRange = distance < item.ActivationRange;
        //                    //LogDebug("Looking through the scripts " + distance);
        //                    //LogDebug("Looking through the scripts " + inRange);
        //                    //LogDebug("Looking through the scripts " + item.ExecutedScript);
        //                    if (!item.ExecutedScript && inRange)
        //                    {
        //                        LogDebug("Object within activation range and has not executed the script, starting a script");
        //                        StartNewScript(item.ScriptName, new object[1] { trackedObject.Handle });
        //                        item.ExecutedScript = true;
        //                        item.ScriptHandle = 2;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected override void OnStart()
        {
            if (EngineConfiguration.IsClient)
            {
                _tom = TrackedObjectsManager.GetInstance();
            }
        } 
    }
}