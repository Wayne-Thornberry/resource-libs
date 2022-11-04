using CitizenFX.Core;
using Newtonsoft.Json; 
using ProlineCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore
{
    public partial class API
    {
        //    public static async Task PullSaveFromCloudAsync(Player player, string arg2)
        //    {
        //        List<Internal.SaveFile> data = new List<Internal.SaveFile>();
        //        try
        //        {
        //            Console.WriteLine("Load Request Recived " + arg2);
        //            using (var x = new DBAccessClient())
        //            {
        //                var response = await x.LoadFile(new GetSaveRequest() { Username = arg2 });
        //                var saveFiles = response.SaveFiles;
        //                var instance = DataFileManager.GetInstance();
        //                if (!instance.DoesPlayerHaveSave(player))
        //                    instance.CreateSave(player);
        //                foreach (var item in saveFiles)
        //                {
        //                    data.Add(new Internal.SaveFile()
        //                    {
        //                        Identifier = item.Identity,
        //                        Properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(item.Data),
        //                    });
        //                    Console.WriteLine("data got " + item.Data);
        //                }
        //            }
        //            Console.WriteLine("Load Request  " + data.Count);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Cannot access the web api, is the web service down?");
        //        }
        //        finally
        //        {
        //            //  ExternalInvokeCallback(player, data);
        //        }

        //    }

        //    public static void PullSaveFromLocal(Player player)
        //    {
        //        try
        //        {
        //            var save = WriteSaveProcessor.ReadSaveFromLocal(player);
        //            var instance = DataFileManager.GetInstance();
        //            if (save != null)
        //            {
        //                instance.InsertSave(save);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.ToString());
        //        }
        //        finally
        //        {
        //            //  ExternalInvokeCallback(player, data);
        //        }
        //    }

        //    public static async Task SendSaveToCloudAsync(Player player, Save save)
        //    {
        //        var responseCode = -1;
        //        try
        //        {
        //            using (var x = new DBAccessClient())
        //            {
        //                var response = await x.RegisterPlayer(new RegisterPlayerRequest() { Name = player.Name });
        //                long id = response.Id;
        //                Debug.WriteLine(string.Format("Registered user returned {0}, id {1}", response.ReturnCode, response.Id));
        //                if (response.ReturnCode == 1)
        //                {
        //                    var getPlayerResponse = await x.GetPlayer(new GetPlayerRequest() { Username = player.Name });
        //                    Debug.WriteLine(string.Format("Getting Player {0}, id {1}", getPlayerResponse.ReturnCode, getPlayerResponse.PlayerId));
        //                    id = getPlayerResponse.PlayerId;
        //                }
        //                foreach (var saveFile in save.GetSaveFiles())
        //                {
        //                    var response2 = await x.SaveFile(new InsertSaveRequest() { PlayerId = id, Identity = saveFile.Identifier, Data = JsonConvert.SerializeObject(saveFile.Properties) });
        //                    responseCode = response2.ReturnCode;
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Cannot access the web api, is the web service down?");
        //        }
        //        finally
        //        {
        //            //ExternalInvokeCallback(player, responseCode);
        //        }
        //    }
    }
}
