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
    internal class OnNewHandlesFound
    {
        private static Log _log = new Log();
        public void OnEventInvoked(List<object> handles)
        {
            try
            {
                var sm = ScriptObjectManager.GetInstance();

                foreach (int handle in handles)
                {
                    if (!API.DoesEntityExist(handle)) return;
                    var modelHash = API.GetEntityModel(handle);
                    if (!sm.ContainsSO(handle) && sm.ContainsKey(modelHash))
                    {
                        _log.Debug(handle + " Oh boy, we found a matching script object with that model hash from that handle, time to track it");
                        sm.AddSO(handle, new ScriptObject()
                        {
                            Data = sm.Get(modelHash),
                            Handle = handle,
                            State = 0,
                        });
                    }
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
