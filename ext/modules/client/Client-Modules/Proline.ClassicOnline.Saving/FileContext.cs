using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Saving
{
    public class FileContext : ModuleContext
    {
        private List<Dictionary<string, object>> _files;

        public FileContext()
        {
            _files = new List<Dictionary<string, object>>();


            // I want these to be exports :(
            //EventManager.AddEventListenerV2("CreateFile", new Action(CreateFile));
            //EventManager.AddEventListenerV2("AddData", new Action<string, object>(AddData));
            //EventManager.AddEventListenerV2("UploadFile", new Action(UploadData));

            // Actual event listensers
            //EventManager.AddEventListenerV2("playerJoinedSessionHandler", new Action(OnPlayerJoinedSession));

            // Responses
            //EventHandlers.Add("fileDownloadResponseHandler", new Action<string>(OnFileDownloadResponseHandler));
            //EventHandlers.Add("fileUploadResponseHandler", new Action<long>(OnFileUploadResponseHandler));
        }

        private void OnFileUploadResponseHandler(long obj)
        {

            //EventManager.InvokeEventV2("fileUploadComplete");
        }

        private void OnFileDownloadResponseHandler(string obj)
        {
            var dataCollection = JsonConvert.DeserializeObject<List<string>>(obj);
            foreach (var item in dataCollection)
            {
                _files.Add(JsonConvert.DeserializeObject<Dictionary<string, object>>(item));
            }
            //EventManager.InvokeEventV2("fileDownloadComplete");
        }



        public override void OnLoad()
        {
            //   RegisterScriptContext(new TestScript());
            ////   RegisterEventContext(new TestEventContext());
            //   TriggerServerEvent("fileDownloadRequestHandler");
            base.OnLoad();
        }

    }
}