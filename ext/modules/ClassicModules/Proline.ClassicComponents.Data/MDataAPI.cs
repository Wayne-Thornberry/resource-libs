using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;

namespace Proline.ClassicOnline.MData
{
    public static class MDataAPI
    {

#if CLIENT
        public static void CreateFile()
        {
            var fm = FileManager.GetInstance();
            fm.CreateNewFile();
        }

        public static void SaveFile()
        {
            var fm = FileManager.GetInstance();
            var json = JsonConvert.SerializeObject(fm.GetCurrentSaveFile());
            MDebug.MDebugAPI.LogDebug($"Saving file... {json}");
            BaseScript.TriggerServerEvent(EventHandlerNames.SAVEFILEHANDLER, json);
            fm.IsSaveInProgress = true;
        }

        public static bool IsSaveInProgress()
        {
            var fm = FileManager.GetInstance();
            return fm.IsSaveInProgress;
        }

        public static int? GetSaveState()
        {
            var fm = FileManager.GetInstance();
            if (fm.LastSaveResult != null)
            {
                var state = fm.LastSaveResult;
                fm.LastSaveResult = null;
                return state.Value;
            } 
            return null;
        }

        public static void AddFileValue(string key, object value)
        {
            var fm = FileManager.GetInstance();
            var dictionary = fm.GetCurrentSaveFile();
            if(dictionary != null)
                dictionary.Add(key, value);
        } 

        public static object GetFileValue(string key)
        {
            var fm = FileManager.GetInstance();
            var dictionary = fm.GetCurrentSaveFile();
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            return null;
        } 

        public static T GetFileValue<T>(string key)
        {
            var fm = FileManager.GetInstance();
            var dictionary = fm.GetCurrentSaveFile();
            if (dictionary.ContainsKey(key))
                return JsonConvert.DeserializeObject<T>(dictionary[key].ToString());
            return default;
        }

        public static void LoadFile(long id)
        { 
            BaseScript.TriggerServerEvent(EventHandlerNames.LOADFILEHANDLER, id); 
        }

        public static bool IsFileLoaded()
        { 
            var fm = FileManager.GetInstance();
            return fm.IsInMemory();
        }

        public static string LoadResourceFile(string v)
        {
            return API.LoadResourceFile(API.GetCurrentResourceName(), v);
        }
#endif
    }
}
