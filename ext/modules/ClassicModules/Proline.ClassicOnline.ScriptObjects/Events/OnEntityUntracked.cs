using CitizenFX.Core.Native;
using Proline.ClassicOnline.MBrain.Entity;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Events
{
    internal class OnEntityUntracked
    {
        private static Log _log = new Log();
        public void OnEventInvoked(int handles)
        {
            try
            {
                var sm = ScriptObjectManager.GetInstance();
                if (API.DoesEntityExist(handles)) return;
                var modelHash = API.GetEntityModel(handles);
                if (!sm.ContainsKey(modelHash)) return;
                if (sm.ContainsKey(handles))
                    sm.Remove(handles);
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                throw;
            }
        }
    }
}
