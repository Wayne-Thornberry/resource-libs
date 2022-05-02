using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Networking
{
    public interface IEventMethods
    {
        void TriggerServerEvent(string eventName, params object[] args);
        void TriggerClientEvent(int playerId, string eventName, params object[] args);
        void TriggerClientEvent(string eventName, params object[] args);
        void TriggerEvent(string eventName, params object[] args);
    }
}
