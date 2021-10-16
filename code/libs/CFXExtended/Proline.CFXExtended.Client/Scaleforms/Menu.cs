

namespace Proline.CFXExtended.Core.Scaleforms
{
    //public class MenuCheckboxItem : MenuItem
    //{
    //    public bool Checked { get; set; } = false;
    //    public CheckboxStyle Style { get; set; } = CheckboxStyle.Tick;
    //    public enum CheckboxStyle
    //    {
    //        Cross,
    //        Tick
    //    }

    //    /// <summary>
    //    /// Creates a basic <see cref="MenuCheckboxItem"/>.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    public MenuCheckboxItem(string text) : this(text, null) { }
    //    /// <summary>
    //    /// Creates a basic <see cref="MenuCheckboxItem"/> and sets the checked state to <param name="_checked"></param>'s state.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    /// <param name="_checked"></param>
    //    public MenuCheckboxItem(string text, bool _checked) : this(text, null, _checked) { }
    //    /// <summary>
    //    /// Creates a basic <see cref="MenuCheckboxItem"/> and adds an item description.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    /// <param name="description"></param>
    //    public MenuCheckboxItem(string text, string description) : this(text, description, false) { }
    //    /// <summary>
    //    /// Creates a new <see cref="MenuCheckboxItem"/> with all parameters set.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    /// <param name="description"></param>
    //    /// <param name="_checked"></param>
    //    public MenuCheckboxItem(string text, string description, bool _checked) : base(text, description)
    //    {
    //        Checked = _checked;
    //    }


    //    int GetSpriteColour()
    //    {
    //        return 255;
    //    }

    //    string GetSpriteName()
    //    {
    //        if (Checked)
    //        {
    //            if (Style == CheckboxStyle.Tick)
    //            {
    //                if (Selected)
    //                {
    //                    return "shop_box_tickb";
    //                }
    //                return "shop_box_tick";
    //            }
    //            else
    //            {
    //                if (Selected)
    //                {
    //                    return "shop_box_crossb";
    //                }
    //                return "shop_box_cross";
    //            }
    //        }
    //        else
    //        {
    //            if (base.Selected)
    //            {
    //                return "shop_box_blankb";
    //            }
    //            return "shop_box_blank";
    //        }
    //    }

    //    float GetSpriteX()
    //    {
    //        bool leftSide = false;
    //        bool leftAligned = ParentMenu.LeftAligned;
    //        return leftSide ? (leftAligned ? (20f / MenuController.ScreenWidth) : API.GetSafeZoneSize() - ((Width - 20f) / MenuController.ScreenWidth)) : (leftAligned ? (Width - 20f) / MenuController.ScreenWidth : (API.GetSafeZoneSize() - (20f / MenuController.ScreenWidth)));
    //    }

    //    internal override void Draw(int offset)
    //    {
    //        RightIcon = Icon.NONE;
    //        Label = null;

    //        base.Draw(offset);

    //        API.SetScriptGfxAlign(76, 84);
    //        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //        float yOffset = ParentMenu.MenuItemsYOffset + 1f - (RowHeight * MathUtil.Clamp(ParentMenu.Size, 0, ParentMenu.MaxItemsOnScreen));

    //        string name = GetSpriteName();
    //        float spriteY = (ParentMenu.Position.Y + ((Index - offset) * RowHeight) + (20f) + yOffset) / MenuController.ScreenHeight;
    //        float spriteX = GetSpriteX();
    //        float spriteHeight = 45f / MenuController.ScreenHeight;
    //        float spriteWidth = 45f / MenuController.ScreenWidth;
    //        int color = GetSpriteColour();

    //        API.DrawSprite("commonmenu", name, spriteX, spriteY, spriteWidth, spriteHeight, 0f, color, color, color, 255);
    //        API.ResetScriptGfxAlign();

    //    }
    //}
    //public class MenuDynamicListItem : MenuItem
    //{
    //    public bool HideArrowsWhenNotSelected { get; set; } = false;
    //    public string CurrentItem { get; set; } = null;

    //    public delegate string ChangeItemCallback(MenuDynamicListItem item, bool left);

    //    public ChangeItemCallback Callback { get; set; }

    //    public MenuDynamicListItem(string text, string initialValue, ChangeItemCallback callback) : this(text, initialValue, callback, null) { }
    //    public MenuDynamicListItem(string text, string initialValue, ChangeItemCallback callback, string description) : base(text, description)
    //    {
    //        CurrentItem = initialValue;
    //        Callback = callback;
    //    }

    //    internal override void Draw(int indexOffset)
    //    {
    //        if (HideArrowsWhenNotSelected && !Selected)
    //        {
    //            Label = CurrentItem ?? "~r~N/A";
    //        }
    //        else
    //        {
    //            Label = $"~s~← {CurrentItem ?? "~r~N/A~s~"} ~s~→";
    //        }

    //        base.Draw(indexOffset);
    //    }

    //}
    //public class MenuItem
    //{
    //    public enum Icon
    //    {
    //        NONE,
    //        LOCK,
    //        STAR,
    //        WARNING,
    //        CROWN,
    //        MEDAL_BRONZE,
    //        MEDAL_GOLD,
    //        MEDAL_SILVER,
    //        CASH,
    //        COKE,
    //        HEROIN,
    //        METH,
    //        WEED,
    //        AMMO,
    //        ARMOR,
    //        BARBER,
    //        CLOTHING,
    //        FRANKLIN,
    //        BIKE,
    //        CAR,
    //        GUN,
    //        HEALTH_HEART,
    //        MAKEUP_BRUSH,
    //        MASK,
    //        MICHAEL,
    //        TATTOO,
    //        TICK,
    //        TREVOR,
    //        FEMALE,
    //        MALE
    //    }

    //    public string Text { get; set; }
    //    public string Label { get; set; }
    //    public Icon LeftIcon { get; set; }
    //    public Icon RightIcon { get; set; }
    //    public bool Enabled { get; set; } = true;
    //    public string Description { get; set; }
    //    public int Index { get { if (ParentMenu != null) return ParentMenu.GetMenuItems().IndexOf(this); return -1; } } //{ get; internal set; }
    //    public bool Selected { get { if (ParentMenu != null) { return ParentMenu.CurrentIndex == Index; } return false; } }
    //    public Menu ParentMenu { get; set; }
    //    public int PositionOnScreen { get; internal set; }
    //    protected const float Width = Menu.Width;
    //    protected const float RowHeight = 38f;

    //    // Allows you to attach data to a menu item if you want to identify the menu item without having to put identification info in the visible text or description.
    //    public dynamic ItemData { get; set; }

    //    /// <summary>
    //    /// Creates a new <see cref="MenuItem"/>.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    public MenuItem(string text) : this(text, null) { }

    //    /// <summary>
    //    /// Creates a new <see cref="MenuItem"/>.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    /// <param name="description"></param>
    //    public MenuItem(string text, string description)
    //    {
    //        Text = text;
    //        Description = description;
    //    }

    //    protected string GetSpriteName(Icon icon, bool selected)
    //    {
    //        switch (icon)
    //        {
    //            case Icon.AMMO: return selected ? "shop_ammo_icon_b" : "shop_ammo_icon_a";
    //            case Icon.ARMOR: return selected ? "shop_armour_icon_b" : "shop_armour_icon_a";
    //            case Icon.BARBER: return selected ? "shop_barber_icon_b" : "shop_barber_icon_a";
    //            case Icon.BIKE: return selected ? "shop_garage_bike_icon_b" : "shop_garage_bike_icon_a";
    //            case Icon.CAR: return selected ? "shop_garage_icon_b" : "shop_garage_icon_a";
    //            case Icon.CASH: return "mp_specitem_cash";
    //            case Icon.CLOTHING: return selected ? "shop_clothing_icon_b" : "shop_clothing_icon_a";
    //            case Icon.COKE: return "mp_specitem_coke";
    //            case Icon.CROWN: return "mp_hostcrown";
    //            case Icon.FRANKLIN: return selected ? "shop_franklin_icon_b" : "shop_franklin_icon_a";
    //            case Icon.GUN: return selected ? "shop_gunclub_icon_b" : "shop_gunclub_icon_a";
    //            case Icon.HEALTH_HEART: return selected ? "shop_health_icon_b" : "shop_health_icon_a";
    //            case Icon.HEROIN: return "mp_specitem_heroin";
    //            case Icon.LOCK: return "shop_lock";
    //            case Icon.MAKEUP_BRUSH: return selected ? "shop_makeup_icon_b" : "shop_makeup_icon_a";
    //            case Icon.MASK: return selected ? "shop_mask_icon_b" : "shop_mask_icon_a";
    //            case Icon.MEDAL_BRONZE: return "mp_medal_bronze";
    //            case Icon.MEDAL_GOLD: return "mp_medal_gold";
    //            case Icon.MEDAL_SILVER: return "mp_medal_silver";
    //            case Icon.METH: return "mp_specitem_meth";
    //            case Icon.MICHAEL: return selected ? "shop_michael_icon_b" : "shop_michael_icon_a";
    //            case Icon.STAR: return "shop_new_star";
    //            case Icon.TATTOO: return selected ? "shop_tattoos_icon_b" : "shop_tattoos_icon_a";
    //            case Icon.TICK: return "shop_tick_icon";
    //            case Icon.TREVOR: return selected ? "shop_trevor_icon_b" : "shop_trevor_icon_a";
    //            case Icon.WARNING: return "mp_alerttriangle";
    //            case Icon.WEED: return "mp_specitem_weed";
    //            case Icon.MALE: return "leaderboard_male_icon";
    //            case Icon.FEMALE: return "leaderboard_female_icon";
    //            default:
    //                break;
    //        }
    //        return "";
    //    }

    //    protected float GetSpriteSize(Icon icon, bool width)
    //    {
    //        switch (icon)
    //        {
    //            case Icon.CASH:
    //            case Icon.COKE:
    //            case Icon.CROWN:
    //            case Icon.HEROIN:
    //            case Icon.METH:
    //            case Icon.WEED:
    //                return 30f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);

    //            case Icon.STAR:
    //                return 52f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);
    //            case Icon.MEDAL_SILVER:
    //                return 22f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);
    //            default:
    //                return 38f / (width ? MenuController.ScreenWidth : MenuController.ScreenHeight);
    //        }
    //    }

    //    protected int GetSpriteColour(Icon icon, bool selected)
    //    {
    //        switch (icon)
    //        {
    //            case Icon.CROWN:
    //            case Icon.TICK:
    //            case Icon.MALE:
    //            case Icon.FEMALE:
    //            //return selected ? 0 : 255;
    //            case Icon.LOCK: return selected ? (Enabled ? 0 : 50) : (Enabled ? 255 : 109);
    //            default:
    //                return 255;
    //        }
    //    }

    //    protected float GetSpriteX(Icon icon, bool leftAligned, bool leftSide)
    //    {
    //        switch (icon)
    //        {
    //            case Icon.AMMO:
    //            case Icon.ARMOR:
    //            case Icon.BARBER:
    //            case Icon.BIKE:
    //            case Icon.CAR:
    //            case Icon.CASH:
    //            case Icon.CLOTHING:
    //            case Icon.COKE:
    //            case Icon.CROWN:
    //            case Icon.FRANKLIN:
    //            case Icon.GUN:
    //            case Icon.HEALTH_HEART:
    //            case Icon.HEROIN:
    //            case Icon.LOCK:
    //            case Icon.MAKEUP_BRUSH:
    //            case Icon.MASK:
    //            case Icon.MEDAL_BRONZE:
    //            case Icon.MEDAL_GOLD:
    //            case Icon.MEDAL_SILVER:
    //            case Icon.METH:
    //            case Icon.MICHAEL:
    //            case Icon.STAR:
    //            case Icon.TATTOO:
    //            case Icon.TICK:
    //            case Icon.TREVOR:
    //            case Icon.WARNING:
    //            case Icon.WEED:
    //            case Icon.FEMALE:
    //            case Icon.MALE:
    //                return leftSide ? (leftAligned ? (20f / MenuController.ScreenWidth) : API.GetSafeZoneSize() - ((Width - 20f) / MenuController.ScreenWidth)) : (leftAligned ? (Width - 20f) / MenuController.ScreenWidth : (API.GetSafeZoneSize() - (20f / MenuController.ScreenWidth)));
    //            default:
    //                break;
    //        }
    //        return 0f;
    //    }

    //    protected float GetSpriteY(Icon icon)
    //    {
    //        switch (icon)
    //        {
    //            case Icon.AMMO:
    //            case Icon.ARMOR:
    //            case Icon.BARBER:
    //            case Icon.BIKE:
    //            case Icon.CAR:
    //            case Icon.CASH:
    //            case Icon.CLOTHING:
    //            case Icon.COKE:
    //            case Icon.CROWN:
    //            case Icon.FRANKLIN:
    //            case Icon.GUN:
    //            case Icon.HEALTH_HEART:
    //            case Icon.HEROIN:
    //            case Icon.LOCK:
    //            case Icon.MAKEUP_BRUSH:
    //            case Icon.MASK:
    //            case Icon.MEDAL_BRONZE:
    //            case Icon.MEDAL_GOLD:
    //            case Icon.MEDAL_SILVER:
    //            case Icon.METH:
    //            case Icon.MICHAEL:
    //            case Icon.STAR:
    //            case Icon.TATTOO:
    //            case Icon.TICK:
    //            case Icon.TREVOR:
    //            case Icon.WARNING:
    //            case Icon.WEED:
    //            case Icon.MALE:
    //            case Icon.FEMALE:
    //                break;
    //            default:
    //                break;
    //        }
    //        return 0f;
    //    }


    //    /// <summary>
    //    /// Draws the item on the screen.
    //    /// </summary>
    //    internal virtual void Draw(int indexOffset)
    //    {
    //        if (ParentMenu != null)
    //        {
    //            float yOffset = ParentMenu.MenuItemsYOffset + 1f - (RowHeight * MathUtil.Clamp(ParentMenu.Size, 0, ParentMenu.MaxItemsOnScreen));

    //            #region Background Rect
    //            API.SetScriptGfxAlign(ParentMenu.LeftAligned ? 76 : 82, 84);
    //            API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //            float x = (ParentMenu.Position.X + (Width / 2f)) / MenuController.ScreenWidth;
    //            float y = (ParentMenu.Position.Y + ((Index - indexOffset) * RowHeight) + (20f) + yOffset) / MenuController.ScreenHeight;
    //            float width = Width / MenuController.ScreenWidth;
    //            float height = (RowHeight) / MenuController.ScreenHeight;

    //            if (Selected)
    //            {
    //                API.DrawRect(x, y, width, height, 255, 255, 255, 225);
    //            }
    //            API.ResetScriptGfxAlign();
    //            #endregion

    //            #region Left Icon
    //            float textXOffset = 0f;
    //            if (LeftIcon != Icon.NONE)
    //            {
    //                textXOffset = 25f;

    //                API.SetScriptGfxAlign(76, 84);
    //                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                string name = GetSpriteName(LeftIcon, Selected);
    //                float spriteY = y;// GetSpriteY(LeftIcon);
    //                float spriteX = GetSpriteX(LeftIcon, ParentMenu.LeftAligned, true);
    //                float spriteHeight = GetSpriteSize(LeftIcon, false);
    //                float spriteWidth = GetSpriteSize(LeftIcon, true);
    //                int spriteColor = GetSpriteColour(LeftIcon, Selected);
    //                string textureDictionary = "commonmenu";
    //                if (LeftIcon == Icon.MALE || LeftIcon == Icon.FEMALE)
    //                {
    //                    textureDictionary = "mpleaderboard";
    //                }

    //                API.DrawSprite(textureDictionary, name, spriteX, spriteY, spriteWidth, spriteHeight, 0f, spriteColor, spriteColor, spriteColor, 255);
    //                API.ResetScriptGfxAlign();
    //            }
    //            #endregion

    //            float rightTextIconOffset = 0f;
    //            #region Right Icon
    //            if (RightIcon != Icon.NONE)
    //            {
    //                rightTextIconOffset = 25f;

    //                API.SetScriptGfxAlign(76, 84);
    //                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                string name = GetSpriteName(RightIcon, Selected);
    //                float spriteY = y;// GetSpriteY(RightIcon);
    //                float spriteX = GetSpriteX(RightIcon, ParentMenu.LeftAligned, false);
    //                float spriteHeight = GetSpriteSize(RightIcon, false);
    //                float spriteWidth = GetSpriteSize(RightIcon, true);
    //                int spriteColor = GetSpriteColour(RightIcon, Selected);
    //                string textureDictionary = "commonmenu";
    //                if (RightIcon == Icon.MALE || RightIcon == Icon.FEMALE)
    //                {
    //                    textureDictionary = "mpleaderboard";
    //                }

    //                API.DrawSprite(textureDictionary, name, spriteX, spriteY, spriteWidth, spriteHeight, 0f, spriteColor, spriteColor, spriteColor, 255);
    //                API.ResetScriptGfxAlign();
    //            }
    //            #endregion

    //            #region Text
    //            int font = 0;
    //            float textSize = (14f * 27f) / MenuController.ScreenHeight;
    //            //float textSize = 0.34f;

    //            API.SetScriptGfxAlign(76, 84);
    //            API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //            API.SetTextFont(font);
    //            API.SetTextScale(textSize, textSize);
    //            API.SetTextJustification(1);
    //            API.BeginTextCommandDisplayText("STRING");
    //            API.AddTextComponentSubstringPlayerName(Text ?? "N/A");
    //            int textColor = Selected ? (Enabled ? 0 : 50) : (Enabled ? 255 : 109);
    //            if (Selected || !Enabled)
    //            {
    //                API.SetTextColour(textColor, textColor, textColor, 255);
    //            }
    //            //selected ? (Enabled ? 0 : 50) : (Enabled ? 255 : 109);
    //            //if (Selected)
    //            //{
    //            //    if (Enabled)
    //            //        SetTextColour(textColor, textColor, textColor, 255);
    //            //    else
    //            //        SetTextColour(textColor, textColor, textColor, 255);
    //            //}
    //            //else
    //            //{
    //            //    if (!Enabled)
    //            //        SetTextColour(textColor, textColor, textColor, 255);
    //            //}
    //            float textMinX = (textXOffset / MenuController.ScreenWidth) + (10f / MenuController.ScreenWidth);
    //            float textMaxX = (Width - 10f) / MenuController.ScreenWidth;
    //            float textHeight = API.GetTextScaleHeight(textSize, font);
    //            float textY = y - ((30f / 2f) / MenuController.ScreenHeight);
    //            if (ParentMenu.LeftAligned)
    //            {
    //                API.SetTextWrap(textMinX, textMaxX);
    //                API.EndTextCommandDisplayText(textMinX, textY);
    //            }
    //            else
    //            {
    //                textMinX = (textXOffset / MenuController.ScreenWidth) + API.GetSafeZoneSize() - ((Width - 10f) / MenuController.ScreenWidth);
    //                textMaxX = API.GetSafeZoneSize() - (10f / MenuController.ScreenWidth);
    //                API.SetTextWrap(textMinX, textMaxX);
    //                API.EndTextCommandDisplayText(textMinX, textY);
    //            }
    //            API.ResetScriptGfxAlign();

    //            #endregion

    //            #region Label
    //            if (!string.IsNullOrEmpty(Label))
    //            {
    //                API.SetScriptGfxAlign(76, 84);
    //                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                API.BeginTextCommandDisplayText("STRING");
    //                API.SetTextFont(font);
    //                API.SetTextScale(textSize, textSize);
    //                API.SetTextJustification(2);
    //                API.AddTextComponentSubstringPlayerName(Label);
    //                if (Selected || !Enabled)
    //                {
    //                    API.SetTextColour(textColor, textColor, textColor, 255);
    //                }
    //                //if (Selected)
    //                //{
    //                //    SetTextColour(0, 0, 0, 255);
    //                //}
    //                if (ParentMenu.LeftAligned)
    //                {
    //                    API.SetTextWrap(0f, ((490f - rightTextIconOffset) / MenuController.ScreenWidth));
    //                    API.EndTextCommandDisplayText((10f + rightTextIconOffset) / MenuController.ScreenWidth, textY);
    //                }
    //                else
    //                {
    //                    API.SetTextWrap(0f, API.GetSafeZoneSize() - ((10f + rightTextIconOffset) / MenuController.ScreenWidth));
    //                    API.EndTextCommandDisplayText(0f, textY);
    //                }

    //                API.ResetScriptGfxAlign();
    //            }
    //            #endregion



    //        }
    //    }

    //}
    //public class MenuListItem : MenuItem
    //{
    //    public int ListIndex { get; set; } = 0;
    //    public List<string> ListItems { get; set; } = new List<string>();
    //    public bool HideArrowsWhenNotSelected { get; set; } = false;
    //    public bool ShowOpacityPanel { get; set; } = false;
    //    public bool ShowColorPanel { get; set; } = false;
    //    public ColorPanelType ColorPanelColorType = ColorPanelType.Hair;
    //    public enum ColorPanelType
    //    {
    //        Hair,
    //        Makeup
    //    }
    //    public int ItemsCount => ListItems.Count;

    //    public string GetCurrentSelection()
    //    {
    //        if (ItemsCount > 0 && ListIndex >= 0 && ListIndex < ItemsCount)
    //        {
    //            return ListItems[ListIndex];
    //        }
    //        return null;
    //    }

    //    public MenuListItem(string text, List<string> items, int index) : this(text, items, index, null) { }
    //    public MenuListItem(string text, List<string> items, int index, string description) : base(text, description)
    //    {
    //        ListItems = items;
    //        ListIndex = index;
    //    }

    //    internal override void Draw(int indexOffset)
    //    {
    //        if (ItemsCount < 1)
    //        {
    //            // Add a dummy item to prevent the other while loops from freezing the game.
    //            ListItems.Add("N/A");
    //        }

    //        while (ListIndex < 0)
    //        {
    //            ListIndex += ItemsCount;
    //        }

    //        while (ListIndex >= ItemsCount)
    //        {
    //            ListIndex -= ItemsCount;
    //        }

    //        if (HideArrowsWhenNotSelected && !Selected)
    //        {
    //            Label = GetCurrentSelection() ?? "~r~N/A";
    //        }
    //        else
    //        {
    //            Label = $"~s~← {GetCurrentSelection() ?? "~r~N/A~s~"} ~s~→";
    //        }

    //        base.Draw(indexOffset);
    //    }

    //}
    //public class MenuSliderItem : MenuItem
    //{
    //    public int Min { get; private set; } = 0;
    //    public int Max { get; private set; } = 10;
    //    public bool ShowDivider { get; set; }
    //    public int Position { get; set; } = 0;

    //    public Icon SliderLeftIcon { get; set; } = Icon.NONE;
    //    public Icon SliderRightIcon { get; set; } = Icon.NONE;

    //    public Color BackgroundColor { get; set; } = Color.FromArgb(255, 24, 93, 151);
    //    public Color BarColor { get; set; } = Color.FromArgb(255, 53, 165, 223);

    //    public MenuSliderItem(string name, int min, int max, int startPosition) : this(name, min, max, startPosition, false) { }
    //    public MenuSliderItem(string name, int min, int max, int startPosition, bool showDivider) : this(name, null, min, max, startPosition, showDivider) { }
    //    public MenuSliderItem(string name, string description, int min, int max, int startPosition) : this(name, description, min, max, startPosition, false) { }
    //    public MenuSliderItem(string name, string description, int min, int max, int startPosition, bool showDivider) : base(name, description)
    //    {
    //        Min = min;
    //        Max = max;
    //        ShowDivider = showDivider;
    //        Position = startPosition;
    //    }

    //    /// <summary>
    //    /// Maps '<see cref="float"/> <paramref name="val"/>' to be a value between '<see cref="float"/> <paramref name="out_min"/>' and '<see cref="float"/> <paramref name="out_max"/>'.
    //    /// </summary>
    //    /// <param name="val"></param>
    //    /// <param name="in_min"></param>
    //    /// <param name="in_max"></param>
    //    /// <param name="out_min"></param>
    //    /// <param name="out_max"></param>
    //    /// <returns></returns>
    //    private float Map(float val, float in_min, float in_max, float out_min, float out_max)
    //    {
    //        return (val - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    //    }

    //    internal override void Draw(int indexOffset)
    //    {

    //        RightIcon = SliderRightIcon;
    //        Label = null;

    //        base.Draw(indexOffset);

    //        if (Position > Max || Position < Min)
    //        {
    //            Position = (Max - Min) / 2;
    //        }


    //        float yOffset = ParentMenu.MenuItemsYOffset + 1f - (RowHeight * MathUtil.Clamp(ParentMenu.Size, 0, ParentMenu.MaxItemsOnScreen));

    //        float width = 150f / MenuController.ScreenWidth;
    //        float height = 10f / MenuController.ScreenHeight;
    //        float y = (ParentMenu.Position.Y + ((Index - indexOffset) * RowHeight) + (20f) + yOffset) / MenuController.ScreenHeight;
    //        float x = (ParentMenu.Position.X + (Width)) / MenuController.ScreenWidth - (width / 2f) - (8f / MenuController.ScreenWidth);
    //        if (!ParentMenu.LeftAligned)
    //        {
    //            x = (width / 2f) - (8f / MenuController.ScreenWidth);
    //        }

    //        if (SliderLeftIcon != Icon.NONE && SliderRightIcon != Icon.NONE)
    //        {
    //            x -= 40f / MenuController.ScreenWidth;

    //            var leftColor = GetSpriteColour(SliderLeftIcon, Selected);
    //            var rightColor = GetSpriteColour(SliderRightIcon, Selected);


    //            API.SetScriptGfxAlign(ParentMenu.LeftAligned ? 76 : 82, 84);
    //            API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //            string textureDictionary = "commonmenu";
    //            if (SliderLeftIcon == Icon.MALE || SliderLeftIcon == Icon.FEMALE)
    //            {
    //                textureDictionary = "mpleaderboard";
    //            }

    //            if (ParentMenu.LeftAligned)
    //            {
    //                // left sprite left aligned.
    //                API.DrawSprite(textureDictionary, GetSpriteName(SliderLeftIcon, Selected), x - (width / 2f + (4f / MenuController.ScreenWidth)) - (GetSpriteSize(SliderLeftIcon, true) / 2f), y, GetSpriteSize(SliderLeftIcon, true), GetSpriteSize(SliderLeftIcon, false), 0f, leftColor, leftColor, leftColor, 255);

    //                // right sprite is managed by the regular function in MenuItem that handles the right icon.
    //            }
    //            else
    //            {
    //                // left sprite right aligned.
    //                API.DrawSprite(textureDictionary, GetSpriteName(SliderLeftIcon, Selected), x - (width + (4f / MenuController.ScreenWidth)) - GetSpriteSize(SliderLeftIcon, true) - (20f / MenuController.ScreenWidth), y, GetSpriteSize(SliderLeftIcon, true), GetSpriteSize(SliderLeftIcon, false), 0f, leftColor, leftColor, leftColor, 255);

    //                // right sprite is managed by the regular function in MenuItem that handles the right icon.
    //            }



    //            API.ResetScriptGfxAlign();
    //        }

    //        API.SetScriptGfxAlign(ParentMenu.LeftAligned ? 76 : 82, 84);
    //        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //        #region drawing background bar and foreground bar

    //        // background
    //        API.DrawRect(x, y, width, height, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);

    //        float xOffset = Map((float)Position, (float)Min, (float)Max, -((width / 4f) * MenuController.ScreenWidth), ((width / 4f) * MenuController.ScreenWidth)) / MenuController.ScreenWidth;

    //        // bar (foreground)
    //        if (!ParentMenu.LeftAligned)
    //            API.DrawRect(x - (width / 2f) + xOffset, y, width / 2f, height, BarColor.R, BarColor.G, BarColor.B, BarColor.A);
    //        else
    //            API.DrawRect(x + xOffset, y, width / 2f, height, BarColor.R, BarColor.G, BarColor.B, BarColor.A);

    //        #endregion

    //        #region drawing divider
    //        if (ShowDivider)
    //        {
    //            if (!ParentMenu.LeftAligned)
    //                API.DrawRect(x - width + (4f / MenuController.ScreenWidth), y, 4f / MenuController.ScreenWidth, RowHeight / MenuController.ScreenHeight / 2f, 255, 255, 255, 255);
    //            else
    //                API.DrawRect(x + (2f / MenuController.ScreenWidth), y, 4f / MenuController.ScreenWidth, RowHeight / MenuController.ScreenHeight / 2f, 255, 255, 255, 255);
    //        }
    //        #endregion
    //        API.ResetScriptGfxAlign();



    //    }
    //}
    public class Menu : ScaleformUI
    {
        public Menu(string scaleformId) : base("")
        {
        }
    }
    //public class Menu
    //{
    //    #region Setting up events

    //    #region delegates
    //    /// <summary>
    //    /// Triggered when a <see cref="MenuItem"/> is selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this event occurred.</param>
    //    /// <param name="menuItem">The <see cref="MenuItem"/> that was selected.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of this <see cref="MenuItem"/>.</param>
    //    public delegate void ItemSelectEvent(Menu menu, MenuItem menuItem, int itemIndex);

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuCheckboxItem"/> was toggled.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this event occurred.</param>
    //    /// <param name="menuItem">The <see cref="MenuCheckboxItem"/> that was toggled.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of this <see cref="MenuCheckboxItem"/>.</param>
    //    /// <param name="newCheckedState">The new <see cref="MenuCheckboxItem.Checked"/> state of this <see cref="MenuCheckboxItem"/>.</param>
    //    public delegate void CheckboxItemChangeEvent(Menu menu, MenuCheckboxItem menuItem, int itemIndex, bool newCheckedState);

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuListItem"/> is selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnListItemSelect"/> event occurred.</param>
    //    /// <param name="listItem">The <see cref="MenuListItem"/> that was selected.</param>
    //    /// <param name="selectedIndex">The <see cref="MenuListItem.ListIndex"/> of the <see cref="MenuListItem"/>.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of the <see cref="MenuListItem"/> in the <see cref="Menu"/>.</param>
    //    public delegate void ListItemSelectedEvent(Menu menu, MenuListItem listItem, int selectedIndex, int itemIndex);

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuListItem"/>'s index was changed.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnListIndexChange"/> event occurred.</param>
    //    /// <param name="listItem">The <see cref="MenuListItem"/> that was changed.</param>
    //    /// <param name="oldSelectionIndex">The old <see cref="MenuListItem.ListIndex"/> of the <see cref="MenuListItem"/>.</param>
    //    /// <param name="newSelectionIndex">The new <see cref="MenuListItem.ListIndex"/> of the <see cref="MenuListItem"/>.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of the <see cref="MenuListItem"/> in the <see cref="Menu"/>.</param>
    //    public delegate void ListItemIndexChangedEvent(Menu menu, MenuListItem listItem, int oldSelectionIndex, int newSelectionIndex, int itemIndex);

    //    /// <summary>
    //    /// Triggered when a <see cref="Menu"/> is closed.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> that was closed.</param>
    //    public delegate void MenuClosedEvent(Menu menu);

    //    /// <summary>
    //    /// Triggered when a <see cref="Menu"/> is opened.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> that has been opened.</param>
    //    public delegate void MenuOpenedEvent(Menu menu);

    //    /// <summary>
    //    /// Triggered when the <see cref="CurrentIndex"/> changes.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnIndexChange"/></param> event occurred.
    //    /// <param name="oldItem">The old <see cref="MenuItem"/> that was previously selected.</param>
    //    /// <param name="newItem">The new <see cref="MenuItem"/> that is now selected.</param>
    //    /// <param name="oldIndex">The old <see cref="MenuItem.Index"/> of this item.</param>
    //    /// <param name="newIndex">The new <see cref="MenuItem.Index"/> of this item.</param>
    //    public delegate void IndexChangedEvent(Menu menu, MenuItem oldItem, MenuItem newItem, int oldIndex, int newIndex);

    //    /// <summary>
    //    /// Triggered when the <see cref="MenuSliderItem.Position"/> changes.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnSliderPositionChange"/> event occurred.</param>
    //    /// <param name="sliderItem">The <see cref="MenuSliderItem>"/> that was changed.</param>
    //    /// <param name="oldPosition">The old position of the slider bar.</param>
    //    /// <param name="newPosition">The new position of the slider bar.</param>
    //    /// <param name="itemIndex">The index of this <see cref="MenuSliderItem"/>.</param>
    //    public delegate void SliderPositionChangedEvent(Menu menu, MenuSliderItem sliderItem, int oldPosition, int newPosition, int itemIndex);

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuSliderItem"/> was selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnSliderItemSelect"/> event occurred.</param>
    //    /// <param name="sliderItem">The <see cref="MenuSliderItem>"/> that was pressed.</param>
    //    /// <param name="sliderPosition">The current position of the slider bar.</param>
    //    /// <param name="itemIndex">The index of this <see cref="MenuSliderItem"/>.</param>
    //    public delegate void SliderItemSelectedEvent(Menu menu, MenuSliderItem sliderItem, int sliderPosition, int itemIndex);

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuDynamicListItem"/>'s value was changed.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnDynamicListItemCurrentItemChange"/> event occurred.</param>
    //    /// <param name="dynamicListItem">The <see cref="MenuDynamicListItem"/> that was changed.</param>
    //    /// <param name="oldValue">The old <see cref="MenuDynamicListItem.CurrentItem"/> of the <see cref="MenuDynamicListItem"/>.</param>
    //    /// <param name="newValue">The new <see cref="MenuDynamicListItem.CurrentItem"/> of the <see cref="MenuDynamicListItem"/>.</param>
    //    public delegate void DynamicListItemCurrentItemChangedEvent(Menu menu, MenuDynamicListItem dynamicListItem, string oldValue, string newValue);

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuDynamicListItem"/> is selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnDynamicListItemSelect"/> event occurred.</param>
    //    /// <param name="dynamicListItem">The <see cref="MenuDynamicListItem"/> that was selected.</param>
    //    /// <param name="currentItem">The <see cref="MenuDynamicListItem.CurrentItem"/> of the <see cref="MenuDynamicListItem"/> in the <see cref="Menu"/>.</param>
    //    public delegate void DynamicListItemSelectedEvent(Menu menu, MenuDynamicListItem dynamicListItem, string currentItem);
    //    #endregion

    //    #region events
    //    /// <summary>
    //    /// Triggered when a <see cref="MenuItem"/> is selected.
    //    /// Parameters: <see cref="Menu"/> parentMenu, <see cref="MenuItem"/> menuItem, <see cref="int"/> itemIndex.
    //    /// </summary>
    //    public event ItemSelectEvent OnItemSelect;

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuCheckboxItem"/> was toggled.
    //    /// Parameters: <see cref="Menu"/> parentMenu, <see cref="MenuCheckboxItem"/> menuItem, <see cref="int"/> itemIndex, <see cref="bool"/> newCheckedState.
    //    /// </summary>
    //    public event CheckboxItemChangeEvent OnCheckboxChange;

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuListItem"/> is selected.
    //    /// Parameters: <see cref="Menu"/> menu, <see cref="MenuListItem"/> listItem, <see cref="MenuListItem.ListIndex"/> selectedIndex, <see cref="int"/> itemIndex.
    //    /// </summary>
    //    public event ListItemSelectedEvent OnListItemSelect;

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuListItem"/>'s index was changed.
    //    /// Parameters: <see cref="Menu"/> menu, <see cref="MenuListItem"/> listItem, <see cref="MenuListItem.ListIndex"/> oldSelectionIndex, <see cref="int"/> newSelectionIndex, <see cref="int"/> itemIndex.
    //    /// </summary>
    //    public event ListItemIndexChangedEvent OnListIndexChange;

    //    /// <summary>
    //    /// Triggered when a <see cref="Menu"/> is closed.
    //    /// Parameters: <see cref="Menu"/> closedMenu.
    //    /// </summary>
    //    public event MenuClosedEvent OnMenuClose;

    //    /// <summary>
    //    /// Triggered when a <see cref="Menu"/> is opened.
    //    /// Parameters: <see cref="Menu"/> openedMenu.
    //    /// </summary>
    //    public event MenuOpenedEvent OnMenuOpen;

    //    /// <summary>
    //    /// Triggered when the <see cref="CurrentIndex"/> changes.
    //    /// Parameters: <see cref="Menu"/> menu, <see cref="MenuItem"/> oldSelectedItem, <see cref="MenuItem"/> newSelectedItem, <see cref="int"/> oldIndex, <see cref="int"/> newIndex.
    //    /// </summary>
    //    public event IndexChangedEvent OnIndexChange;

    //    /// <summary>
    //    /// Triggered when the <see cref="MenuSliderItem.Position"/> changes.
    //    /// Parameters: <see cref="Menu"/> menu, <see cref="MenuSliderItem"/> sliderItem, <see cref="int"/> oldPosition, <see cref="int"/> newPosition, <see cref="int"/> itemIndex
    //    /// </summary>
    //    public event SliderPositionChangedEvent OnSliderPositionChange;

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuSliderItem"/> was selected.
    //    /// Parameters: <see cref="Menu"/> menu, <see cref="MenuSliderItem"/> sliderItem, <see cref="int"/> sliderPosition, <see cref="int"/> itemIndex.
    //    /// </summary>
    //    public event SliderItemSelectedEvent OnSliderItemSelect;

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuDynamicListItem"/>'s value was changed.
    //    /// Parameters: <see cref="Menu"/> menu, <see cref="MenuListItem"/> dynamicListItem, <see cref="MenuDynamicListItem.CurrentItem"/> oldValue, <see cref="MenuDynamicListItem.CurrentItem"/> newValue.
    //    /// </summary>
    //    public event DynamicListItemCurrentItemChangedEvent OnDynamicListItemCurrentItemChange;

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuDynamicListItem"/> is selected.
    //    /// Parameters: <see cref="Menu"/> menu, <see cref="MenuDynamicListItem"/> dynamicListItem, <see cref="MenuDynamicListItem.CurrentItem"/> itemValue.
    //    /// </summary>
    //    public event DynamicListItemSelectedEvent OnDynamicListItemSelect;
    //    #endregion

    //    #region virtual voids
    //    /// <summary>
    //    /// Triggered when a <see cref="MenuItem"/> is selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this event occurred.</param>
    //    /// <param name="menuItem">The <see cref="MenuItem"/> that was selected.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of this <see cref="MenuItem"/>.</param>
    //    protected virtual void ItemSelectedEvent(MenuItem menuItem, int itemIndex)
    //    {
    //        OnItemSelect?.Invoke(this, menuItem, itemIndex);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuCheckboxItem"/> was toggled.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this event occurred.</param>
    //    /// <param name="menuItem">The <see cref="MenuCheckboxItem"/> that was toggled.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of this <see cref="MenuCheckboxItem"/>.</param>
    //    /// <param name="newCheckedState">The new <see cref="MenuCheckboxItem.Checked"/> state of this <see cref="MenuCheckboxItem"/>.</param>
    //    protected virtual void CheckboxChangedEvent(MenuCheckboxItem menuItem, int itemIndex, bool _checked)
    //    {
    //        OnCheckboxChange?.Invoke(this, menuItem, itemIndex, _checked);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuListItem"/> is selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnListItemSelect"/> event occurred.</param>
    //    /// <param name="listItem">The <see cref="MenuListItem"/> that was selected.</param>
    //    /// <param name="selectedIndex">The <see cref="MenuListItem.ListIndex"/> of the <see cref="MenuListItem"/>.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of the <see cref="MenuListItem"/> in the <see cref="Menu"/>.</param>
    //    protected virtual void ListItemSelectEvent(Menu menu, MenuListItem listItem, int selectedIndex, int itemIndex)
    //    {
    //        OnListItemSelect?.Invoke(menu, listItem, selectedIndex, itemIndex);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuListItem"/>'s index was changed.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnListIndexChange"/> event occurred.</param>
    //    /// <param name="listItem">The <see cref="MenuListItem"/> that was changed.</param>
    //    /// <param name="oldSelectionIndex">The old <see cref="MenuListItem.ListIndex"/> of the <see cref="MenuListItem"/>.</param>
    //    /// <param name="newSelectionIndex">The new <see cref="MenuListItem.ListIndex"/> of the <see cref="MenuListItem"/>.</param>
    //    /// <param name="itemIndex">The <see cref="MenuItem.Index"/> of the <see cref="MenuListItem"/> in the <see cref="Menu"/>.</param>
    //    protected virtual void ListItemIndexChangeEvent(Menu menu, MenuListItem listItem, int oldSelectionIndex, int newSelectionIndex, int itemIndex)
    //    {
    //        OnListIndexChange?.Invoke(menu, listItem, oldSelectionIndex, newSelectionIndex, itemIndex);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="Menu"/> is closed.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> that was closed.</param>
    //    protected virtual void MenuCloseEvent(Menu menu)
    //    {
    //        OnMenuClose?.Invoke(menu);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="Menu"/> is opened.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> that has been opened.</param>
    //    protected virtual void MenuOpenEvent(Menu menu)
    //    {
    //        OnMenuOpen?.Invoke(menu);
    //    }

    //    /// <summary>
    //    /// Triggered when the <see cref="CurrentIndex"/> changes.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnIndexChange"/> event occurred.</param>
    //    /// <param name="oldItem">The old <see cref="MenuItem"/> that was previously selected.</param>
    //    /// <param name="newItem">The new <see cref="MenuItem"/> that is now selected.</param>
    //    /// <param name="oldIndex">The old <see cref="MenuItem.Index"/> of this item.</param>
    //    /// <param name="newIndex">The new <see cref="MenuItem.Index"/> of this item.</param>
    //    protected virtual void IndexChangeEvent(Menu menu, MenuItem oldItem, MenuItem newItem, int oldIndex, int newIndex)
    //    {
    //        OnIndexChange?.Invoke(menu, oldItem, newItem, oldIndex, newIndex);
    //    }

    //    /// <summary>
    //    /// Triggered when the <see cref="MenuSliderItem.Position"/> changes.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnSliderPositionChange"/> event occurred.</param>
    //    /// <param name="sliderItem">The <see cref="MenuSliderItem>"/> that was changed.</param>
    //    /// <param name="oldPosition">The old position of the slider bar.</param>
    //    /// <param name="newPosition">The new position of the slider bar.</param>
    //    /// <param name="itemIndex">The index of this <see cref="MenuSliderItem"/>.</param>
    //    protected virtual void SliderItemChangedEvent(Menu menu, MenuSliderItem sliderItem, int oldPosition, int newPosition, int itemIndex)
    //    {
    //        OnSliderPositionChange?.Invoke(menu, sliderItem, oldPosition, newPosition, itemIndex);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuSliderItem"/> was selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnSliderItemSelect"/> event occurred.</param>
    //    /// <param name="sliderItem">The <see cref="MenuSliderItem>"/> that was pressed.</param>
    //    /// <param name="sliderPosition">The current position of the slider bar.</param>
    //    /// <param name="itemIndex">The index of this <see cref="MenuSliderItem"/>.</param>
    //    protected virtual void SliderSelectedEvent(Menu menu, MenuSliderItem sliderItem, int sliderPosition, int itemIndex)
    //    {
    //        OnSliderItemSelect?.Invoke(menu, sliderItem, sliderPosition, itemIndex);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuDynamicListItem"/>'s value was changed.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnDynamicListItemCurrentItemChange"/> event occurred.</param>
    //    /// <param name="dynamicListItem">The <see cref="MenuDynamicListItem"/> that was changed.</param>
    //    /// <param name="oldValue">The old <see cref="MenuDynamicListItem.CurrentItem"/> of the <see cref="MenuDynamicListItem"/>.</param>
    //    /// <param name="newValue">The new <see cref="MenuDynamicListItem.CurrentItem"/> of the <see cref="MenuDynamicListItem"/>.</param>
    //    protected virtual void DynamicListItemCurrentItemChanged(Menu menu, MenuDynamicListItem dynamicListItem, string oldValue, string newValue)
    //    {
    //        OnDynamicListItemCurrentItemChange?.Invoke(menu, dynamicListItem, oldValue, newValue);
    //    }

    //    /// <summary>
    //    /// Triggered when a <see cref="MenuDynamicListItem"/> is selected.
    //    /// </summary>
    //    /// <param name="menu">The <see cref="Menu"/> in which this <see cref="OnDynamicListItemSelect"/> event occurred.</param>
    //    /// <param name="dynamicListItem">The <see cref="MenuDynamicListItem"/> that was selected.</param>
    //    /// <param name="currentItem">The <see cref="MenuDynamicListItem.CurrentItem"/> of the <see cref="MenuDynamicListItem"/> in the <see cref="Menu"/>.</param>
    //    protected virtual void DynamicListItemSelectEvent(Menu menu, MenuDynamicListItem dynamicListItem, string currentItem)
    //    {
    //        OnDynamicListItemSelect?.Invoke(menu, dynamicListItem, currentItem);
    //    }

    //    #endregion

    //    #endregion

    //    #region constants or readonlys
    //    public const float Width = 500f;
    //    #endregion

    //    #region private variables
    //    private static SizeF headerSize = new SizeF(Width, 110f);

    //    public int ViewIndexOffset { get; private set; } = 0;

    //    private List<MenuItem> VisibleMenuItems
    //    {
    //        get
    //        {
    //            // Create a duplicate list, just in case the original list is modified while we're looping through it.
    //            if (filterActive)
    //            {
    //                var items = FilterItems.ToList().GetRange(ViewIndexOffset, Math.Min(MaxItemsOnScreen, Size - ViewIndexOffset));
    //                return items;
    //            }
    //            else
    //            {
    //                var items = GetMenuItems().ToList().GetRange(ViewIndexOffset, Math.Min(MaxItemsOnScreen, Size - ViewIndexOffset));
    //                return items;
    //            }

    //        }
    //    }

    //    private List<MenuItem> FilterItems { get; set; } = new List<MenuItem>();
    //    private List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    //    private readonly int ColorPanelScaleform = API.RequestScaleformMovie("COLOUR_SWITCHER_02"); // Could probably be improved, but was getting some glitchy results if it wasn't pre-loaded.
    //    private readonly int OpacityPanelScaleform = API.RequestScaleformMovie("COLOUR_SWITCHER_01"); // Could probably be improved, but was getting some glitchy results if it wasn't pre-loaded.
    //    #endregion

    //    #region Public Variables
    //    public string MenuTitle { get; set; }

    //    public string MenuSubtitle { get; set; }

    //    public KeyValuePair<string, string> HeaderTexture { get; set; } = new KeyValuePair<string, string>();

    //    public bool IgnoreDontOpenMenus { get; set; } = false;

    //    public int MaxItemsOnScreen { get; internal set; } = 10;

    //    public int Size => filterActive ? FilterItems.Count : MenuItems.Count;

    //    public bool Visible { get; set; } = false;

    //    public bool LeftAligned => MenuController.MenuAlignment == MenuController.MenuAlignmentOption.Left;

    //    public PointF Position { get; private set; } = new PointF(0f, 0f);

    //    public float MenuItemsYOffset { get; private set; } = 0f;

    //    public string CounterPreText { get; set; }

    //    public Menu ParentMenu { get; internal set; } = null;

    //    public int CurrentIndex { get; internal set; } = 0;

    //    public bool EnableInstructionalButtons { get; set; } = true;

    //    public float[] WeaponStats { get; private set; }
    //    public float[] WeaponComponentStats { get; private set; }
    //    public bool ShowWeaponStatsPanel { get; set; } = false;

    //    private bool filterActive = false;

    //    public Dictionary<Control, string> InstructionalButtons = new Dictionary<Control, string>() { { Control.FrontendAccept, API.GetLabelText("HUD_INPUT28") }, { Control.FrontendCancel, API.GetLabelText("HUD_INPUT53") } };

    //    public List<InstructionalButton> CustomInstructionalButtons = new List<InstructionalButton>();

    //    public struct InstructionalButton
    //    {
    //        public string controlString;
    //        public string instructionText;

    //        public InstructionalButton(string controlString, string instructionText)
    //        {
    //            this.controlString = controlString;
    //            this.instructionText = instructionText;
    //        }
    //    }

    //    public enum ControlPressCheckType
    //    {
    //        JUST_RELEASED,
    //        JUST_PRESSED,
    //        RELEASED,
    //        PRESSED
    //    }

    //    public struct ButtonPressHandler
    //    {
    //        // The control to listen for.
    //        internal Control control;
    //        // The type. 
    //        internal ControlPressCheckType pressType;
    //        // The function to call when the control is triggered.
    //        internal Action<Menu, Control> function;
    //        // Whether or not the control needs to be disabled if the menu is visible.
    //        internal bool disableControl;

    //        public ButtonPressHandler(Control control, ControlPressCheckType pressType, Action<Menu, Control> function, bool disableControl)
    //        {
    //            this.control = control;
    //            this.pressType = pressType;
    //            this.function = function;
    //            this.disableControl = disableControl;
    //        }
    //    }
    //    public List<ButtonPressHandler> ButtonPressHandlers = new List<ButtonPressHandler>();

    //    #endregion

    //    #region Constructors
    //    /// <summary>
    //    /// Creates a new <see cref="Menu"/>.
    //    /// </summary>
    //    /// <param name="name"></param>
    //    public Menu(string name) : this(name, null) { }

    //    /// <summary>
    //    /// Creates a new <see cref="Menu"/>.
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="subtitle"></param>
    //    public Menu(string name, string subtitle)
    //    {
    //        MenuTitle = name;
    //        MenuSubtitle = subtitle;
    //        this.SetWeaponStats(0f, 0f, 0f, 0f);
    //        this.SetWeaponComponentStats(0f, 0f, 0f, 0f);
    //    }
    //    #endregion

    //    #region Public functions
    //    /// <summary>
    //    /// Sets the max amount of visible items on screen at a time.
    //    /// Min = 3, max = 10.
    //    /// </summary>
    //    /// <param name="max">A value between 3 and 10 (inclusive).</param>
    //    public void SetMaxItemsOnScreen(int max)
    //    {
    //        if (max < 11 && max > 2)
    //        {
    //            MaxItemsOnScreen = max;
    //        }
    //    }

    //    /// <summary>
    //    /// Resets the index to 0
    //    /// </summary>
    //    public void RefreshIndex() => RefreshIndex(0, 0);
    //    public void RefreshIndex(int index) => RefreshIndex(index, index > MaxItemsOnScreen ? index - MaxItemsOnScreen : 0);
    //    public void RefreshIndex(int index, int viewOffset) { CurrentIndex = index; ViewIndexOffset = viewOffset; }

    //    /// <summary>
    //    /// Returns the menu items in this menu.
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<MenuItem> GetMenuItems()
    //    {
    //        return filterActive ? FilterItems.ToList() : MenuItems.ToList();
    //    }

    //    /// <summary>
    //    /// Removes all menu items.
    //    /// </summary>
    //    public void ClearMenuItems()
    //    {
    //        CurrentIndex = 0;
    //        ViewIndexOffset = 0;
    //        MenuItems.Clear();
    //        FilterItems.Clear();
    //    }
    //    /// <summary>
    //    /// Removes all menu items.
    //    /// </summary>
    //    public void ClearMenuItems(bool dontResetIndex)
    //    {
    //        if (!dontResetIndex)
    //        {
    //            CurrentIndex = 0;
    //            ViewIndexOffset = 0;
    //        }
    //        MenuItems.Clear();
    //        FilterItems.Clear();
    //    }

    //    /// <summary>
    //    /// Adds a <see cref="MenuItem"/> to this <see cref="Menu"/>.
    //    /// </summary>
    //    /// <param name="item"></param>
    //    public void AddMenuItem(MenuItem item)
    //    {
    //        MenuItems.Add(item);
    //        item.PositionOnScreen = item.Index;
    //        item.ParentMenu = this;
    //    }

    //    /// <summary>
    //    /// Removes the item at that index.
    //    /// </summary>
    //    /// <param name="itemIndex"></param>
    //    public void RemoveMenuItem(int itemIndex)
    //    {
    //        if (CurrentIndex >= itemIndex)
    //        {
    //            if (Size > CurrentIndex)
    //            {
    //                CurrentIndex--;
    //            }
    //            else
    //            {
    //                CurrentIndex = 0;
    //            }
    //        }
    //        if (itemIndex < Size && itemIndex > -1)
    //        {
    //            RemoveMenuItem(MenuItems[itemIndex]);
    //            RemoveMenuItem(MenuItems[itemIndex]);
    //        }
    //    }

    //    /// <summary>
    //    /// Removes the specified <see cref="MenuItem"/> from this <see cref="Menu"/>.
    //    /// </summary>
    //    /// <param name="item"></param>
    //    public void RemoveMenuItem(MenuItem item)
    //    {
    //        if (MenuItems.Contains(item))
    //        {
    //            if (CurrentIndex >= item.Index)
    //            {
    //                if (Size > CurrentIndex)
    //                {
    //                    CurrentIndex--;
    //                }
    //                else
    //                {
    //                    CurrentIndex = 0;
    //                }
    //            }
    //            MenuItems.Remove(item);
    //        }
    //    }

    //    /// <summary>
    //    /// Triggers the <see cref="ItemSelectedEvent(MenuItem, int)"/> event function.
    //    /// </summary>
    //    /// <param name="index"></param>
    //    public void SelectItem(int index)
    //    {
    //        if (!filterActive)
    //        {
    //            if (index > -1 && MenuItems.Count - 1 >= index)
    //            {
    //                SelectItem(MenuItems[index]);
    //            }
    //        }
    //        else
    //        {
    //            if (index > -1 && FilterItems.Count - 1 >= index)
    //            {
    //                SelectItem(FilterItems[index]);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Triggers the <see cref="ItemSelectedEvent(MenuItem, int)"/> event function.
    //    /// </summary>
    //    /// <param name="index"></param>
    //    public void SelectItem(MenuItem item)
    //    {
    //        if (item != null && item.Enabled)
    //        {
    //            if (item is MenuCheckboxItem checkbox)
    //            {
    //                checkbox.Checked = !checkbox.Checked;
    //                CheckboxChangedEvent(checkbox, item.Index, checkbox.Checked);
    //            }
    //            else if (item is MenuListItem listItem)
    //            {
    //                ListItemSelectEvent(this, listItem, listItem.ListIndex, listItem.Index);
    //            }
    //            else if (item is MenuSliderItem slider)
    //            {
    //                SliderSelectedEvent(this, slider, slider.Position, slider.Index);
    //            }
    //            else if (item is MenuDynamicListItem dynamicListItem)
    //            {
    //                DynamicListItemSelectEvent(this, dynamicListItem, dynamicListItem.CurrentItem);
    //            }
    //            else
    //            {
    //                ItemSelectedEvent(item, item.Index);
    //            }
    //            API.PlaySoundFrontend(-1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //            if (MenuController.MenuButtons.ContainsKey(item))
    //            {
    //                // this updates the parent menu.
    //                MenuController.AddSubmenu(MenuController.GetCurrentMenu(), MenuController.MenuButtons[item]);

    //                MenuController.GetCurrentMenu().CloseMenu();
    //                MenuController.MenuButtons[item].OpenMenu();
    //            }
    //        }
    //        else if (item != null && !item.Enabled)
    //            API.PlaySoundFrontend(-1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //    }

    //    /// <summary>
    //    /// Returns to the parent menu. If there's no parent menu, then the current menu just closes.
    //    /// </summary>
    //    public void GoBack()
    //    {
    //        API.PlaySoundFrontend(-1, "BACK", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //        CloseMenu();
    //        if (ParentMenu != null)
    //        {
    //            ParentMenu.OpenMenu();
    //        }
    //    }

    //    /// <summary>
    //    /// Closes the menu. Also triggers the <see cref="OnMenuClose"/> event.
    //    /// </summary>
    //    public void CloseMenu()
    //    {
    //        Visible = false;
    //        MenuCloseEvent(this);
    //    }

    //    /// <summary>
    //    /// Opens the menu and triggers the <see cref="OnMenuOpen"/> event.
    //    /// </summary>
    //    public void OpenMenu()
    //    {
    //        Visible = true;
    //        MenuOpenEvent(this);
    //    }

    //    /// <summary>
    //    /// Goes up one menu item if possible.
    //    /// </summary>
    //    public void GoUp()
    //    {
    //        if (Visible && Size > 1)
    //        {
    //            MenuItem oldItem;

    //            if (filterActive)
    //            {
    //                oldItem = FilterItems[CurrentIndex];
    //            }
    //            else
    //            {
    //                oldItem = MenuItems[CurrentIndex];
    //            }

    //            CurrentIndex--; if (CurrentIndex < 0)
    //            {
    //                CurrentIndex = Size - 1;
    //            }


    //            if (!VisibleMenuItems.Contains(filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex]))
    //            {
    //                ViewIndexOffset--;
    //                if (ViewIndexOffset < 0)
    //                {
    //                    ViewIndexOffset = Math.Max(Size - MaxItemsOnScreen, 0);
    //                }
    //            }

    //            IndexChangeEvent(this, oldItem, filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex], oldItem.Index, CurrentIndex);
    //            API.PlaySoundFrontend(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //        }
    //    }

    //    /// <summary>
    //    /// Goes down one menu item if possible.
    //    /// </summary>
    //    public void GoDown()
    //    {
    //        if (Visible && Size > 1)
    //        {
    //            MenuItem oldItem;

    //            if (filterActive)
    //            {
    //                oldItem = FilterItems[CurrentIndex];
    //            }
    //            else
    //            {
    //                oldItem = MenuItems[CurrentIndex];
    //            }

    //            CurrentIndex++;
    //            if (CurrentIndex >= Size)
    //            {
    //                CurrentIndex = 0;
    //            }
    //            if (!VisibleMenuItems.Contains(filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex]))
    //            {
    //                ViewIndexOffset++;
    //                if (CurrentIndex == 0)
    //                {
    //                    ViewIndexOffset = 0;
    //                }
    //            }
    //            IndexChangeEvent(this, oldItem, filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex], oldItem.Index, CurrentIndex);
    //            API.PlaySoundFrontend(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //        }
    //    }

    //    /// <summary>
    //    /// If the item is a <see cref="MenuListItem"/> or a <see cref="MenuSliderItem"/> then it'll go left if possible.
    //    /// </summary>
    //    public void GoLeft()
    //    {
    //        if (MenuController.AreMenuButtonsEnabled)
    //        {
    //            var item = filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex];
    //            if (item.Enabled && item is MenuListItem listItem)
    //            {
    //                if (listItem.ItemsCount > 0)
    //                {
    //                    int oldIndex = listItem.ListIndex;
    //                    int newIndex = oldIndex;
    //                    if (listItem.ListIndex < 1)
    //                    {
    //                        newIndex = listItem.ItemsCount - 1;
    //                    }
    //                    else
    //                    {
    //                        newIndex--;
    //                    }
    //                    listItem.ListIndex = newIndex;
    //                    ListItemIndexChangeEvent(this, listItem, oldIndex, newIndex, listItem.Index);
    //                    API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //                }
    //            }
    //            else if (item.Enabled && item is MenuSliderItem slider)
    //            {
    //                if (slider.Position > slider.Min)
    //                {
    //                    SliderItemChangedEvent(this, slider, slider.Position, slider.Position - 1, slider.Index);
    //                    slider.Position--;
    //                    API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //                }
    //                else
    //                {
    //                    API.PlaySoundFrontend(-1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //                }
    //            }
    //            else if (item.Enabled && item is MenuDynamicListItem dynList)
    //            {
    //                string oldValue = dynList.CurrentItem;
    //                string newSelectedItem = dynList.Callback(dynList, true);
    //                dynList.CurrentItem = newSelectedItem;
    //                DynamicListItemCurrentItemChanged(this, dynList, oldValue, newSelectedItem);
    //                API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// If the item is a <see cref="MenuListItem"/> or a <see cref="MenuSliderItem"/> then it'll go right if possible.
    //    /// </summary>
    //    public void GoRight()
    //    {
    //        if (MenuController.AreMenuButtonsEnabled)
    //        {
    //            var item = filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex];
    //            if (item.Enabled && item is MenuListItem listItem)
    //            {
    //                if (listItem.ItemsCount > 0)
    //                {
    //                    int oldIndex = listItem.ListIndex;
    //                    int newIndex = oldIndex;
    //                    if (listItem.ListIndex >= listItem.ItemsCount - 1)
    //                    {
    //                        newIndex = 0;
    //                    }
    //                    else
    //                    {
    //                        newIndex++;
    //                    }
    //                    listItem.ListIndex = newIndex;
    //                    ListItemIndexChangeEvent(this, listItem, oldIndex, newIndex, listItem.Index);
    //                    API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //                }
    //            }
    //            else if (item.Enabled && item is MenuSliderItem slider)
    //            {
    //                if (slider.Position < slider.Max)
    //                {
    //                    SliderItemChangedEvent(this, slider, slider.Position, slider.Position + 1, slider.Index);
    //                    slider.Position++;
    //                    API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //                }
    //                else
    //                {
    //                    API.PlaySoundFrontend(-1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //                }
    //            }
    //            else if (item.Enabled && item is MenuDynamicListItem dynList)
    //            {
    //                string oldValue = dynList.CurrentItem;
    //                string newSelectedItem = dynList.Callback(dynList, false);
    //                dynList.CurrentItem = newSelectedItem;
    //                DynamicListItemCurrentItemChanged(this, dynList, oldValue, newSelectedItem);
    //                API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Allows you to sort the menu items using your own compare function.
    //    /// </summary>
    //    /// <param name="compare"></param>
    //    public void SortMenuItems(Comparison<MenuItem> compare)
    //    {
    //        if (filterActive)
    //        {
    //            filterActive = false;
    //            FilterItems.Clear();
    //        }
    //        MenuItems.Sort(compare);
    //    }

    //    public void FilterMenuItems(Func<MenuItem, bool> predicate)
    //    {
    //        if (filterActive)
    //        {
    //            ResetFilter();
    //        }
    //        RefreshIndex(0, 0);
    //        ViewIndexOffset = 0;
    //        FilterItems = MenuItems.Where(i => predicate.Invoke(i)).ToList();
    //        filterActive = true;
    //    }

    //    public void ResetFilter()
    //    {
    //        RefreshIndex(0, 0);
    //        filterActive = false;
    //        FilterItems.Clear();
    //    }

    //    public void SetWeaponStats(float damage, float fireRate, float accuracy, float range)
    //    {
    //        WeaponStats = new float[4]
    //        {
    //            MathUtil.Clamp(damage, 0f, 1f),
    //            MathUtil.Clamp(fireRate, 0f, 1f),
    //            MathUtil.Clamp(accuracy, 0f, 1f),
    //            MathUtil.Clamp(range, 0f, 1f)
    //        };
    //    }

    //    public void SetWeaponComponentStats(float damage, float fireRate, float accuracy, float range)
    //    {
    //        WeaponComponentStats = new float[4]
    //        {
    //            MathUtil.Clamp(WeaponStats[0] + damage, 0f, 1f),
    //            MathUtil.Clamp(WeaponStats[1] + fireRate, 0f, 1f),
    //            MathUtil.Clamp(WeaponStats[2] + accuracy, 0f, 1f),
    //            MathUtil.Clamp(WeaponStats[3] + range, 0f, 1f)
    //        };
    //    }

    //    #endregion

    //    #region internal task functions
    //    /// <summary>
    //    /// Draws the menu title + subtitle, calls all Draw functions for all menu items and draws the description for the selected item.
    //    /// </summary>
    //    /// <returns></returns>
    //    internal async void Draw()
    //    {
    //        if (!Game.IsPaused && API.IsScreenFadedIn() && !API.IsPlayerSwitchInProgress() && !Game.PlayerPed.IsDead)
    //        {
    //            #region Listen for custom key presses.
    //            if (ButtonPressHandlers.Count > 0)
    //            {
    //                if (!MenuController.DisableMenuButtons)
    //                {
    //                    foreach (ButtonPressHandler handler in ButtonPressHandlers)
    //                    {
    //                        if (handler.disableControl)
    //                        {
    //                            Game.DisableControlThisFrame(0, handler.control);
    //                        }

    //                        switch (handler.pressType)
    //                        {
    //                            case ControlPressCheckType.JUST_PRESSED:
    //                                if (Game.IsControlJustPressed(0, handler.control) || Game.IsDisabledControlJustPressed(0, handler.control))
    //                                    handler.function.Invoke(this, handler.control);
    //                                break;
    //                            case ControlPressCheckType.JUST_RELEASED:
    //                                if (Game.IsControlJustReleased(0, handler.control) || Game.IsDisabledControlJustReleased(0, handler.control))
    //                                    handler.function.Invoke(this, handler.control);
    //                                break;
    //                            case ControlPressCheckType.PRESSED:
    //                                if (Game.IsControlPressed(0, handler.control) || Game.IsDisabledControlPressed(0, handler.control))
    //                                    handler.function.Invoke(this, handler.control);
    //                                break;
    //                            case ControlPressCheckType.RELEASED:
    //                                if (!Game.IsControlPressed(0, handler.control) && !Game.IsDisabledControlPressed(0, handler.control))
    //                                    handler.function.Invoke(this, handler.control);
    //                                break;
    //                            default:
    //                                break;
    //                        }
    //                    }
    //                }
    //            }
    //            #endregion

    //            MenuItemsYOffset = 0f;

    //            #region Draw Header
    //            if (!string.IsNullOrEmpty(MenuTitle))
    //            {
    //                #region Draw Header Background
    //                API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                float x = (Position.X + (headerSize.Width / 2f)) / MenuController.ScreenWidth;
    //                float y = (Position.Y + (headerSize.Height / 2f)) / MenuController.ScreenHeight;
    //                float width = headerSize.Width / MenuController.ScreenWidth;
    //                float height = headerSize.Height / MenuController.ScreenHeight;

    //                if (!string.IsNullOrEmpty(HeaderTexture.Key) && !string.IsNullOrEmpty(HeaderTexture.Value))
    //                {
    //                    if (!API.HasStreamedTextureDictLoaded(HeaderTexture.Key))
    //                    {
    //                        API.RequestStreamedTextureDict(HeaderTexture.Key, false);
    //                        while (!API.HasStreamedTextureDictLoaded(HeaderTexture.Key))
    //                        {
    //                            await BaseScript.Delay(0);
    //                        }
    //                    }
    //                    API.DrawSprite(HeaderTexture.Key, HeaderTexture.Value, x, y, width, height, 0f, 255, 255, 255, 255);
    //                }
    //                else
    //                {
    //                    API.DrawSprite(MenuController._texture_dict, MenuController._header_texture, x, y, width, height, 0f, 255, 255, 255, 255);
    //                }


    //                API.ResetScriptGfxAlign();
    //                #endregion

    //                #region Draw Header Menu Title
    //                int font = 1;
    //                float size = (45f * 27f) / MenuController.ScreenHeight;
    //                //float size = 1.1f;

    //                API.SetScriptGfxAlign(76, 84);
    //                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                API.BeginTextCommandDisplayText("STRING");
    //                API.SetTextFont(font);
    //                API.SetTextColour(255, 255, 255, 255);
    //                API.SetTextScale(size, size);
    //                API.SetTextJustification(0);
    //                API.AddTextComponentSubstringPlayerName(MenuTitle);
    //                if (LeftAligned)
    //                {
    //                    API.EndTextCommandDisplayText(((headerSize.Width / 2f) / MenuController.ScreenWidth), y - (API.GetTextScaleHeight(size, font) / 2f));
    //                }
    //                else
    //                {
    //                    API.EndTextCommandDisplayText(API.GetSafeZoneSize() - ((headerSize.Width / 2f) / MenuController.ScreenWidth), y - (API.GetTextScaleHeight(size, font) / 2f));
    //                }

    //                API.ResetScriptGfxAlign();

    //                MenuItemsYOffset = headerSize.Height;
    //                #endregion
    //            }
    //            #endregion

    //            #region Draw Subtitle
    //            {
    //                #region draw subtitle background
    //                API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                float bgHeight = 38f;


    //                float x = (Position.X + (headerSize.Width / 2f)) / MenuController.ScreenWidth;
    //                float y = ((Position.Y + MenuItemsYOffset + (bgHeight / 2f)) / MenuController.ScreenHeight);
    //                float width = headerSize.Width / MenuController.ScreenWidth;
    //                float height = bgHeight / MenuController.ScreenHeight;

    //                API.DrawRect(x, y, width, height, 0, 0, 0, 250);
    //                API.ResetScriptGfxAlign();
    //                #endregion

    //                #region draw subtitle text
    //                if (!string.IsNullOrEmpty(MenuSubtitle))
    //                {
    //                    int font = 0;
    //                    float size = (14f * 27f) / MenuController.ScreenHeight;
    //                    //float size = 0.34f;

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                    API.BeginTextCommandDisplayText("STRING");
    //                    API.SetTextFont(font);
    //                    API.SetTextScale(size, size);
    //                    API.SetTextJustification(1);
    //                    // Don't make the text blue if another color is used in the string.
    //                    if (MenuSubtitle.Contains("~") || string.IsNullOrEmpty(MenuTitle))
    //                    {
    //                        API.AddTextComponentSubstringPlayerName(MenuSubtitle.ToUpper());
    //                    }
    //                    else
    //                    {
    //                        API.AddTextComponentSubstringPlayerName("~HUD_COLOUR_HB_BLUE~" + MenuSubtitle.ToUpper());
    //                    }

    //                    if (LeftAligned)
    //                    {
    //                        API.EndTextCommandDisplayText(10f / MenuController.ScreenWidth, y - (API.GetTextScaleHeight(size, font) / 2f + (4f / MenuController.ScreenHeight)));
    //                    }
    //                    else
    //                    {
    //                        API.EndTextCommandDisplayText(API.GetSafeZoneSize() - ((headerSize.Width - 10f) / MenuController.ScreenWidth), y - (API.GetTextScaleHeight(size, font) / 2f + (4f / MenuController.ScreenHeight)));
    //                    }

    //                    API.ResetScriptGfxAlign();
    //                }
    //                #endregion

    //                #region draw counter + pre-counter text
    //                string counterText = $"{CounterPreText ?? ""}{CurrentIndex + 1} / {Size}";
    //                if (!string.IsNullOrEmpty(CounterPreText) || MaxItemsOnScreen < Size)
    //                {
    //                    int font = 0;
    //                    float size = (14f * 27f) / MenuController.ScreenHeight;
    //                    //float size = 0.34f;

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                    API.BeginTextCommandDisplayText("STRING");
    //                    API.SetTextFont(font);
    //                    API.SetTextScale(size, size);
    //                    API.SetTextJustification(2);
    //                    if (MenuSubtitle.Contains("~") || (CounterPreText ?? "").Contains("~") || string.IsNullOrEmpty(MenuTitle))
    //                    {
    //                        API.AddTextComponentSubstringPlayerName(counterText.ToUpper());
    //                    }
    //                    else
    //                    {
    //                        API.AddTextComponentSubstringPlayerName("~HUD_COLOUR_HB_BLUE~" + counterText.ToUpper());
    //                    }
    //                    if (LeftAligned)
    //                    {
    //                        API.SetTextWrap(0f, (485f / MenuController.ScreenWidth));
    //                        API.EndTextCommandDisplayText(10f / MenuController.ScreenWidth, y - (API.GetTextScaleHeight(size, font) / 2f + (4f / MenuController.ScreenHeight)));
    //                    }
    //                    else
    //                    {
    //                        API.SetTextWrap(0f, API.GetSafeZoneSize() - (10f / MenuController.ScreenWidth));
    //                        API.EndTextCommandDisplayText(0f, y - (API.GetTextScaleHeight(size, font) / 2f + (4f / MenuController.ScreenHeight)));
    //                    }

    //                    API.ResetScriptGfxAlign();
    //                }
    //                if (!string.IsNullOrEmpty(MenuSubtitle) || (CounterPreText != null || MaxItemsOnScreen < Size))
    //                {
    //                    MenuItemsYOffset += bgHeight - 1f;
    //                }

    //                #endregion
    //            }
    //            #endregion

    //            #region Draw menu items background gradient
    //            if (Size > 0)
    //            {
    //                API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                float bgHeight = 38f * MathUtil.Clamp(Size, 0, MaxItemsOnScreen);


    //                float x = (Position.X + (headerSize.Width / 2f)) / MenuController.ScreenWidth;
    //                float y = ((Position.Y + MenuItemsYOffset + ((bgHeight + 1f) / 2f)) / MenuController.ScreenHeight);
    //                float width = headerSize.Width / MenuController.ScreenWidth;
    //                float height = (bgHeight + 1f) / MenuController.ScreenHeight;

    //                //DrawSprite(MenuController._texture_dict, "gradient_bgd", x, y, width, height, 0f, 255, 255, 255, 255);
    //                API.DrawRect(x, y, width, height, 0, 0, 0, 180);
    //                API.ResetScriptGfxAlign();
    //                MenuItemsYOffset += bgHeight - 1f;
    //            }
    //            #endregion

    //            #region Draw menu items that are visible in the current view.
    //            if (Size > 0)
    //            {
    //                foreach (var item in VisibleMenuItems)
    //                {
    //                    item.Draw(ViewIndexOffset);
    //                }
    //            }
    //            #endregion

    //            #region Up Down overflow Indicator
    //            float descriptionYOffset = 0f;
    //            if (Size > 0)
    //            {
    //                if (Size > MaxItemsOnScreen)
    //                {
    //                    #region background
    //                    float width = Width / MenuController.ScreenWidth;
    //                    float height = 60f / MenuController.ScreenWidth;
    //                    float x = (Position.X + (Width / 2f)) / MenuController.ScreenWidth;
    //                    float y = MenuItemsYOffset / MenuController.ScreenHeight + (height / 2f) + (6f / MenuController.ScreenHeight);

    //                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                    API.DrawRect(x, y, width, height, 0, 0, 0, 180);
    //                    descriptionYOffset = height;// + (1f / MenuController.ScreenHeight);
    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                    #region up/down icons
    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    float xMin = 0f;
    //                    float xMax = Width / MenuController.ScreenWidth;
    //                    float xCenter = 250f / MenuController.ScreenWidth;
    //                    float yTop = y - (20f / MenuController.ScreenHeight);
    //                    float yBottom = y - (10f / MenuController.ScreenHeight);

    //                    API.BeginTextCommandDisplayText("STRING");
    //                    API.AddTextComponentSubstringPlayerName("↑");

    //                    API.SetTextFont(0);
    //                    API.SetTextScale(1f, (14f * 27f) / MenuController.ScreenHeight);
    //                    //SetTextScale(0.35f, 0.35f);
    //                    API.SetTextJustification(0);
    //                    if (LeftAligned)
    //                    {
    //                        API.SetTextWrap(xMin, xMax);
    //                        API.EndTextCommandDisplayText(xCenter, yTop);
    //                    }
    //                    else
    //                    {
    //                        xMin = API.GetSafeZoneSize() - ((Width - 10f) / MenuController.ScreenWidth);
    //                        xMax = API.GetSafeZoneSize() - (10f / MenuController.ScreenWidth);
    //                        xCenter = API.GetSafeZoneSize() - (250f / MenuController.ScreenWidth);
    //                        API.SetTextWrap(xMin, xMax);
    //                        API.EndTextCommandDisplayText(xCenter, yTop);
    //                    }

    //                    API.BeginTextCommandDisplayText("STRING");
    //                    API.AddTextComponentSubstringPlayerName("↓");

    //                    API.SetTextFont(0);
    //                    API.SetTextScale(1f, (14f * 27f) / MenuController.ScreenHeight);
    //                    //SetTextScale(0.35f, 0.35f);
    //                    API.SetTextJustification(0);
    //                    if (LeftAligned)
    //                    {
    //                        API.SetTextWrap(xMin, xMax);
    //                        API.EndTextCommandDisplayText(xCenter, yBottom);
    //                    }
    //                    else
    //                    {
    //                        API.SetTextWrap(xMin, xMax);
    //                        API.EndTextCommandDisplayText(xCenter, yBottom);
    //                    }

    //                    API.ResetScriptGfxAlign();
    //                    #endregion
    //                }
    //            }

    //            #endregion

    //            #region Draw Description
    //            if (Size > 0)
    //            {
    //                if (!string.IsNullOrEmpty(filterActive ? FilterItems[CurrentIndex].Description : MenuItems[CurrentIndex].Description))
    //                {
    //                    #region description text
    //                    int font = 0;
    //                    float textSize = (14f * 27f) / MenuController.ScreenHeight;
    //                    //float textSize = 0.35f;

    //                    float textMinX = 0f + (10f / MenuController.ScreenWidth);
    //                    float textMaxX = Width / MenuController.ScreenWidth - (10f / MenuController.ScreenWidth);
    //                    float textY = MenuItemsYOffset / MenuController.ScreenHeight + (16f / MenuController.ScreenHeight) + descriptionYOffset;

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                    API.BeginTextCommandDisplayText("CELL_EMAIL_BCON");
    //                    API.SetTextFont(font);
    //                    API.SetTextScale(textSize, textSize);
    //                    API.SetTextJustification(1);
    //                    string text = filterActive ? FilterItems[CurrentIndex].Description : MenuItems[CurrentIndex].Description;
    //                    foreach (string s in CitizenFX.Core.UI.Screen.StringToArray(text))
    //                    {
    //                        API.AddTextComponentSubstringPlayerName(s);
    //                    }
    //                    float textHeight = API.GetTextScaleHeight(textSize, font);
    //                    if (LeftAligned)
    //                    {
    //                        API.SetTextWrap(textMinX, textMaxX);
    //                        API.EndTextCommandDisplayText(textMinX, textY);
    //                    }
    //                    else
    //                    {
    //                        textMinX = API.GetSafeZoneSize() - ((Width - 10f) / MenuController.ScreenWidth);
    //                        textMaxX = API.GetSafeZoneSize() - (10f / MenuController.ScreenWidth);
    //                        API.SetTextWrap(textMinX, textMaxX);
    //                        API.EndTextCommandDisplayText(textMinX, textY);
    //                    }

    //                    API.ResetScriptGfxAlign();

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                    API.BeginTextCommandLineCount("CELL_EMAIL_BCON");
    //                    API.SetTextScale(textSize, textSize);
    //                    API.SetTextJustification(1);
    //                    API.SetTextFont(font);
    //                    int lineCount = 1;
    //                    foreach (string s in CitizenFX.Core.UI.Screen.StringToArray(text))
    //                    {
    //                        API.AddTextComponentSubstringPlayerName(s);
    //                    }
    //                    if (LeftAligned)
    //                    {
    //                        API.SetTextWrap(textMinX, textMaxX);
    //                        lineCount = API.GetTextScreenLineCount(textMinX, textY);
    //                    }
    //                    else
    //                    {
    //                        API.SetTextWrap(textMinX, textMaxX);
    //                        lineCount = API.GetTextScreenLineCount(textMinX, textY);
    //                    }

    //                    API.ResetScriptGfxAlign();

    //                    #endregion

    //                    #region background
    //                    float descWidth = Width / MenuController.ScreenWidth;
    //                    float descHeight = (textHeight + 0.005f) * lineCount + (8f / MenuController.ScreenHeight) + (2.5f / MenuController.ScreenHeight);
    //                    float descX = (Position.X + (Width / 2f)) / MenuController.ScreenWidth;
    //                    float descY = textY - (6f / MenuController.ScreenHeight) + (descHeight / 2f);

    //                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

    //                    API.DrawRect(descX, descY - (descHeight / 2f) + (2f / MenuController.ScreenHeight), descWidth, 4f / MenuController.ScreenHeight, 0, 0, 0, 200);
    //                    API.DrawRect(descX, descY, descWidth, descHeight, 0, 0, 0, 180);

    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                    descriptionYOffset += descY + (descHeight / 2f) - (4f / MenuController.ScreenHeight);
    //                }
    //                else
    //                {
    //                    descriptionYOffset += MenuItemsYOffset / MenuController.ScreenHeight + (2f / MenuController.ScreenHeight) + descriptionYOffset;
    //                }
    //            }

    //            #endregion

    //            #region Draw Weapon Stats
    //            {
    //                if (Size > 0)
    //                {
    //                    var currentItem = filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex];
    //                    if (currentItem is MenuListItem listItem)
    //                    {
    //                        if (listItem.ShowColorPanel || listItem.ShowOpacityPanel)
    //                        {
    //                            goto SKIP_WEAPON_STATS;
    //                        }
    //                    }
    //                }

    //                if (ShowWeaponStatsPanel)
    //                {
    //                    float textSize = (14f * 27f) / MenuController.ScreenHeight;
    //                    float width = Width / MenuController.ScreenWidth;
    //                    float height = (140f) / MenuController.ScreenHeight;
    //                    float x = ((Width / 2f) / MenuController.ScreenWidth);
    //                    float y = descriptionYOffset + (height / 2f) + (8f / MenuController.ScreenHeight);
    //                    if (Size > MaxItemsOnScreen)
    //                    {
    //                        y -= (30f / MenuController.ScreenHeight);
    //                    }


    //                    #region background
    //                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    API.DrawRect(x, y, width, height, 0, 0, 0, 180);
    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                    float bgStatBarWidth = (Width / 2f) / MenuController.ScreenWidth;
    //                    float bgStatBarX = x + (bgStatBarWidth / 2f) - (10f / MenuController.ScreenWidth);

    //                    if (!LeftAligned)
    //                    {
    //                        bgStatBarX = x - (bgStatBarWidth / 2f) - (10f / MenuController.ScreenWidth);
    //                    }
    //                    float barWidth;
    //                    float componentBarWidth;
    //                    float barY = y - (height / 2f) + (25f / MenuController.ScreenHeight);
    //                    float bgStatBarHeight = 10f / MenuController.ScreenHeight;
    //                    float barX;
    //                    float componentBarX;
    //                    #region damage bar
    //                    barWidth = bgStatBarWidth * WeaponStats[0];
    //                    componentBarWidth = bgStatBarWidth * WeaponComponentStats[0];
    //                    if (LeftAligned)
    //                    {
    //                        barX = bgStatBarX - (bgStatBarWidth / 2f) + (barWidth / 2f);
    //                        componentBarX = bgStatBarX - (bgStatBarWidth / 2f) + (componentBarWidth / 2f);
    //                    }
    //                    else
    //                    {
    //                        barX = (barWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                        componentBarX = (componentBarWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                    }
    //                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    // bar bg
    //                    API.DrawRect(bgStatBarX, barY, bgStatBarWidth, bgStatBarHeight, 100, 100, 100, 180);
    //                    // component stats
    //                    API.DrawRect(componentBarX, barY, componentBarWidth, bgStatBarHeight, 93, 182, 229, 255);
    //                    // real bar
    //                    API.DrawRect(barX, barY, barWidth, bgStatBarHeight, 255, 255, 255, 255);
    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                    #region fire rate bar
    //                    barWidth = bgStatBarWidth * WeaponStats[1];
    //                    componentBarWidth = bgStatBarWidth * WeaponComponentStats[1];
    //                    barY += 30f / MenuController.ScreenHeight;
    //                    if (LeftAligned)
    //                    {
    //                        barX = bgStatBarX - (bgStatBarWidth / 2f) + (barWidth / 2f);
    //                        componentBarX = bgStatBarX - (bgStatBarWidth / 2f) + (componentBarWidth / 2f);
    //                    }
    //                    else
    //                    {
    //                        barX = (barWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                        componentBarX = (componentBarWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                    }
    //                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    // bar bg
    //                    API.DrawRect(bgStatBarX, barY, bgStatBarWidth, bgStatBarHeight, 100, 100, 100, 180);
    //                    // component stats
    //                    API.DrawRect(componentBarX, barY, componentBarWidth, bgStatBarHeight, 93, 182, 229, 255);
    //                    // real bar
    //                    API.DrawRect(barX, barY, barWidth, bgStatBarHeight, 255, 255, 255, 255);
    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                    #region accuracy bar
    //                    barWidth = bgStatBarWidth * WeaponStats[2];
    //                    componentBarWidth = bgStatBarWidth * WeaponComponentStats[2];
    //                    barY += 30f / MenuController.ScreenHeight;
    //                    if (LeftAligned)
    //                    {
    //                        barX = bgStatBarX - (bgStatBarWidth / 2f) + (barWidth / 2f);
    //                        componentBarX = bgStatBarX - (bgStatBarWidth / 2f) + (componentBarWidth / 2f);
    //                    }
    //                    else
    //                    {
    //                        barX = (barWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                        componentBarX = (componentBarWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                    }
    //                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    // bar bg
    //                    API.DrawRect(bgStatBarX, barY, bgStatBarWidth, bgStatBarHeight, 100, 100, 100, 180);
    //                    // component stats
    //                    API.DrawRect(componentBarX, barY, componentBarWidth, bgStatBarHeight, 93, 182, 229, 255);
    //                    // real bar
    //                    API.DrawRect(barX, barY, barWidth, bgStatBarHeight, 255, 255, 255, 255);
    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                    #region range bar
    //                    barWidth = bgStatBarWidth * WeaponStats[3];
    //                    componentBarWidth = bgStatBarWidth * WeaponComponentStats[3];
    //                    barY += 30f / MenuController.ScreenHeight;
    //                    if (LeftAligned)
    //                    {
    //                        barX = bgStatBarX - (bgStatBarWidth / 2f) + (barWidth / 2f);
    //                        componentBarX = bgStatBarX - (bgStatBarWidth / 2f) + (componentBarWidth / 2f);
    //                    }
    //                    else
    //                    {
    //                        barX = (barWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                        componentBarX = (componentBarWidth * 1.5f) - bgStatBarWidth - (10f / MenuController.ScreenWidth);
    //                    }
    //                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    // bar bg
    //                    API.DrawRect(bgStatBarX, barY, bgStatBarWidth, bgStatBarHeight, 100, 100, 100, 180);
    //                    // component stats
    //                    API.DrawRect(componentBarX, barY, componentBarWidth, bgStatBarHeight, 93, 182, 229, 255);
    //                    // real bar
    //                    API.DrawRect(barX, barY, barWidth, bgStatBarHeight, 255, 255, 255, 255);
    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                    #region weapon stats text
    //                    float textX = LeftAligned ? x - (width / 2f) + (10f / MenuController.ScreenWidth) : API.GetSafeZoneSize() - ((Width - 10f) / MenuController.ScreenWidth);
    //                    float textY = y - (height / 2f) + (10f / MenuController.ScreenHeight);

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    API.BeginTextCommandDisplayText("PM_DAMAGE");
    //                    API.SetTextJustification(1);
    //                    API.SetTextScale(textSize, textSize);

    //                    API.EndTextCommandDisplayText(textX, textY);
    //                    API.ResetScriptGfxAlign();

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    API.BeginTextCommandDisplayText("PM_FIRERATE");
    //                    API.SetTextJustification(1);
    //                    API.SetTextScale(textSize, textSize);
    //                    textY += 30f / MenuController.ScreenHeight;
    //                    API.EndTextCommandDisplayText(textX, textY);
    //                    API.ResetScriptGfxAlign();

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    API.BeginTextCommandDisplayText("PM_ACCURACY");
    //                    API.SetTextJustification(1);
    //                    API.SetTextScale(textSize, textSize);
    //                    textY += 30f / MenuController.ScreenHeight;
    //                    API.EndTextCommandDisplayText(textX, textY);
    //                    API.ResetScriptGfxAlign();

    //                    API.SetScriptGfxAlign(76, 84);
    //                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                    API.BeginTextCommandDisplayText("PM_RANGE");
    //                    API.SetTextJustification(1);
    //                    API.SetTextScale(textSize, textSize);
    //                    textY += 30f / MenuController.ScreenHeight;
    //                    API.EndTextCommandDisplayText(textX, textY);
    //                    API.ResetScriptGfxAlign();
    //                    #endregion

    //                }
    //            }


    //        SKIP_WEAPON_STATS:
    //            #endregion

    //            #region Draw Color and opacity palletes
    //            if (Size > 0)
    //            {
    //                var currentItem = filterActive ? FilterItems[CurrentIndex] : MenuItems[CurrentIndex];
    //                if (currentItem is MenuListItem listItem)
    //                {
    //                    /// OPACITY PANEL
    //                    if (listItem.ShowOpacityPanel)
    //                    {
    //                        API.BeginScaleformMovieMethod(OpacityPanelScaleform, "SET_TITLE");
    //                        API.PushScaleformMovieMethodParameterString("Opacity");
    //                        API.PushScaleformMovieMethodParameterString("");
    //                        API.ScaleformMovieMethodAddParamInt(listItem.ListIndex * 10); // opacity percent
    //                        API.EndScaleformMovieMethod();

    //                        float width = Width / MenuController.ScreenWidth;
    //                        float height = ((700f / 500f) * Width) / MenuController.ScreenHeight;
    //                        float x = ((Width / 2f) / MenuController.ScreenWidth);
    //                        float y = descriptionYOffset + (height / 2f) + (4f / MenuController.ScreenHeight);
    //                        if (Size > MaxItemsOnScreen)
    //                        {
    //                            y -= (30f / MenuController.ScreenHeight);
    //                        }

    //                        API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                        API.DrawScaleformMovie(OpacityPanelScaleform, x, y, width, height, 255, 255, 255, 255, 0);
    //                        API.ResetScriptGfxAlign();
    //                    }

    //                    /// COLOR PALLETE
    //                    else if (listItem.ShowColorPanel)
    //                    {
    //                        API.BeginScaleformMovieMethod(ColorPanelScaleform, "SET_TITLE");
    //                        API.PushScaleformMovieMethodParameterString("Opacity");
    //                        API.BeginTextCommandScaleformString("FACE_COLOUR");
    //                        API.AddTextComponentInteger(listItem.ListIndex + 1);
    //                        API.AddTextComponentInteger(listItem.ItemsCount);
    //                        API.EndTextCommandScaleformString();
    //                        API.ScaleformMovieMethodAddParamInt(0); // opacity percent unused
    //                        API.ScaleformMovieMethodAddParamBool(true);
    //                        API.EndScaleformMovieMethod();

    //                        API.BeginScaleformMovieMethod(ColorPanelScaleform, "SET_DATA_SLOT_EMPTY");
    //                        API.EndScaleformMovieMethod();

    //                        for (int i = 0; i < 64; i++)
    //                        {
    //                            var r = 0;
    //                            var g = 0;
    //                            var b = 0;
    //                            if (listItem.ColorPanelColorType == MenuListItem.ColorPanelType.Hair)
    //                            {
    //                                API.GetHairRgbColor(i, ref r, ref g, ref b); // _GetHairRgbColor
    //                            }
    //                            else
    //                            {
    //                                API.GetMakeupRgbColor(i, ref r, ref g, ref b); // _GetMakeupRgbColor
    //                            }

    //                            API.BeginScaleformMovieMethod(ColorPanelScaleform, "SET_DATA_SLOT");
    //                            API.ScaleformMovieMethodAddParamInt(i); // index
    //                            API.ScaleformMovieMethodAddParamInt(r); // r
    //                            API.ScaleformMovieMethodAddParamInt(g); // g
    //                            API.ScaleformMovieMethodAddParamInt(b); // b
    //                            API.EndScaleformMovieMethod();
    //                        }

    //                        API.BeginScaleformMovieMethod(ColorPanelScaleform, "DISPLAY_VIEW");
    //                        API.EndScaleformMovieMethod();

    //                        API.BeginScaleformMovieMethod(ColorPanelScaleform, "SET_HIGHLIGHT");
    //                        API.ScaleformMovieMethodAddParamInt(listItem.ListIndex);
    //                        API.EndScaleformMovieMethod();

    //                        API.BeginScaleformMovieMethod(ColorPanelScaleform, "SHOW_OPACITY");
    //                        API.ScaleformMovieMethodAddParamBool(false);
    //                        API.ScaleformMovieMethodAddParamBool(true);
    //                        API.EndScaleformMovieMethod();

    //                        float width = Width / MenuController.ScreenWidth;
    //                        float height = ((700f / 500f) * Width) / MenuController.ScreenHeight;
    //                        float x = ((Width / 2f) / MenuController.ScreenWidth);
    //                        float y = descriptionYOffset + (height / 2f) + (4f / MenuController.ScreenHeight);
    //                        if (Size > MaxItemsOnScreen)
    //                        {
    //                            y -= (30f / MenuController.ScreenHeight);
    //                        }

    //                        API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
    //                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
    //                        API.DrawScaleformMovie(ColorPanelScaleform, x, y, width, height, 255, 255, 255, 255, 0);
    //                        API.ResetScriptGfxAlign();
    //                    }
    //                }
    //            }

    //            #endregion
    //        }
    //    }
    //    #endregion
    //}
}
