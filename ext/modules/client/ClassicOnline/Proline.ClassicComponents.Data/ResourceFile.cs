using System;
using System.Collections.Generic;
using System.Dynamic;
using CitizenFX.Core.Native;
using Newtonsoft.Json;

namespace Proline.ClassicOnline.MData
{
    public static class ResourceFile
    {
        private static Dictionary<string, object> _file;

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

        public static string LoadFile(string v)
        {
            return API.LoadResourceFile(API.GetCurrentResourceName(), v);
        }
    }
}
