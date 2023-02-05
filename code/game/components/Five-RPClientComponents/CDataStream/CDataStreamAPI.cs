using CDataStream.Data;
using CDataStream.Internal;
using CDebugActions;
using CitizenFX.Core;
using Newtonsoft.Json;

using Proline.Resource.IO;
using Proline.ResourceLibs.ServerEventCaller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace CDataStream
{
    public class CDataStreamAPI : ICDataStreamAPI
    {
        // CreateDataFile - Creates an temp data file to write too, if save is not called, this data file can be overriden
        // SaveDataFile - Saves the current active data file to local memory

        // SelectDataFile - Selects data file to be active data file
        // GetDataFileValue - Gets a value from the active data file
        // AddDataFileValue - Adds a value to the active data file
        // DeleteDataFileValue - Sets a value from the active data file
        // SetDataFileValue - Sets a value from the active data file

        public void CreateDataFile()
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var saveFile = new SaveFile();
                saveFile.Name = "Tempname";
                saveFile.Properties = new Dictionary<string, object>();
                saveFile.LastChanged = DateTime.UtcNow;
                saveFile.HasUpdated = true;
                fm.TempFile = saveFile;
                fm.ActiveFile = saveFile;
            }
            catch (Exception e)
            {
                api.LogError(e.ToString());
            }
        }

        public string GetSelectedDataFile()
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var saveFile = fm.ActiveFile;
                return saveFile.Name;
            }
            catch (Exception e)
            {
                api.LogError(e.ToString());
            }
            return "";
        }

        public bool DoesDataFileExist(string id)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var save = fm.GetSave(Game.Player);
                return save.GetSaveFile(id) != null;
            }
            catch (Exception e)
            {
                api.LogError(e.ToString());
            }
            return false;
        }

        public void SaveDataFile(string identifier)
        {
            var api = new CDebugActionsAPI();
            try
            {

                if (string.IsNullOrEmpty(identifier))
                {
                    throw new ArgumentException("Identitier argument cannot be null or empty");
                }

                var fm = DataFileManager.GetInstance();
                var id = identifier;//string.IsNullOrEmpty(identifier) ? "SaveFile" + index : identifier;
                var tempFile = fm.TempFile;
                tempFile.Name = id;
                if (fm.TempFile != null)
                {
                    fm.GetSave(Game.Player).InsertSaveFile(fm.TempFile);
                    fm.ClearTempFile();
                }
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
        }
        public void SelectDataFile(string identifier)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var save = fm.GetSave(Game.Player);
                var dataFile = save.GetSaveFile(identifier);
                if (dataFile == null)
                    throw new Exception("Could not target a save file, save file does not exist " + identifier);
                fm.ActiveFile = dataFile;
            }
            catch (Exception e)
            {
                api.LogError(e.ToString());
            }
        }
        public bool DoesDataFileValueExist(string key)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var saveFile = fm.ActiveFile;
                var dictionary = saveFile.Properties;
                if (saveFile != null && dictionary != null)
                {
                    return dictionary.ContainsKey(key);
                }
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
            return false;
        }
        public void AddDataFileValue(string key, object value)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var saveFile = fm.ActiveFile;
                if (saveFile == null) return;
                var dictionary = saveFile.Properties;
                if (dictionary == null)
                    return;
                if (!dictionary.ContainsKey(key))
                    dictionary.Add(key, value);
                saveFile.HasUpdated = true;
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
        }
        public void SetDataFileValue(string key, object value)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var saveFile = fm.ActiveFile;
                if (saveFile == null)
                    throw new Exception("No save file has been targed");
                var dictionary = saveFile.Properties;
                if (dictionary == null)
                    throw new Exception("Save file does not have any property dictionary");
                if (dictionary.ContainsKey(key))
                    dictionary[key] = value;
                saveFile.HasUpdated = true;
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
        }
        public object GetDataFileValue(string key)
        {
            var api = new CDebugActionsAPI();
            try
            {
                return GetDataFileValue<object>(key);
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
            return null;
        }
        public T GetDataFileValue<T>(string key)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                var saveFile = fm.ActiveFile;
                if (saveFile == null)
                    throw new Exception("No save file has been targed");
                var dictionary = saveFile.Properties;
                if (dictionary.ContainsKey(key))
                    return JsonConvert.DeserializeObject<T>(dictionary[key].ToString());
                else
                    throw new Exception("No key exists for " + key);

            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
            return default;
        }


        public int GetNumOfAudioSamples()
        {
            var api = new CDebugActionsAPI();
            try
            {
                var resourceData2 = ResourceFile.Load($"data/audio/fronend_usages.json");
                var buildingMetaData = JsonConvert.DeserializeObject<List<SoundExample>>(resourceData2.Load());
                return buildingMetaData.Count;
            }
            catch (Exception e)
            {
                api.LogError(e.ToString());
            }
            return 0;
        }

        public async Task SendSaveToCloud()
        {
            var api = new CDebugActionsAPI();
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
                if (files.Count() > 0)
                {
                    api.LogDebug($"Saving...");
                    foreach (var file in files)
                    {
                        try
                        {
                            if (file == null)
                            {
                                throw new Exception($"Cannot save file, current save file is null");
                            };
                            var json = JsonConvert.SerializeObject(file.Properties);
                            api.LogDebug($"Saving file... {json}");

                            var x = new EventCaller("SaveFileHandler");
                            x.OnEventCallback((args) =>
                            {

                            });
                            x.Invoke(Path.Combine(path, file.Name + ".json"), json);
                            await x.WaitForCallback();
                            file.HasUpdated = false;
                        }
                        catch (Exception e)
                        {
                            api.LogDebug(e.ToString());
                        }
                    }
                }
                else
                {
                    api.LogDebug("Cannot send save to cloud, No save files to save");
                }

                if (save.HasChanged)
                {
                    api.LogDebug($"Saving manifest...");
                    var saveFiles = save.GetSaveFiles().Select(e => e.Name);
                    var x = new EventCaller("SaveFileHandler");
                    x.OnEventCallback((args) =>
                    {

                    });
                    x.Invoke(Path.Combine(path, "Manifest.json"), JsonConvert.SerializeObject(saveFiles));
                    await x.WaitForCallback();
                    save.HasChanged = false;
                }
                fm.IsSaveInProgress = false;
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
        }
        public bool IsSaveInProgress()
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                return fm.IsSaveInProgress;
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
            return false;
        }

        public int? GetSaveState()
        {
            var api = new CDebugActionsAPI();
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
                api.LogDebug(e.ToString());
            }
            return null;
        }

        public async Task PullSaveFromCloud()
        {
            var api = new CDebugActionsAPI();
            try
            {
                api.LogDebug("Load Request put in");
                var fm = DataFileManager.GetInstance();
                api.LogInfo("Waiting for callback to be completed...");
                var identifier = Game.Player.Name;
                var x = new EventCaller("LoadFileHandler");
                x.OnEventCallback(async (args) =>
                {
                    if (args != null)
                    {
                        // old way via getting id
                        if (args.Length > 0)
                        {
                            if (args[0] != null)
                            { 
                                var data = args[0].ToString();
                                if (data == null)
                                    throw new Exception("No manifest data for player save has been found");
                                var manifest = JsonConvert.DeserializeObject<List<string>>(data);
                                var instance = DataFileManager.GetInstance();
                                var save = instance.GetSave(Game.Player);
                                foreach (var item in manifest)
                                {
                                    var xy = new EventCaller("LoadFileHandler");
                                    xy.OnEventCallback((args2) =>
                                    {
                                        var result1 = args2[0].ToString();
                                        Console.WriteLine(item);
                                        var properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(result1);
                                        var saveFile = new SaveFile()
                                        {
                                            Name = item,
                                            Properties = properties,
                                        };
                                        save.InsertSaveFile(saveFile);
                                    });
                                    xy.Invoke($"Saves/{identifier}/{item}.json");
                                    await x.WaitForCallback();
                                }

                                instance.HasLoadedFromNet = true;
                                fm.HasLoadedFromNet = true;
                            }
                        }
                    }
                });
                x.Invoke($"Saves/{identifier}/Manifest.json");
                await x.WaitForCallback(); 
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
        }

        public bool HasSaveLoaded()
        {
            var api = new CDebugActionsAPI();
            try
            {
                var fm = DataFileManager.GetInstance();
                return fm.HasLoadedFromNet;
            }
            catch (Exception e)
            {
                api.LogDebug(e.ToString());
            }
            return false;
        }

        public void GetAudioSamplesAtIndex(int id, out string audioId, out string audioRef, out bool enabled)
        {
            var api = new CDebugActionsAPI();
            audioId = "";
            audioRef = "";
            enabled = false;
            try
            {
                var resourceData2 = ResourceFile.Load($"data/audio/fronend_usages.json");
                var buildingMetaData = JsonConvert.DeserializeObject<List<SoundExample>>(resourceData2.Load());
                audioId = buildingMetaData[id].AudioId;
                audioRef = buildingMetaData[id].AudioRef;
                enabled = buildingMetaData[id].Enabled;
            }
            catch (Exception e)
            {
                api.LogError(e.ToString());
            }
        }
    }
}
