using CitizenFX.Core;
using CScreenRendering;
using System.Drawing;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static void Draw2DBox(float x, float y, float width, float heigth, Color color)
        {
            var api = new CScreenRenderingAPI();
            api.Draw2DBox(x, y, width, heigth, color);

        }

        public static Vector3 ScreenRelToWorld(Vector3 camPos, Vector3 camRot, Vector2 coord, out Vector3 forwardDirection)
        {
            var api = new CScreenRenderingAPI();
            return api.ScreenRelToWorld(camPos, camRot, coord, out forwardDirection);
        }


        public static void DrawDebug2DBox(PointF start, PointF end, Color color)
        {
            var api = new CScreenRenderingAPI();
            api.DrawDebug2DBox(start, end, color);
        }

        public static void DrawDebugText2D(string text, PointF vector2, float scale, int font)
        {
            var api = new CScreenRenderingAPI();
            api.DrawDebugText2D(text, vector2, scale, font);

        }
    }
}
