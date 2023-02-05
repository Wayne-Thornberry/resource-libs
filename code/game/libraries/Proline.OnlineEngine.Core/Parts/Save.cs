using CDataStream; 
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static int? GetSaveState()
        {
            var api = new CDataStreamAPI();
            return api.GetSaveState();
        }
        public static bool HasSaveLoaded()
        {
            var api = new CDataStreamAPI();
            return api.HasSaveLoaded();
        }
        public static bool IsSaveInProgress()
        {
            var api = new CDataStreamAPI();
            return api.IsSaveInProgress();
        }
        public static async Task PullSaveFromCloud()
        {
            var api = new CDataStreamAPI();
            await api.PullSaveFromCloud();
        }
        public static async Task SendSaveToCloud()
        {
            var api = new CDataStreamAPI();
            await api.SendSaveToCloud();
        }
    }
}
