using CitizenFX.Core;
using CScreenRendering;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static async Task FlashBlip(Blip blip, int duration = 100)
        {
            var api = new CScreenRenderingAPI();
            await api.FlashBlip(blip, duration);
        }

        public static async Task FlashBlip(int blipHandle, int duration = 100)
        {
            var api = new CScreenRenderingAPI();
            await api.FlashBlip(blipHandle, duration);

        }
    }
}
