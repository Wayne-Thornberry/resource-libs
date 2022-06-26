using CitizenFX.Core;
using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using System.Threading.Tasks;
using Console = Proline.Resource.Console; 

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {
        private static LoadFileNetworkEvent _event;

        public const string LOADFILEHANDLER = "LoadFileHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            { 
                _event = CreateEvent();
                _event.Subscribe();
            } 
        }

        public static LoadFileNetworkEvent CreateEvent()
        {
            return new LoadFileNetworkEvent();
        }

#if CLIENT
        public static LoadFileNetworkEvent TriggerEvent(long id)
        {
            var x = CreateEvent();
            x.Invoke(id);
            return x;
        }

#elif SERVER
        public static LoadFileNetworkEvent TriggerEvent(Player player, long id)
        {
            var x = CreateEvent();
            x.Invoke(player, id);
            return x;
        }
#endif
    }
}
