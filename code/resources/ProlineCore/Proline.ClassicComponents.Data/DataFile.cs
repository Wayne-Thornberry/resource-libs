using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Internal;
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
        // CreateDataFile - Creates an temp data file to write too, if save is not called, this data file can be overriden
        // SaveDataFile - Saves the current active data file to local memory

        // SelectDataFile - Selects data file to be active data file
        // GetDataFileValue - Gets a value from the active data file
        // AddDataFileValue - Adds a value to the active data file
        // DeleteDataFileValue - Sets a value from the active data file
        // SetDataFileValue - Sets a value from the active data file

        public static void CreateDataFile()
        {
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
                MDebugAPI.LogError(e.ToString());
            }
        }

        public static bool DoesDataFileExist(string id)
        {
            try
            {
                var fm = DataFileManager.GetInstance();
                var save = fm.GetSave(Game.Player);
                return save.GetSaveFile(id) != null;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e.ToString());
            }
            return false;
        }

        public static void SaveDataFile(string identifier)
        {
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
                if(fm.TempFile != null)
                { 
                    fm.GetSave(Game.Player).InsertSaveFile(fm.TempFile);
                    fm.ClearTempFile();
                } 
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        }
        public static void SelectDataFile(string identifier)
        {
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
                MDebugAPI.LogError(e.ToString());
            }
        }
        public static bool DoesDataFileValueExist(string key)
        {
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
                MDebugAPI.LogDebug(e.ToString());
            }
            return false;
        }
        public static void AddDataFileValue(string key, object value)
        {
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
                MDebugAPI.LogDebug(e.ToString());
            }
        }
        public static void SetDataFileValue(string key, object value)
        {
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
                MDebugAPI.LogDebug(e.ToString());
            }
        }
        public static object GetDataFileValue(string key)
        {
            try
            {
                return GetDataFileValue<object>(key);
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return null;
        }
        public static T GetDataFileValue<T>(string key)
        {
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
                MDebugAPI.LogDebug(e.ToString());
            }
            return default;
        }
    }
}
