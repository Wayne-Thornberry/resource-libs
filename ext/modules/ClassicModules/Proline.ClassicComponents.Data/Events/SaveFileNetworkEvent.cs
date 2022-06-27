using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Events
{
    public partial class SaveFileNetworkEvent : ExtendedEvent
    {
        private static SaveFileNetworkEvent _event;

        public const string SAVEFILEHANDLER = "SaveFileHandler";

        public static SaveFileNetworkEvent SubscribeEvent()
        {
            if (_event == null)
                _event = CreateEvent();
            _event.Subscribe();
            return _event;
        }

        public static SaveFileNetworkEvent CreateEvent()
        { 
            return new SaveFileNetworkEvent();
        }

#if CLIENT
        public static SaveFileNetworkEvent TriggerEvent(string json)
        {
            var x = CreateEvent();
            x.Invoke(json);
            return x;
        }

#elif SERVER
        public static SaveFileNetworkEvent TriggerEvent(Player player, long id)
        {
            var x = CreateEvent();
            x.Invoke(player, id);
            return x;
        }
#endif
    }
}
