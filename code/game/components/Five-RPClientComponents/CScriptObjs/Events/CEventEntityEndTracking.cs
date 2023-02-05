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
    public class CEventEntityEndTracking
    {
        public void OnEventInvoked(object[] args)
        {
            //Console.WriteLine(this.GetType().Name + " Invoked");
            if (args == null || args.Length == 0)
                return;
            var handle = (int)args[0];

            var _sm = ScriptObjectManager.GetInstance();

            if (API.DoesEntityExist(handle))
                return;
            var modelHash = API.GetEntityModel(handle);
            if (!_sm.ContainsKey(modelHash))
                return;
            if (_sm.ContainsKey(handle))
                _sm.Remove(handle);
        }
    }
}
