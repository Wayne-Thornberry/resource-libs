using CitizenFX.Core;
using System.Drawing;
using System.Threading.Tasks;

namespace CScreenRendering
{
    public interface ICScreenRenderingAPI
    {
        void Draw2DBox(float x, float y, float width, float heigth, Color color);
        void DrawDebug2DBox(PointF start, PointF end, Color color);
        void DrawDebugText2D(string text, PointF vector2, float scale, int font);
        Task FlashBlip(Blip blip, int duration = 100);
        Task FlashBlip(int blipHandle, int duration = 100);
        Vector3 ScreenRelToWorld(Vector3 camPos, Vector3 camRot, Vector2 coord, out Vector3 forwardDirection);
    }
}