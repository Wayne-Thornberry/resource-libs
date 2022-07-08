using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {

        public LoadFileNetworkEvent() : base(LOADFILEHANDLER, true)
        {

        }

        protected override void OnEventCallback(params object[] args)
        {
            var instance = DataFileManager.GetInstance();
            if (args == null) return;
            if(args.Length > 0)
            {
                if (args[0] == null) return;
                List<SaveFile> saveFiles = JsonConvert.DeserializeObject<List<SaveFile>>(args[0].ToString());
                Console.WriteLine("data got " + args[0]);
                foreach (var item in saveFiles)
                {  
                    instance.GetSave(Game.Player).InsertSaveFile(item); 
                }
                instance.HasLoadedFromNet = true;
            }
            
        }
         
        public static LoadFileNetworkEvent TriggerEvent(long id)
        {
            var x = new LoadFileNetworkEvent();
            x.Invoke(id);
            return x;
        }

        public static LoadFileNetworkEvent TriggerEvent(string username)
        {
            var x = new LoadFileNetworkEvent();
            x.Invoke(username);
            return x;
        }
    }
}
