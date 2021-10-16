using CitizenFX.Core;
using Proline.Game.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CScriptObjects
{
    public class ScriptObjectsScript : ComponentScript
    {
        private TrackedObjectsManager _tom;

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
                        var distance = World.GetDistance(CitizenFX.Core.Game.PlayerPed.Position, entity.Position);
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
