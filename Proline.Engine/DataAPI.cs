

namespace Proline.Engine
{
    public static class DataAPI
    { //// Provides methods to interact with the data files on the client, loading, reading, formatting, 
        /// like networking, this will be called by mutliple different apis that are simplitic version
        /// For example, Save will call this, Stats will call this, Store will call this
        /// 

        public static void GetFileNames(out string[] fileNames)
        {
            fileNames = null;
            return;
        }

        public static void GetResourceMetadata(out string fileNames)
        {
            // Returns the data contained 

            fileNames = null;
            return;
        }

        public static void LoadResourceFile(string filePath, out string data)
        {
            //// Loads resources using the native
            data = CitizenAccess.GetInstance().LoadResourceFile(CitizenAccess.GetInstance().GetCurrentResourceName(), filePath);
            //if (string.IsNullOrEmpty(data))
            //    DebugAPI.LogDebug($"No file found {filePath}");
            //else
            //    DebugAPI.LogDebug(data);

            //// Files called here should be cached to stop unessary api calls
            /// Cache should be flushed regullarly  

            return;
        }

        //public void LoadNetworkResourceFile(string fileName, out string data)
        //{
        //    //// Loads resources using the native
        //    var rc = CallNetworkAPI("LoadResourceFile", fileName);
        //    data = (string)outData;
        //    WriteLine(data);

        //    return;
        //}
    }
}
