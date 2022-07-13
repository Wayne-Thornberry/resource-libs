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

        public static void SubscribeEvent()
        {
            if (_event == null)
            { 
                _event = new SaveFileNetworkEvent();
                _event.Subscribe();
            } 
        }


        public static SaveFileNetworkEvent TriggerEvent(string json)
        {
            var x = new SaveFileNetworkEvent();
            x.Invoke(json);
            return x;
        } 
    }
}
