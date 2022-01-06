using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Component.CSaving.Client;
using Proline.Component.Framework.Client.Access;
using Proline.Resource.Client.Eventing;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Res;
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
            ExportManager.CreateExport("CreateFile", new Action(CreateFile));
            ExportManager.CreateExport("AddData", new Action<string, object>(AddData));
            ExportManager.CreateExport("UploadData", new Action(UploadData));
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
            FileAPI.CreateFile();
            FileAPI.AddData("Test", 1);
            FileAPI.AddData("Test2", true);
            FileAPI.AddData("Test3", "string");
            FileAPI.AddData("Test4", 1.0f);
            FileAPI.AddData("Test5", new object[] { 1,"ses",false});
            FileAPI.UploadData();
            base.OnLoad();
        }

    }
}