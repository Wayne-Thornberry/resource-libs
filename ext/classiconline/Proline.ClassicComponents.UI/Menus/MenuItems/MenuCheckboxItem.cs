using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MScreen.Menus;

namespace Proline.ClassicOnline.MScreen.Menus.MenuItems
{
    public class MenuCheckboxItem : MenuItem
    {
        public enum CheckboxStyle
        {
            Cross,
            Tick
        }

        /// <summary>
        ///     Creates a basic <see cref="MenuCheckboxItem" />.
        /// </summary>
        /// <param name="text"></param>
        public MenuCheckboxItem(string text) : this(text, null)
        {
        }

        /// <summary>
        ///     Creates a basic <see cref="MenuCheckboxItem" /> and sets the checked state to
        ///     <param name="_checked"></param>
        ///     's state.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="_checked"></param>
        public MenuCheckboxItem(string text, bool _checked) : this(text, null, _checked)
        {
        }

        /// <summary>
        ///     Creates a basic <see cref="MenuCheckboxItem" /> and adds an item description.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="description"></param>
        public MenuCheckboxItem(string text, string description) : this(text, description, false)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="MenuCheckboxItem" /> with all parameters set.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="description"></param>
        /// <param name="_checked"></param>
        public MenuCheckboxItem(string text, string description, bool _checked) : base(text, description)
        {
            Checked = _checked;
        }

        public bool Checked { get; set; }
        public CheckboxStyle Style { get; set; } = CheckboxStyle.Tick;


        private int GetSpriteColour()
        {
            return 255;
        }

        private string GetSpriteName()
        {
            if (Checked)
            {
                if (Style == CheckboxStyle.Tick)
                {
                    if (Selected) return "shop_box_tickb";
                    return "shop_box_tick";
                }

                if (Selected) return "shop_box_crossb";
                return "shop_box_cross";
            }

            if (Selected) return "shop_box_blankb";
            return "shop_box_blank";
        }

        private float GetSpriteX()
        {
            var leftSide = false;
            var leftAligned = ParentMenu.LeftAligned;
            return leftSide ? leftAligned ? 20f / MenuController.ScreenWidth :
                API.GetSafeZoneSize() - (Width - 20f) / MenuController.ScreenWidth :
                leftAligned ? (Width - 20f) / MenuController.ScreenWidth :
                API.GetSafeZoneSize() - 20f / MenuController.ScreenWidth;
        }

        internal override void Draw(int offset)
        {
            RightIcon = Icon.NONE;
            Label = null;

            base.Draw(offset);

            API.SetScriptGfxAlign(76, 84);
            API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

            var yOffset = ParentMenu.MenuItemsYOffset + 1f -
                          RowHeight * MathUtil.Clamp(ParentMenu.Size, 0, ParentMenu.MaxItemsOnScreen);

            var name = GetSpriteName();
            var spriteY = (ParentMenu.Position.Y + (Index - offset) * RowHeight + 20f + yOffset) /
                          MenuController.ScreenHeight;
            var spriteX = GetSpriteX();
            var spriteHeight = 45f / MenuController.ScreenHeight;
            var spriteWidth = 45f / MenuController.ScreenWidth;
            var color = GetSpriteColour();

            API.DrawSprite("commonmenu", name, spriteX, spriteY, spriteWidth, spriteHeight, 0f, color, color, color,
                255);
            API.ResetScriptGfxAlign();
        }
    }
}