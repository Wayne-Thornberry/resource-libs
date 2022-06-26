using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using Console = Proline.Resource.Console;
using Newtonsoft.Json;
using CitizenFX.Core.Native;

namespace Proline.ClassicOnline.MData.Events
{ 
    internal class LoadFileEvent : CallbackEvent // behind the scenes this is a Subscriber
    { 
        public LoadFileEvent() : base(EventHandlerNames.LOADFILEHANDLER)
        {
            
        }

        public override object OnEventCallback(params object[] args)
        { 
            var obj = args[0].ToString();
            if (string.IsNullOrEmpty(obj)) return null;
            var instance = SaveFileManager.GetInstance();
            Console.WriteLine("data got " + obj);
            instance.PutFileIntoMemory(obj);
            return 1;
        }

        public static async Task Execute(params object[] args)
        {
            var callbackEvent = new LoadFileEvent(); 
            await callbackEvent.TriggerEventAsync(args);
        } 

    }
}

