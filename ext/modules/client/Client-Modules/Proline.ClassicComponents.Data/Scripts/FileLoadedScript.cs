using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Scripts
{
    public class FileLoadedScript : ModuleScript
    {
        public FileLoadedScript()
        {
            EventHandlers.Add("FileLoadedHandler", new Action<string>(OnFileLoaded)); 
        }

        private void OnFileLoaded(string obj)
        {
            var instance = FileManager.GetInstance();
            instance.PutFileIntoMemory(obj);
        }
    }
}
