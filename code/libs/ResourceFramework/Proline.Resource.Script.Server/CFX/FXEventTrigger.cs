using CitizenFX.Core;
using Proline.Resource.CFX;
using Proline.Resource.Common;
using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Script.CFX
{
    public class FXEventTrigger : CFXObject, IFXEventTrigger
    {

        public void TriggerEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerEvent(eventName, args);
        }

        public void TriggerSidedEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerClientEvent(eventName, args);
        }

        public void TriggerLatentSidedEvent(string eventName, int bps, params object[] args)
        {
            BaseScript.TriggerClientEvent(eventName, bps, args);
        }
    }
}
