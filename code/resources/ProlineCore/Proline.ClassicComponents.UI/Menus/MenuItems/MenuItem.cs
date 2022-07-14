using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Proline.ClassicOnline.MScreen.Menus.MenuItems
{
    public class MenuItem
    {
        public enum Icon
        {
            NONE,
            LOCK,
            STAR,
            WARNING,
            CROWN,
            MEDAL_BRONZE,
            MEDAL_GOLD,
            MEDAL_SILVER,
            CASH,
            COKE,
            HEROIN,
            METH,
            WEED,
            AMMO,
            ARMOR,
            BARBER,
            CLOTHING,
            FRANKLIN,
            BIKE,
            CAR,
            GUN,
            HEALTH_HEART,
            MAKEUP_BRUSH,
            MASK,
            MICHAEL,
            TATTOO,
            TICK,
            TREVOR,
            FEMALE,
            MALE
        }

        protected const float Width = Menu.Width;
        protected const float RowHeight = 38f;

        /// <summary>
        ///     Creates a new <see cref="MenuItem" />.
        /// </summary>
        /// <param name="text"></param>
        public MenuItem(string text) : this(text, null)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="MenuItem" />.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="description"></param>
        public MenuItem(string text, string description)
        {
            Text = text;
            Description = description;
        }

        public string Text { get; set; }
        public string Label { get; set; }
        public Icon LeftIcon { get; set; }
        public Icon RightIcon { get; set; }
        public bool Enabled { get; set; } = true;
        public string Description { get; set; }

        public int Index
        {
            get
            {
                if (ParentMenu != null) return ParentMenu.GetMenuItems().IndexOf(this);
                return -1;
            }
        } //{ get; internal set; }

        public bool Selected
        {
            get
            {
                if (ParentMenu != null) return ParentMenu.CurrentIndex == Index;
                return false;
            }
        }

        public Menu ParentMenu { get; set; }
        public int PositionOnScreen { get; internal set; }

        protected string GetSpriteName(Icon icon, bool selected)
        {
            switch (icon)
            {
                case Icon.AMMO: return selected ? "shop_ammo_icon_b" : "shop_ammo_icon_a";
                case Icon.ARMOR: return selected ? "shop_armour_icon_b" : "shop_armour_icon_a";
                case Icon.BARBER: return selected ? "shop_barber_icon_b" : "shop_barber_icon_a";
                case Icon.BIKE: return selected ? "shop_garage_bike_icon_b" : "shop_garage_bike_icon_a";
                case Icon.CAR: return selected ? "shop_garage_icon_b" : "shop_garage_icon_a";
                case Icon.CASH: return "mp_specitem_cash";
                case Icon.CLOTHING: return selected ? "shop_clothing_icon_b" : "shop_clothing_icon_a";
                case Icon.COKE: return "mp_specitem_coke";
                case Icon.CROWN: return "mp_hostcrown";
                case Icon.FRANKLIN: return selected ? "shop_franklin_icon_b" : "shop_franklin_icon_a";
                case Icon.GUN: return selected ? "shop_gunclub_icon_b" : "shop_gunclub_icon_a";
                case Icon.HEALTH_HEART: return selected ? "shop_health_icon_b" : "shop_health_icon_a";
                case Icon.HEROIN: return "mp_specitem_heroin";
                case Icon.LOCK: return "shop_lock";
                case Icon.MAKEUP_BRUSH: return selected ? "shop_makeup_icon_b" : "shop_makeup_icon_a";
                case Icon.MASK: return selected ? "shop_mask_icon_b" : "shop_mask_icon_a";
                case Icon.MEDAL_BRONZE: return "mp_medal_bronze";
                case Icon.MEDAL_GOLD: return "mp_medal_gold";
                case Icon.MEDAL_SILVER: return "mp_medal_silver";
                case Icon.METH: return "mp_specitem_meth";
                case Icon.MICHAEL: return selected ? "shop_michael_icon_b" : "shop_michael_icon_a";
                case Icon.STAR: return "shop_new_star";
                case Icon.TATTOO: return selected ? "shop_tattoos_icon_b" : "shop_tattoos_icon_a";
                case Icon.TICK: return "shop_tick_icon";
                case Icon.TREVOR: return selected ? "shop_trevor_icon_b" : "shop_trevor_icon_a";
                case Icon.WARNING: return "mp_alerttriangle";
                case Icon.WEED: return "mp_specitem_weed";
                case Icon.MALE: return "leaderboard_male_icon";
                case Icon.FEMALE: return "leaderboard_female_icon";
            }

            return "";
        }

        protected float GetSpriteSize(Icon icon, bool width)
        {
            switch (icon)
            {
                case Icon.CASH:
                case Icon.COKE:
                case Icon.CROWN:
                case Icon.HEROIN:
                case Icon.METH:
                case Icon.WEED:
                    return 30f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);

                case Icon.STAR:
                    return 52f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);
                case Icon.MEDAL_SILVER:
                    return 22f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);
                default:
                    return 38f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);
            }
        }

        protected int GetSpriteColour(Icon icon, bool selected)
        {
            switch (icon)
            {
                case Icon.CROWN:
                case Icon.TICK:
                case Icon.MALE:
                case Icon.FEMALE:
                //return selected ? 0 : 255;
                case Icon.LOCK: return selected ? Enabled ? 0 : 50 : Enabled ? 255 : 109;
                default:
                    return 255;
            }
        }

        protected float GetSpriteX(Icon icon, bool leftAligned, bool leftSide)
        {
            switch (icon)
            {
                case Icon.AMMO:
                case Icon.ARMOR:
                case Icon.BARBER:
                case Icon.BIKE:
                case Icon.CAR:
                case Icon.CASH:
                case Icon.CLOTHING:
                case Icon.COKE:
                case Icon.CROWN:
                case Icon.FRANKLIN:
                case Icon.GUN:
                case Icon.HEALTH_HEART:
                case Icon.HEROIN:
                case Icon.LOCK:
                case Icon.MAKEUP_BRUSH:
                case Icon.MASK:
                case Icon.MEDAL_BRONZE:
                case Icon.MEDAL_GOLD:
                case Icon.MEDAL_SILVER:
                case Icon.METH:
                case Icon.MICHAEL:
                case Icon.STAR:
                case Icon.TATTOO:
                case Icon.TICK:
                case Icon.TREVOR:
                case Icon.WARNING:
                case Icon.WEED:
                case Icon.FEMALE:
                case Icon.MALE:
                    return leftSide ? leftAligned ? 20f / MenuController.ScreenWidth :
                        API.GetSafeZoneSize() - (Width - 20f) / MenuController.ScreenWidth :
                        leftAligned ? (Width - 20f) / MenuController.ScreenWidth :
                        API.GetSafeZoneSize() - 20f / MenuController.ScreenWidth;
            }

            return 0f;
        }

        protected float GetSpriteY(Icon icon)
        {
            switch (icon)
            {
                case Icon.AMMO:
                case Icon.ARMOR:
                case Icon.BARBER:
                case Icon.BIKE:
                case Icon.CAR:
                case Icon.CASH:
                case Icon.CLOTHING:
                case Icon.COKE:
                case Icon.CROWN:
                case Icon.FRANKLIN:
                case Icon.GUN:
                case Icon.HEALTH_HEART:
                case Icon.HEROIN:
                case Icon.LOCK:
                case Icon.MAKEUP_BRUSH:
                case Icon.MASK:
                case Icon.MEDAL_BRONZE:
                case Icon.MEDAL_GOLD:
                case Icon.MEDAL_SILVER:
                case Icon.METH:
                case Icon.MICHAEL:
                case Icon.STAR:
                case Icon.TATTOO:
                case Icon.TICK:
                case Icon.TREVOR:
                case Icon.WARNING:
                case Icon.WEED:
                case Icon.MALE:
                case Icon.FEMALE:
                    break;
            }

            return 0f;
        }


        /// <summary>
        ///     Draws the item on the screen.
        /// </summary>
        internal virtual void Draw(int indexOffset)
        {
            if (ParentMenu != null)
            {
                var yOffset = ParentMenu.MenuItemsYOffset + 1f -
                              RowHeight * MathUtil.Clamp(ParentMenu.Size, 0, ParentMenu.MaxItemsOnScreen);

                #region Background Rect

                API.SetScriptGfxAlign(ParentMenu.LeftAligned ? 76 : 82, 84);
                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                var x = (ParentMenu.Position.X + Width / 2f) / MenuController.ScreenWidth;
                var y = (ParentMenu.Position.Y + (Index - indexOffset) * RowHeight + 20f + yOffset) /
                        MenuController.ScreenHeight;
                var width = Width / MenuController.ScreenWidth;
                var height = RowHeight / MenuController.ScreenHeight;

                if (Selected) API.DrawRect(x, y, width, height, 255, 255, 255, 225);
                API.ResetScriptGfxAlign();

                #endregion

                #region Left Icon

                var textXOffset = 0f;
                if (LeftIcon != Icon.NONE)
                {
                    textXOffset = 25f;

                    API.SetScriptGfxAlign(76, 84);
                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                    var name = GetSpriteName(LeftIcon, Selected);
                    var spriteY = y; // GetSpriteY(LeftIcon);
                    var spriteX = GetSpriteX(LeftIcon, ParentMenu.LeftAligned, true);
                    var spriteHeight = GetSpriteSize(LeftIcon, false);
                    var spriteWidth = GetSpriteSize(LeftIcon, true);
                    var spriteColor = GetSpriteColour(LeftIcon, Selected);
                    var textureDictionary = "commonmenu";
                    if (LeftIcon == Icon.MALE || LeftIcon == Icon.FEMALE) textureDictionary = "mpleaderboard";

                    API.DrawSprite(textureDictionary, name, spriteX, spriteY, spriteWidth, spriteHeight, 0f,
                        spriteColor, spriteColor, spriteColor, 255);
                    API.ResetScriptGfxAlign();
                }

                #endregion

                var rightTextIconOffset = 0f;

                #region Right Icon

                if (RightIcon != Icon.NONE)
                {
                    rightTextIconOffset = 25f;

                    API.SetScriptGfxAlign(76, 84);
                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                    var name = GetSpriteName(RightIcon, Selected);
                    var spriteY = y; // GetSpriteY(RightIcon);
                    var spriteX = GetSpriteX(RightIcon, ParentMenu.LeftAligned, false);
                    var spriteHeight = GetSpriteSize(RightIcon, false);
                    var spriteWidth = GetSpriteSize(RightIcon, true);
                    var spriteColor = GetSpriteColour(RightIcon, Selected);
                    var textureDictionary = "commonmenu";
                    if (RightIcon == Icon.MALE || RightIcon == Icon.FEMALE) textureDictionary = "mpleaderboard";

                    API.DrawSprite(textureDictionary, name, spriteX, spriteY, spriteWidth, spriteHeight, 0f,
                        spriteColor, spriteColor, spriteColor, 255);
                    API.ResetScriptGfxAlign();
                }

                #endregion

                #region Text

                var font = 0;
                var textSize = 14f * 27f / MenuController.ScreenHeight;
                //float textSize = 0.34f;

                API.SetScriptGfxAlign(76, 84);
                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
                API.SetTextFont(font);
                API.SetTextScale(textSize, textSize);
                API.SetTextJustification(1);
                API.BeginTextCommandDisplayText("STRING");
                API.AddTextComponentSubstringPlayerName(Text ?? "N/A");
                var textColor = Selected ? Enabled ? 0 : 50 : Enabled ? 255 : 109;
                if (Selected || !Enabled) API.SetTextColour(textColor, textColor, textColor, 255);
                //selected ? (Enabled ? 0 : 50) : (Enabled ? 255 : 109);
                //if (Selected)
                //{
                //    if (Enabled)
                //        SetTextColour(textColor, textColor, textColor, 255);
                //    else
                //        SetTextColour(textColor, textColor, textColor, 255);
                //}
                //else
                //{
                //    if (!Enabled)
                //        SetTextColour(textColor, textColor, textColor, 255);
                //}
                var textMinX = textXOffset / MenuController.ScreenWidth + 10f / MenuController.ScreenWidth;
                var textMaxX = (Width - 10f) / MenuController.ScreenWidth;
                var textHeight = API.GetTextScaleHeight(textSize, font);
                var textY = y - 30f / 2f / MenuController.ScreenHeight;
                if (ParentMenu.LeftAligned)
                {
                    API.SetTextWrap(textMinX, textMaxX);
                    API.EndTextCommandDisplayText(textMinX, textY);
                }
                else
                {
                    textMinX = textXOffset / MenuController.ScreenWidth + API.GetSafeZoneSize() -
                               (Width - 10f) / MenuController.ScreenWidth;
                    textMaxX = API.GetSafeZoneSize() - 10f / MenuController.ScreenWidth;
                    API.SetTextWrap(textMinX, textMaxX);
                    API.EndTextCommandDisplayText(textMinX, textY);
                }

                API.ResetScriptGfxAlign();

                #endregion

                #region Label

                if (!string.IsNullOrEmpty(Label))
                {
                    API.SetScriptGfxAlign(76, 84);
                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                    API.BeginTextCommandDisplayText("STRING");
                    API.SetTextFont(font);
                    API.SetTextScale(textSize, textSize);
                    API.SetTextJustification(2);
                    API.AddTextComponentSubstringPlayerName(Label);
                    if (Selected || !Enabled) API.SetTextColour(textColor, textColor, textColor, 255);
                    //if (Selected)
                    //{
                    //    SetTextColour(0, 0, 0, 255);
                    //}
                    if (ParentMenu.LeftAligned)
                    {
                        API.SetTextWrap(0f, (490f - rightTextIconOffset) / MenuController.ScreenWidth);
                        API.EndTextCommandDisplayText((10f + rightTextIconOffset) / MenuController.ScreenWidth, textY);
                    }
                    else
                    {
                        API.SetTextWrap(0f,
                            API.GetSafeZoneSize() - (10f + rightTextIconOffset) / MenuController.ScreenWidth);
                        API.EndTextCommandDisplayText(0f, textY);
                    }

                    API.ResetScriptGfxAlign();
                }

                #endregion
            }
        }
    }
}