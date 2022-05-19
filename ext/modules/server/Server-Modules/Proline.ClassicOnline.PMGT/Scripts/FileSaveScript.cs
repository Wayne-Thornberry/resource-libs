using CitizenFX.Core;
using Proline.DBAccess.Proxies;
using Proline.Resource.Console;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Scripts
{
    public class FileSaveScript : ModuleScript
    {
        public FileSaveScript()
        {
            EventHandlers.Add("SaveFileHandler", new Action<Player, string>(OnFileSave));
            EventHandlers.Add("LoadFileHandler", new Action<Player, long>(OnFileLoad));
        }

        private void OnFileSave([FromSource] Player arg1, string arg2)
        {
            SaveFileAsync(arg2);
        }

        private static async Task SaveFileAsync(string arg2)
        {
            try
            {
                Debug.WriteLine(arg2);
                using (var x = new DBAccessClient())
                {
                    await x.SaveFile(new PlacePlayerDataInParameters() { Data = arg2 });
                }
            }
            catch (Exception e)
            {
                EConsole.WriteLine(e.ToString());
            }
        }

        private void OnFileLoad([FromSource] Player arg1, long arg2)
        {
            LoadFileAsync(arg1, arg2);
        }

        private static async Task LoadFileAsync(Player arg1, long arg2)
        {
            try
            {
                var data = "";
                using (var x = new DBAccessClient())
                {
                    data = (await x.LoadFile(new GetPlayerDataInParameters() { Id = arg2 })).Data;
                }
                arg1.TriggerEvent("FileLoadedHandler", data);
            }
            catch (Exception e)
            {
                EConsole.WriteLine(e.ToString());
            };
        }
    }
}
