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
            var instance = SaveManager.GetInstance();
            if (args == null) return;
            if(args.Length > 0)
            {
                if (args[0] == null) return;
                List<SaveFile> saveFiles = JsonConvert.DeserializeObject<List<SaveFile>>(args[0].ToString());
                Console.WriteLine("data got " + args[0]);
                foreach (var item in saveFiles)
                { 
                    var saveFile = instance.GetSaveFile(item.Identifier); 
                    if (saveFile != null)
                    {
                        instance.OverrightSaveFile(item);
                    }
                    else
                    {
                        instance.InsertSaveFile(item);
                    } 
                }
                instance.HasLoadedFromNet = true;
            }
            
        }
    }
}
