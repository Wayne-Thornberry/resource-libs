using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Internal;
using Proline.ClassicOnline.MDebug;
using Proline.ServerAccess.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

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
                var identifier = "acf48492b3aa7739725e33fc30aed8129b901d1f";
                var path = $"Saves/{identifier}/";
                var manifest = new List<string>();
                foreach (var item in fm.GetSave(Game.Player).GetSaveFiles())
                {
                    try
                    { 
                        if (item == null)
                        {
                            throw new Exception($"Cannot save file, current save file is null");
                        };
                        var json = JsonConvert.SerializeObject(item.Properties);
                        MDebugAPI.LogDebug($"Saving file... {json}"); 
                        await ServerFile.WriteLocalFile(Path.Combine(path, item.Identifier + ".json"), json);
                        manifest.Add(item.Identifier);
                    }
                    catch (Exception e)
                    {
                        MDebugAPI.LogDebug(e.ToString());
                    }
                }
                await ServerFile.WriteLocalFile(Path.Combine(path, "Manifest.json"), JsonConvert.SerializeObject(manifest));
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

        public static async Task PullSaveFromCloud()
        {
            try
            {
                MDebugAPI.LogDebug("Load Request put in");
                var fm = DataFileManager.GetInstance();
                MDebugAPI.LogInfo("Waiting for callback to be completed...");
                var identifier = "acf48492b3aa7739725e33fc30aed8129b901d1f";
                var data = await ServerFile.ReadLocalFile($"Saves/{identifier}/Manifest.json");
                var manifest = JsonConvert.DeserializeObject<List<string>>(data);
                var instance = DataFileManager.GetInstance();
                var save = instance.GetSave(Game.Player);
                foreach (var item in manifest)
                { 
                    var result1 = await ServerFile.ReadLocalFile($"Saves/{identifier}/{item}.json");
                    Console.WriteLine(item);
                    var properties =  JsonConvert.DeserializeObject<Dictionary<string,object>>(result1);
                    var saveFile = new SaveFile()
                    {
                        Identifier = item,
                        Properties = properties,
                    };
                    save.InsertSaveFile(saveFile);
                } 
                  
                instance.HasLoadedFromNet = true;
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

         
    }
}
