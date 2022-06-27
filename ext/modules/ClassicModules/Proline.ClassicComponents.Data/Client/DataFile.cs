using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData
{
    public partial class API
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
                var fm = SaveManager.GetInstance();
                var saveFile = fm.CreateTempSaveFile();
                fm.TargetSaveFile(saveFile); 
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e.ToString());
            }
        } 
        public static void SaveDataFile(string identifier)
        {
            try
            {

                if (string.IsNullOrEmpty(identifier))
                {
                    throw new ArgumentException("Identitier argument cannot be null or empty");
                } 

                var fm = SaveManager.GetInstance();
                fm.SaveTempSaveFile(identifier);
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
                var fm = SaveManager.GetInstance();
                var saveFiles = fm.GetSaveFiles();
                fm.TargetSaveFile(identifier);
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
                var fm = SaveManager.GetInstance();
                var saveFile = fm.GetTargetSaveFile();
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
                var fm = SaveManager.GetInstance();
                var saveFile = fm.GetTargetSaveFile();
                if (saveFile == null) return;
                var dictionary = saveFile.Properties;
                if (dictionary == null)
                    return;
                if (!dictionary.ContainsKey(key))
                    dictionary.Add(key, value); 
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
                var fm = SaveManager.GetInstance();
                var saveFile = fm.GetTargetSaveFile();
                if (saveFile == null) 
                    throw new Exception("No save file has been targed");
                var dictionary = saveFile.Properties;
                if (dictionary == null)
                    throw new Exception("Save file does not have any property dictionary");
                if (dictionary.ContainsKey(key))
                    dictionary[key] = value; 
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
                var fm = SaveManager.GetInstance();
                var saveFile = fm.GetTargetSaveFile();
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
