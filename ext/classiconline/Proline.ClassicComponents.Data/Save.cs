using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MData.Events;
using Proline.ClassicOnline.MDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData
{
    public static partial class API
    {
        // SendSaveToCloud - Sends a request to the server to upload current Save (Save consists of multiple data files)
        // PullSaveFromCloud - Sends a request to download the file to the client
        public static async Task SendSaveToCloud()
        {
            try
            {
                var fm = DataFileManager.GetInstance();
                MDebugAPI.LogDebug($"Saving save file...");
                fm.IsSaveInProgress = true;
                foreach (var item in fm.GetSave(Game.Player).GetSaveFiles())
                {
                    try
                    { 
                        if (item == null)
                        {
                            throw new Exception($"Cannot save file, current save file is null");
                        };
                        var json = JsonConvert.SerializeObject(item);
                        MDebugAPI.LogDebug($"Saving file... {json}");
                        using (var sfEvent = WriteFileAction.TriggerEvent(json))
                        {
                            await sfEvent.WaitForCallback();
                        }
                    }
                    catch (Exception e)
                    {
                        MDebugAPI.LogDebug(e.ToString());
                    }
                }
                fm.IsSaveInProgress = false;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        } 
        public static bool IsSaveInProgress()
        {
            try
            {
                var fm = DataFileManager.GetInstance();
                return fm.IsSaveInProgress;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return false;
        }

        public static int? GetSaveState()
        {
            try
            {
                var fm = DataFileManager.GetInstance();
                if (fm.LastSaveResult != null)
                {
                    var state = fm.LastSaveResult;
                    fm.LastSaveResult = null;
                    return state.Value;
                }
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return null;
        }

        public static async Task PullSaveFromCloud(long id)
        {
            try
            {
                MDebugAPI.LogDebug("Load Request put in");
                var fm = DataFileManager.GetInstance();
                MDebugAPI.LogInfo("Waiting for callback to be completed...");
                var result = ReadFileAction.ReadLocalFile("Saves/acf48492b3aa7739725e33fc30aed8129b901d1f").Result;


                var instance = DataFileManager.GetInstance();
                if (result == null) return; 
                List<SaveFile> saveFiles = JsonConvert.DeserializeObject<List<SaveFile>>(result);
                Console.WriteLine("data got " + result);
                foreach (var item in saveFiles)
                {
                    instance.GetSave(Game.Player).InsertSaveFile(item);
                }
                instance.HasLoadedFromNet = true;
                //if (result == 0)
                //    MDebugAPI.LogInfo("Callback has completed");
                //else if (result == 1)
                //    throw new Exception("Callback has timed out");
                fm.HasLoadedFromNet = true;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        }

        public static bool HasSaveLoaded()
        {
            try
            {
                var fm = DataFileManager.GetInstance(); 
                return fm.HasLoadedFromNet;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return false;
        }


        public static async Task PullSaveFromCloud(string username)
        {
            try
            {
                MDebugAPI.LogDebug("Load Request put in");
                var lfEvent = ReadFileAction.TriggerEvent(username);
                MDebugAPI.LogInfo("Waiting for callback to be completed...");
                var result = await lfEvent.WaitForCallback();
                if (result == 0)
                    MDebugAPI.LogInfo("Callback has completed");
                else if (result == 1)
                    throw new Exception("Callback has timed out");
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        }
         
    }
}
