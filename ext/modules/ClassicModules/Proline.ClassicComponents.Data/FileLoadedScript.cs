using Proline.ClassicOnline.MData.Entity;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Scripts
{
    public class FileLoadedScript : ModuleScript
    {
        public FileLoadedScript(Assembly source) : base(source)
        {
            EventHandlers.Add("FileLoadedHandler", new Action<string>(OnFileLoaded));
            EventHandlers.Add("SaveFileResponseHandler", new Action<int>(OnSaveFileResponse));
        }

        //public FileLoadedScript()
        //{
        //    EventHandlers.Add("FileLoadedHandler", new Action<string>(OnFileLoaded)); 
        //    EventHandlers.Add("SaveFileResponseHandler", new Action<int>(OnSaveFileResponse)); 
        //}

        private void OnSaveFileResponse(int obj)
        {
            var fm = FileManager.GetInstance();
            fm.IsSaveInProgress = false;
            fm.LastSaveResult = obj;
        }

        private void OnFileLoaded(string obj)
        {
            var instance = FileManager.GetInstance();
            instance.PutFileIntoMemory(obj);
        }
    }
}
