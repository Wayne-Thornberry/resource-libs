using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Events
{
    public partial class WriteFileAction : ExtendedEvent
    {

        public WriteFileAction() : base(SAVEFILEHANDLER, false)
        {

        }

        private static WriteFileAction _event;

        public const string SAVEFILEHANDLER = "SaveFileHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new WriteFileAction();
                _event.Subscribe();
            }
        }


        public static WriteFileAction TriggerEvent(string json)
        {
            var x = new WriteFileAction();
            x.Invoke(json);
            return x;
        }
    }
}
