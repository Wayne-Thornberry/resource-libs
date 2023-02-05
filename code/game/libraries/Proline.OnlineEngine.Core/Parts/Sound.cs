using CDataStream;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static void GetAudioSamplesAtIndex(int id, out string audioId, out string audioRef, out bool enabled)
        {
            var api = new CDataStreamAPI();
            api.GetAudioSamplesAtIndex(id, out audioId, out audioRef, out enabled);

        }
        public static int GetNumOfAudioSamples()
        {
            var api = new CDataStreamAPI();
            return api.GetNumOfAudioSamples();
        }
    }
}
