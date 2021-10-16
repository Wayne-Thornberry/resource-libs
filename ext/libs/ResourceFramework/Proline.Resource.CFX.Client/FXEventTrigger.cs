using CitizenFX.Core;
using Proline.Resource.Common;
using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.CFX
{
    public class FXEventTrigger : IFXEvent
    {
        public void TriggerEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerEvent(eventName, args);
        }

        public void TriggerSidedEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerServerEvent(eventName, args);
        }

        public void TriggerLatentSidedEvent(string eventName, int bps, params object[] args)
        {
            BaseScript.TriggerLatentServerEvent(eventName, bps, args);
        }
    }
}
