using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MBrain.Events;
using Proline.ClassicOnline.MBrain.Entity;
using Proline.ClassicOnline.MScripting;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Scripts
{
    internal class ObjectProcesser : ModuleScript
    {
        private ScriptObjectManager _sm;

        public override async Task OnStart()
        {
            _sm = ScriptObjectManager.GetInstance();
            var onNewHandlesFound = new OnNewHandlesFound();
            var onEntityUntracked = new OnEntityUntracked();

            EventHandlers.Add("EntityHandlesTracked", new Action<List<object>>(onNewHandlesFound.OnEventInvoked));
            EventHandlers.Add("EntityHandlesUnTracked", new Action<List<object>>(onEntityUntracked.OnEventInvoked));
        }

        public override async Task OnUpdate()
        {
            ProcessScriptObjects();
            await Delay(10);
        }
        private void ProcessScriptObjects()
        {
            var quew = new Queue<ScriptObject>(_sm.GetValues());
            while (quew.Count > 0)
            {
                var so = quew.Dequeue();
                ProcessScriptObject(so);
            }
        }
        private void ProcessScriptObject(ScriptObject so)
        {
            if (!API.DoesEntityExist(so.Handle))
            {
                _sm.Remove(so.Handle);
                return;
            }
            var entity = CitizenFX.Core.Entity.FromHandle(so.Handle);
            foreach (var item in so.Data)
            {
                if (IsEntityWithinActivationRange(entity, Game.PlayerPed, item.ActivationRange) && so.State == 0)
                {
                    _log.Debug(so.Handle + " Player is within range here, we should start the script and no longer track this for processing");
                    Script.StartNewScript(item.ScriptName, so.Handle);
                    so.State = 1;
                    _sm.Remove(so.Handle);
                    return;
                }
            }
        }


        private bool IsEntityWithinActivationRange(CitizenFX.Core.Entity entity, CitizenFX.Core.Entity playerPed, float activationRange)
        {
            var pos = Game.PlayerPed.Position;
            var pos2 = entity.Position;
            return API.Vdist2(pos.X, pos.Y, pos.Z, pos2.X, pos2.Y, pos2.Z) <= activationRange;
        }
    }
}
