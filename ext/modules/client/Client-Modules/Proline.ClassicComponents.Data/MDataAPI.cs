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
        private static Dictionary<string, object> _file;
        public static void SaveObject(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            MDebug.MDebugAPI.LogDebug($"Saving file... {json}");
            BaseScript.TriggerServerEvent("SaveFileHandler", json);
        }

        public static void CreateNewFile()
        {
            _file = new Dictionary<string, object>();
        }

        public static void AddValue(string key, object value)
        {
            _file.Add(key, value);
        }

        public static object RetriveValue(string key, object value)
        {
            if (_file != null)
            {
                if (_file.ContainsKey(key))
                    return _file[key];
            }
            return null;
        }

        public static object GetFileValue(string file, string key)
        {
            dynamic x = JsonConvert.DeserializeObject<ExpandoObject>(LoadFile(file));
            return x.Money;
        }

        public static async Task<string> LoadFileAsync(long id)
        {
            var fm = FileManager.GetInstance();
            int attemptTicks = 0;
            BaseScript.TriggerServerEvent("LoadFileHandler", id);
            while (!fm.IsInMemory() && attemptTicks < 1000)
            {
                attemptTicks++;
                await BaseScript.Delay(0);
            }

            if (fm.IsInMemory())
            {
               return fm.GetFileFromMemory();
            }
            return null;
        }

        public static string LoadFile(string v)
        {
            return API.LoadResourceFile(API.GetCurrentResourceName(), v);
        }
    }
}
