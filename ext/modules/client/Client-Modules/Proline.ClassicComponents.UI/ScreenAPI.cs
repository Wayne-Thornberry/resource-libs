using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScreen
{
    public static class ScreenAPI
    {
        public static void Draw2DBox(float x, float y, float width, float heigth, Color color)
        {
            //NativeAPI.CallNativeAPI(Hash.DRAW_RECT, x, y, width, heigth, color.R, color.G, color.B, color.A);
        }


        public static void DrawDebug2DBox(PointF start, PointF end, Color color)
        {
            var width = Math.Abs(start.X - end.X);
            var height = Math.Abs(start.Y - end.Y);

            start = new PointF(Math.Abs(start.X + width / 2), Math.Abs(start.Y + height / 2));

            Draw2DBox(start.X, start.Y, width, height, color);
        }

        public static void DrawDebugText2D(string text, PointF vector2, float scale, int font)
        {
            API.SetTextFont(font);
            API.SetTextProportional(true);
            API.SetTextScale(0.0f, scale);
            API.SetTextColour(255, 255, 255, 255);
            // NativeAPI.SetTextDropshadow(0, 0, 0, 0, 255);
            // NativeAPI.SetTextEdge(1, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.SetTextCentre(false);
            API.AddTextComponentString(text);
            API.DrawText(vector2.X, vector2.Y);
        }
    }
}
