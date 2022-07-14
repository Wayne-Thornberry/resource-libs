using System.Drawing;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Proline.ClassicOnline.MScreen.Menus.MenuItems
{
    public class MenuSliderItem : MenuItem
    {
        public MenuSliderItem(string name, int min, int max, int startPosition) : this(name, min, max, startPosition,
            false)
        {
        }

        public MenuSliderItem(string name, int min, int max, int startPosition, bool showDivider) : this(name, null,
            min, max, startPosition, showDivider)
        {
        }

        public MenuSliderItem(string name, string description, int min, int max, int startPosition) : this(name,
            description, min, max, startPosition, false)
        {
        }

        public MenuSliderItem(string name, string description, int min, int max, int startPosition, bool showDivider) :
            base(name, description)
        {
            Min = min;
            Max = max;
            ShowDivider = showDivider;
            Position = startPosition;
        }

        public int Min { get; }
        public int Max { get; } = 10;
        public bool ShowDivider { get; set; }
        public int Position { get; set; }

        public Icon SliderLeftIcon { get; set; } = Icon.NONE;
        public Icon SliderRightIcon { get; set; } = Icon.NONE;

        public Color BackgroundColor { get; set; } = Color.FromArgb(255, 24, 93, 151);
        public Color BarColor { get; set; } = Color.FromArgb(255, 53, 165, 223);

        /// <summary>
        ///     Maps '<see cref="float" /> <paramref name="val" />' to be a value between '<see cref="float" />
        ///     <paramref name="out_min" />' and '<see cref="float" /> <paramref name="out_max" />'.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="in_min"></param>
        /// <param name="in_max"></param>
        /// <param name="out_min"></param>
        /// <param name="out_max"></param>
        /// <returns></returns>
        private float Map(float val, float in_min, float in_max, float out_min, float out_max)
        {
            return (val - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        internal override void Draw(int indexOffset)
        {
            RightIcon = SliderRightIcon;
            Label = null;

            base.Draw(indexOffset);

            if (Position > Max || Position < Min) Position = (Max - Min) / 2;


            var yOffset = ParentMenu.MenuItemsYOffset + 1f -
                          RowHeight * MathUtil.Clamp(ParentMenu.Size, 0, ParentMenu.MaxItemsOnScreen);

            var width = 150f / MenuController.ScreenWidth;
            var height = 10f / MenuController.ScreenHeight;
            var y = (ParentMenu.Position.Y + (Index - indexOffset) * RowHeight + 20f + yOffset) /
                    MenuController.ScreenHeight;
            var x = (ParentMenu.Position.X + Width) / MenuController.ScreenWidth - width / 2f -
                    8f / MenuController.ScreenWidth;
            if (!ParentMenu.LeftAligned) x = width / 2f - 8f / MenuController.ScreenWidth;

            if (SliderLeftIcon != Icon.NONE && SliderRightIcon != Icon.NONE)
            {
                x -= 40f / MenuController.ScreenWidth;

                var leftColor = GetSpriteColour(SliderLeftIcon, Selected);
                var rightColor = GetSpriteColour(SliderRightIcon, Selected);


                API.SetScriptGfxAlign(ParentMenu.LeftAligned ? 76 : 82, 84);
                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                var textureDictionary = "commonmenu";
                if (SliderLeftIcon == Icon.MALE || SliderLeftIcon == Icon.FEMALE) textureDictionary = "mpleaderboard";

                if (ParentMenu.LeftAligned)
                    API.DrawSprite(textureDictionary, GetSpriteName(SliderLeftIcon, Selected),
                        x - (width / 2f + 4f / MenuController.ScreenWidth) - GetSpriteSize(SliderLeftIcon, true) / 2f,
                        y, GetSpriteSize(SliderLeftIcon, true), GetSpriteSize(SliderLeftIcon, false), 0f, leftColor,
                        leftColor, leftColor, 255);
                else
                    API.DrawSprite(textureDictionary, GetSpriteName(SliderLeftIcon, Selected),
                        x - (width + 4f / MenuController.ScreenWidth) - GetSpriteSize(SliderLeftIcon, true) -
                        20f / MenuController.ScreenWidth, y, GetSpriteSize(SliderLeftIcon, true),
                        GetSpriteSize(SliderLeftIcon, false), 0f, leftColor, leftColor, leftColor, 255);


                API.ResetScriptGfxAlign();
            }

            API.SetScriptGfxAlign(ParentMenu.LeftAligned ? 76 : 82, 84);
            API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

            #region drawing background bar and foreground bar

            // background
            API.DrawRect(x, y, width, height, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B,
                BackgroundColor.A);

            var xOffset =
                Map(Position, Min, Max, -(width / 4f * MenuController.ScreenWidth),
                    width / 4f * MenuController.ScreenWidth) / MenuController.ScreenWidth;

            // bar (foreground)
            if (!ParentMenu.LeftAligned)
                API.DrawRect(x - width / 2f + xOffset, y, width / 2f, height, BarColor.R, BarColor.G, BarColor.B,
                    BarColor.A);
            else
                API.DrawRect(x + xOffset, y, width / 2f, height, BarColor.R, BarColor.G, BarColor.B, BarColor.A);

            #endregion

            #region drawing divider

            if (ShowDivider)
            {
                if (!ParentMenu.LeftAligned)
                    API.DrawRect(x - width + 4f / MenuController.ScreenWidth, y, 4f / MenuController.ScreenWidth,
                        RowHeight / MenuController.ScreenHeight / 2f, 255, 255, 255, 255);
                else
                    API.DrawRect(x + 2f / MenuController.ScreenWidth, y, 4f / MenuController.ScreenWidth,
                        RowHeight / MenuController.ScreenHeight / 2f, 255, 255, 255, 255);
            }

            #endregion

            API.ResetScriptGfxAlign();
        }
    }
}