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
                var save = fm.GetSave(Game.Player);
                if (save == null || fm.IsSaveInProgress)
                    throw new Exception("Cannot send save to cloud, save in progress or save is null");
                fm.IsSaveInProgress = true;
                var identifier = Game.Player.Name;
                var path = $"Saves/{identifier}/";
                var files = save.GetSaveFiles().Where(e => e.HasUpdated);
                if(files.Count() > 0)
                {
                    MDebugAPI.LogDebug($"Saving...");
                    foreach (var file in files)
                    {
                        try
                        {
                            if (file == null)
                            {
                                throw new Exception($"Cannot save file, current save file is null");
                            };
                            var json = JsonConvert.SerializeObject(file.Properties);
                            MDebugAPI.LogDebug($"Saving file... {json}");
                            await ServerFile.WriteLocalFile(Path.Combine(path, file.Name + ".json"), json);
                            file.HasUpdated = false;
                        }
                        catch (Exception e)
                        {
                            MDebugAPI.LogDebug(e.ToString());
                        }
                    }
                }
                else
                {
                    MDebugAPI.LogDebug("Cannot send save to cloud, No save files to save");
                }
               
                if (save.HasChanged)
                {
                    MDebugAPI.LogDebug($"Saving manifest...");
                    var saveFiles = save.GetSaveFiles().Select(e => e.Name);
                    await ServerFile.WriteLocalFile(Path.Combine(path, "Manifest.json"), JsonConvert.SerializeObject(saveFiles));
                    save.HasChanged = false;
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

        public static async Task PullSaveFromCloud()
        {
            try
            {
                MDebugAPI.LogDebug("Load Request put in");
                var fm = DataFileManager.GetInstance();
                MDebugAPI.LogInfo("Waiting for callback to be completed...");
                var identifier = Game.Player.Name;
                var data = await ServerFile.ReadLocalFile($"Saves/{identifier}/Manifest.json");
                if (data == null) 
                    throw new Exception("No manifest data for player save has been found");
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
                        Name = item,
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
