


using CitizenFX.Core.Native;

namespace Proline.CFXExtended.Core
{
    public class UIBox 
    {

        public void Draw(float x, float y, float w, float h, int r, int g, int b, int a)
        {
            API.DrawRect(x, y, w, h, r, g, b, a);
        }
    }
}