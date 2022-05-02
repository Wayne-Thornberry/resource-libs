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
        public void OnEventInvoked(List<object> handles)
        {
            try
            {
                var sm = ScriptObjectManager.GetInstance();
                foreach (int handle in handles)
                {
                    if (API.DoesEntityExist(handle)) return;
                    var modelHash = API.GetEntityModel(handle);
                    if (!sm.ContainsKey(modelHash)) return;
                    if (sm.ContainsKey(handle))
                        sm.Remove(handle);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                throw;
            }
        }
    }
}
