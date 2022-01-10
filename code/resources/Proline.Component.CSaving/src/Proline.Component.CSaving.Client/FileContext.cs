using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json; 
using Proline.Component.Framework.Client.Access;
using Proline.Resource.Client.Eventing;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Res;
using Proline.Resource.Component.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CScriptObjects
{
    public class FileContext : ComponentContext
    {
        private Dictionary<string, object> _file;

       public FileContext()
        { 
            EventManager.AddEventListenerV2("CreateFile", new Action(CreateFile));
            EventManager.AddEventListenerV2("AddData", new Action<string, object>(AddData));
            EventManager.AddEventListenerV2("UploadFile", new Action(UploadData));
        }

        private void CreateFile()
        {
            _file = new Dictionary<string, object>();
        }

        private void AddData(string key, object data)
        {
            if (_file == null) return;
            _file.Add(key, data);
        }

        private void UploadData()
        {
            var data = JsonConvert.SerializeObject(_file, Formatting.Indented);
            _log.Debug(data);
        }

        public override void OnLoad()
        {
           
            base.OnLoad();
        }

    }
}