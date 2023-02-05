

using CDataStream;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static void AddDataFileValue(string key, object value)
        {
            var api = new CDataStreamAPI();
            api.AddDataFileValue(key, value);
        }

        public static void CreateDataFile()
        {
            var api = new CDataStreamAPI();
            api.CreateDataFile();
        }
        public static bool DoesDataFileExist(string id)
        {
            var api = new CDataStreamAPI();
            return api.DoesDataFileExist(id);
        }
        public static bool DoesDataFileValueExist(string key)
        {
            var api = new CDataStreamAPI();
            return api.DoesDataFileValueExist(key);
        }
        public static object GetDataFileValue(string key)
        {
            var api = new CDataStreamAPI();
            return api.GetDataFileValue(key);
        }
        public static T GetDataFileValue<T>(string key)
        {
            var api = new CDataStreamAPI();
            return api.GetDataFileValue<T>(key);
        }
        public static void SaveDataFile(string identifier)
        {
            var api = new CDataStreamAPI();
            api.SaveDataFile(identifier);
        }
        public static string GetSelectedDataFile()
        {
            var api = new CDataStreamAPI();
            return api.GetSelectedDataFile();
        }
        public static void SelectDataFile(string identifier)
        {
            var api = new CDataStreamAPI();
            api.SelectDataFile(identifier);
        }
        public static void SetDataFileValue(string key, object value)
        {
            var api = new CDataStreamAPI();
            api.SetDataFileValue(key, value);
        }
    }
}
