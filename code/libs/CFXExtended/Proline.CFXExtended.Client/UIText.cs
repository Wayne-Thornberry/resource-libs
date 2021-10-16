using System;
using System.Drawing;
using System.Text;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace Proline.CFXExtended.Core
{
    public class UIText
    {
        public UIText(string caption, PointF position, float scale)
        {
            Caption = caption;
            Position = position;
            Scale = scale;
        }

        public string Caption { get; set; }
        public PointF Position { get; set; }
        public float Scale { get; set; }
        public CitizenFX.Core.UI.Font Font { get; set; }
        public bool EnableShadow { get; set; }
        public bool EnableOutline { get; set; }
        public Alignment Alignment { get; set; }
        public Color Color { get; set; }

        public int WordWrap { get; set; }
        public bool UseSafezone { get; set; }

        public void Draw()
        {
            var screenw = Screen.Resolution.Width;
            var screenh = Screen.Resolution.Height;
            const float height = 1080f;
            var ratio = (float)screenw / screenh;
            var width = height * ratio;

            var x = Position.X / width;
            var y = Position.Y / height;

            API.SetTextFont((int)Font);
            API.SetTextScale(1.0f, Scale);
            API.SetTextColour(Color.R, Color.G, Color.B, Color.A);
            if (EnableShadow)
                API.SetTextDropshadow(0, 0, 0, 0, 100);
            if (EnableOutline)
                API.SetTextOutline();
            switch (Alignment)
            {
                case Alignment.Center:
                    API.SetTextCentre(true);
                    break;
                case Alignment.Right:
                    API.SetTextRightJustify(true);
                    API.SetTextWrap(0, x);
                    break;
            }

            if (WordWrap != 0)
            {
                var xsize = (Position.X + WordWrap) / width;
                API.SetTextWrap(x, xsize);
            }

            API.SetTextEntry("jamyfafi");
            AddLongString(Caption);

            API.DrawText(x, y);
        }

        private static void AddLongString(string str)
        {
            var utf8ByteCount = Encoding.UTF8.GetByteCount(str);

            if (utf8ByteCount == str.Length)
                AddLongStringForAscii(str);
            else
                AddLongStringForUtf8(str);
        }

        private static void AddLongStringForAscii(string input)
        {
            const int maxByteLengthPerString = 99;

            for (var i = 0; i < input.Length; i += maxByteLengthPerString)
            {
                var substr = input.Substring(i, Math.Min(maxByteLengthPerString, input.Length - i));
                Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, substr);
            }
        }

        private static void AddLongStringForUtf8(string input)
        {
            const int maxByteLengthPerString = 99;

            if (maxByteLengthPerString < 0) throw new ArgumentOutOfRangeException("maxLengthPerString");
            if (string.IsNullOrEmpty(input) || maxByteLengthPerString == 0) return;

            var enc = Encoding.UTF8;

            var utf8ByteCount = enc.GetByteCount(input);
            if (utf8ByteCount < maxByteLengthPerString)
            {
                Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, input);
                return;
            }

            var startIndex = 0;

            for (var i = 0; i < input.Length; i++)
            {
                var length = i - startIndex;
                if (enc.GetByteCount(input.Substring(startIndex, length)) > maxByteLengthPerString)
                {
                    var substr = input.Substring(startIndex, length - 1);
                    Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, substr);

                    i -= 1;
                    startIndex = startIndex + length - 1;
                }
            }

            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, input.Substring(startIndex, input.Length - startIndex));
        }
    }
}