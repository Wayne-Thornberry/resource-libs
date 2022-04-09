using CitizenFX.Core;
using Proline.ResourceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.APIs
{
    public class EventAPI : IFiveAPI, IEventMethods
    {
        public void TriggerServerEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerServerEvent(eventName, args);
        }

        public void TriggerClientEvent(int playerId, string eventName, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void TriggerClientEvent(string eventName, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void TriggerEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerEvent(eventName, args);
        }
    }
}
