using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MData.Events;

namespace Proline.ClassicOnline.MData
{
    public static partial class MDataAPI
    {
        public static void CreateFile()
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                fm.CreateNewFile();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task SaveFile()
        {
            try
            {
                var fm = SaveFileManager.GetInstance();
                var json = JsonConvert.SerializeObject(fm.GetCurrentSaveFile());
                MDebug.MDebugAPI.LogDebug($"Saving file... {json}");
                await SaveFileEvent.Execute(json); 
                fm.IsSaveInProgress = true;
            }
            catch (Exception)
            {

                throw;
            }
        }
         

        public static bool DoesValueExist(string key)
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                var dictionary = fm.GetCurrentSaveFile();
                if (dictionary == null)
                    return false;
                return dictionary.ContainsKey(key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool IsSaveInProgress()
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                return fm.IsSaveInProgress;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int? GetSaveState()
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                if (fm.LastSaveResult != null)
                {
                    var state = fm.LastSaveResult;
                    fm.LastSaveResult = null;
                    return state.Value;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void AddFileValue(string key, object value)
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                var dictionary = fm.GetCurrentSaveFile();
                if (dictionary == null)
                    return;
                if (dictionary.ContainsKey(key))
                    dictionary[key] = value;
                else
                    dictionary.Add(key, value);
            }
            catch (Exception)
            {

                throw;
            }

        } 

        public static object GetFileValue(string key)
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                var dictionary = fm.GetCurrentSaveFile();
                if (dictionary.ContainsKey(key))
                    return dictionary[key];
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        } 

        public static T GetFileValue<T>(string key)
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                var dictionary = fm.GetCurrentSaveFile();
                if (dictionary.ContainsKey(key))
                    return JsonConvert.DeserializeObject<T>(dictionary[key].ToString());
                return default;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task LoadFile(long id)
        {
            try
            { 
               await LoadFileEvent.Execute(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool IsFileLoaded()
        {
            try
            { 
                var fm = SaveFileManager.GetInstance();
                return fm.IsInMemory();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string LoadResourceFile(string v)
        {
            try
            {
                return API.LoadResourceFile(API.GetCurrentResourceName(), v); 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
} 
