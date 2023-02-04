using CCoreSystem;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CScriptObjs.Internal;


using Proline.Resource.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CScriptObjs.Tasks
{
    public class ProcessScriptObjs
    {
        private static Log _log = new Log();

        public ProcessScriptObjs()
        {
            _sm = ScriptObjectManager.GetInstance();
        }

        private ScriptObjectManager _sm;

        public async Task Execute()
        {
            var values = _sm.GetValues();
            if (values == null)
                return;
            var quew = new Queue<ScriptObject>(values);
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
            var entity = Entity.FromHandle(so.Handle);
            foreach (var item in so.Data)
            {
                if (IsEntityWithinActivationRange(entity, Game.PlayerPed, item.ActivationRange) && so.State == 0)
                {
                    _log.Debug(so.Handle + " Player is within range here, we should start the script and no longer track this for processing");
                    var api = new CCoreSystemAPI();
                    api.StartNewScript(item.ScriptName, so.Handle);
                    so.State = 1;
                    _sm.Remove(so.Handle);
                    return;
                }
            }
        }


        private bool IsEntityWithinActivationRange(Entity entity, Entity playerPed, float activationRange)
        {
            var pos = Game.PlayerPed.Position;
            var pos2 = entity.Position;
            return API.Vdist2(pos.X, pos.Y, pos.Z, pos2.X, pos2.Y, pos2.Z) <= activationRange;
        }



    }
}