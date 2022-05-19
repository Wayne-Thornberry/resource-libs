using CitizenFX.Core;
using Proline.ClassicOnline.MData.Scripts;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.Saving
{
    public class MDataContext : ModuleContext
    {
        public MDataContext()
        {
            //EventHandlers.Add("playerConnectedHandler", new Action<int>(OnPlayerConnected));


            //EventHandlers.Add("fileDownloadRequestHandler", new Action<Player>(OnFileDownloadRequest));
            //EventHandlers.Add("fileUploadRequestHandler", new Action<Player, long, long, string>(OnFileUploadRequest));
        }

        public override void OnLoad()
        {
            BaseScript.RegisterScript(new FileSaveScript());
        }

        //private void OnPlayerConnected(int handle)
        //{

        //}

        //private void OnFileDownloadRequest([FromSource] Player player)
        //{
        //    var data = "";
        //    using (var engine = new EngineClient())
        //    {
        //        var files = engine.GetSaveFiles();
        //        var dataCollection = new List<string>();
        //        foreach (var item in files)
        //        {
        //            dataCollection.Add(item.Data);
        //        }
        //        data = JsonConvert.SerializeObject(dataCollection);
        //    }
        //    player.TriggerEvent("fileDownloadResponseHandler", data);
        //}

        //private void OnFileUploadRequest([FromSource] Player player, long playerId, long fileId, string data)
        //{
        //    using (var engine = new EngineClient())
        //    {
        //        if (fileId == -1)
        //        {
        //            engine.PostSaveFile(new SaveFile()
        //            {
        //                Data = data,
        //                InsertedAt = DateTime.UtcNow,
        //                PlayerId = playerId,
        //            });
        //        }
        //        else
        //        {
        //            engine.PutSaveFile(fileId, new SaveFile()
        //            {
        //                PlayerId = playerId,
        //                Data = data
        //            });
        //        }
        //    }
        //    player.TriggerEvent("fileUploadResponseHandler", "");
        //}


    }
}
