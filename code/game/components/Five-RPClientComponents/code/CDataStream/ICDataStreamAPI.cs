using System.Threading.Tasks;

namespace CDataStream
{
    public interface ICDataStreamAPI
    {
        void AddDataFileValue(string key, object value);
        void CreateDataFile();
        bool DoesDataFileExist(string id);
        bool DoesDataFileValueExist(string key);
        void GetAudioSamplesAtIndex(int id, out string audioId, out string audioRef, out bool enabled);
        object GetDataFileValue(string key);
        T GetDataFileValue<T>(string key);
        int GetNumOfAudioSamples();
        int? GetSaveState();
        bool HasSaveLoaded();
        bool IsSaveInProgress();
        Task PullSaveFromCloud();
        void SaveDataFile(string identifier);
        void SelectDataFile(string identifier);
        Task SendSaveToCloud();
        void SetDataFileValue(string key, object value);
    }
}