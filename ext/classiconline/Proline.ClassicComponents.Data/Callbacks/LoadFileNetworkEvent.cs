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
                _event = new LoadFileNetworkEvent();
                _event.Subscribe();
            } 
        }

        public static void UnsubscribeEvent()
        {
            if (_event != null)
            {
                _event.Unsubscribe();
                _event = null;
            }
        }
         
    }
}
