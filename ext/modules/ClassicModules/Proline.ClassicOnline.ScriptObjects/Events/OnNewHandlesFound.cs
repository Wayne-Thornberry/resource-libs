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
        public void OnEventInvoked(int handles)
        {
            try
            {
                var sm = ScriptObjectManager.GetInstance();
                if (!API.DoesEntityExist(handles)) return;
                var modelHash = API.GetEntityModel(handles);
                if (!sm.ContainsSO(handles) && sm.ContainsKey(modelHash))
                {
                    _log.Debug(handles + " Oh boy, we found a matching script object with that model hash from that handle, time to track it");
                    sm.AddSO(handles, new ScriptObject()
                    {
                        Data = sm.Get(modelHash),
                        Handle = handles,
                        State = 0,
                    });
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
