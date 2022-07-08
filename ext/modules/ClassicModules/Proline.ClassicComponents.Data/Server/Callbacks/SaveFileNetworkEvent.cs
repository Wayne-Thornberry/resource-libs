using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MData.Server.Internal;
using Proline.DBAccess.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Events
{
    public partial class SaveFileNetworkEvent
    {
        public SaveFileNetworkEvent() : base(SAVEFILEHANDLER)
        {

        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            if (args.Length > 0)
            {

                var data = args[0].ToString();
                //Debug.WriteLine(data);
                var saveFile = JsonConvert.DeserializeObject<Entity.SaveFile>(data);
                var sm = DataFileManager.GetInstance();
                if (!sm.DoesPlayerHaveSave(player))
                    sm.CreateSave(player);
                var save = sm.GetSave(player);
                if (save == null)
                    throw new Exception("Save does not exist for player, cannot save a savefile in no save");
                save.InsertSaveFile(saveFile);
                var uq = UploadQueue.GetInstance();
                WriteSaveProcessor.WriteSaveToLocal(save);
                uq.Enqueue(save);
                //SaveFileAsync(player, saveFile);
                return null;
            }
            else
            { 
                Console.WriteLine("Argument count does not match expected count");
                return null;
            }
        }

       
    }
}
