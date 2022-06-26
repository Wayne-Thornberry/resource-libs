using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using Console = Proline.Resource.Console;
using Newtonsoft.Json;

namespace Proline.ClassicOnline.MData.Events
{ 
    internal class SaveFileEvent : CallbackEvent // behind the scenes this is a Subscriber
    { 
        public SaveFileEvent() : base(EventHandlerNames.SAVEFILEHANDLER)
        {
            
        }

        public override object OnEventCallback(params object[] args)
        {
            var obj = int.Parse(args[0].ToString());
            var fm = SaveFileManager.GetInstance();
            Console.WriteLine("data got " + obj);
            fm.IsSaveInProgress = false;
            fm.LastSaveResult = obj;
            return 0;
        }

        public static async Task Execute(params object[] args)
        {
            var callbackEvent = new SaveFileEvent(); 
            await callbackEvent.TriggerEventAsync(args);
        } 

    }
}

