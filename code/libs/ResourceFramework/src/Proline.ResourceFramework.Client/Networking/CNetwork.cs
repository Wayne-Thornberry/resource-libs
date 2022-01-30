using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public static class CNetwork
    {
        internal static void InvokeNetworkEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerServerEvent(eventName, new List<object>(args));
        }
    }
}
