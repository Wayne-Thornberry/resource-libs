using CitizenFX.Core.Native;
using CScriptObjs.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace CScriptObjs.Events
{
    public class CEventEntityBeginTracking
    {
        public void OnEventInvoked(object[] args)
        {
            //Console.WriteLine(this.GetType().Name + " Invoked");
            if (args == null || args.Length == 0)
                return;
            var handle = (int)args[0];

            var _sm = ScriptObjectManager.GetInstance();
            if (!API.DoesEntityExist(handle))
                return;
            var modelHash = API.GetEntityModel(handle);
            if (!_sm.ContainsSO(handle) && _sm.ContainsKey(modelHash))
            {
                Console.WriteLine(handle + " Oh boy, we found a matching script object with that model hash from that handle, time to track it");
                _sm.AddSO(handle, new ScriptObject()
                {
                    Data = _sm.Get(modelHash),
                    Handle = handle,
                    State = 0,
                });
            }
        }
    }
}
