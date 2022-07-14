using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.MScreen.Minimap
{
    public static class MinimapAPI
    {
        public static async Task FlashBlip(Blip blip, int duration = 100)
        {
            blip.IsFlashing = true;
            var ms = 0;
            while (ms <= duration)
            {
                ms++;
                await BaseScript.Delay(1);
            }
            blip.IsFlashing = false;
        }

        public static async Task FlashBlip(int blipHandle, int duration = 100)
        {
            var blip = new Blip(blipHandle);
            await FlashBlip(blip);
        }
    }
}
