using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace Proline.ClassicOnline.MScreen.Menus.MenuItems
{
    public class Menu
    {
        #region constants or readonlys

        public const float Width = 500f;

        #endregion

        #region internal task functions

        /// <summary>
        ///     Draws the menu title + subtitle, calls all Draw functions for all menu items and draws the description for the
        ///     selected item.
        /// </summary>
        /// <returns></returns>
        public async void Draw()
        {
            if (!Game.IsPaused && API.IsScreenFadedIn() && !API.IsPlayerSwitchInProgress() && !Game.PlayerPed.IsDead)
            {
                // impossible to reach this code, but i don't like Visual studio warnings.
                if (CurrentIndex == -2) await BaseScript.Delay(0);

                MenuItemsYOffset = 0f;

                #region Draw Header

                if (!string.IsNullOrEmpty(MenuTitle))
                {
                    #region Draw Header Background

                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                    var x = (Position.X + headerSize.Width / 2f) / MenuController.ScreenWidth;
                    var y = (Position.Y + headerSize.Height / 2f) / MenuController.ScreenHeight;
                    var width = headerSize.Width / MenuController.ScreenWidth;
                    var height = headerSize.Height / MenuController.ScreenHeight;

                    API.DrawSprite(MenuController._texture_dict, MenuController._header_texture, x, y, width, height,
                        0f, 255, 255, 255, 255);

                    API.ResetScriptGfxAlign();

                    #endregion

                    #region Draw Header Menu Title

                    var font = 1;
                    var size = 45f * 27f / MenuController.ScreenHeight;
                    //float size = 1.1f;

                    API.SetScriptGfxAlign(76, 84);
                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                    API.BeginTextCommandDisplayText("STRING");
                    API.SetTextFont(font);
                    API.SetTextColour(255, 255, 255, 255);
                    API.SetTextScale(size, size);
                    API.SetTextJustification(0);
                    API.AddTextComponentSubstringPlayerName(MenuTitle);
                    if (LeftAligned)
                        API.EndTextCommandDisplayText(headerSize.Width / 2f / MenuController.ScreenWidth,
                            y - API.GetTextScaleHeight(size, font) / 2f);
                    else
                        API.EndTextCommandDisplayText(
                            API.GetSafeZoneSize() - headerSize.Width / 2f / MenuController.ScreenWidth,
                            y - API.GetTextScaleHeight(size, font) / 2f);

                    API.ResetScriptGfxAlign();

                    MenuItemsYOffset = headerSize.Height;

                    #endregion
                }

                #endregion

                #region Draw Subtitle

                {
                    #region draw subtitle background

                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                    var bgHeight = 38f;


                    var x = (Position.X + headerSize.Width / 2f) / MenuController.ScreenWidth;
                    var y = (Position.Y + MenuItemsYOffset + bgHeight / 2f) / MenuController.ScreenHeight;
                    var width = headerSize.Width / MenuController.ScreenWidth;
                    var height = bgHeight / MenuController.ScreenHeight;

                    API.DrawRect(x, y, width, height, 0, 0, 0, 250);
                    API.ResetScriptGfxAlign();

                    #endregion

                    #region draw subtitle text

                    if (!string.IsNullOrEmpty(MenuSubtitle))
                    {
                        var font = 0;
                        var size = 14f * 27f / MenuController.ScreenHeight;
                        //float size = 0.34f;

                        API.SetScriptGfxAlign(76, 84);
                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                        API.BeginTextCommandDisplayText("STRING");
                        API.SetTextFont(font);
                        API.SetTextScale(size, size);
                        API.SetTextJustification(1);
                        API.AddTextComponentSubstringPlayerName("~HUD_COLOUR_HB_BLUE~" + MenuSubtitle.ToUpper());
                        if (LeftAligned)
                            API.EndTextCommandDisplayText(10f / MenuController.ScreenWidth,
                                y - (API.GetTextScaleHeight(size, font) / 2f + 4f / MenuController.ScreenHeight));
                        else
                            API.EndTextCommandDisplayText(
                                API.GetSafeZoneSize() - (headerSize.Width - 10f) / MenuController.ScreenWidth,
                                y - (API.GetTextScaleHeight(size, font) / 2f + 4f / MenuController.ScreenHeight));

                        API.ResetScriptGfxAlign();
                    }

                    #endregion

                    #region draw counter + pre-counter text

                    var counterText = $"{CounterPreText ?? ""}{CurrentIndex + 1} / {Size}";
                    if (CounterPreText != null || MaxItemsOnScreen < Size)
                    {
                        var font = 0;
                        var size = 14f * 27f / MenuController.ScreenHeight;
                        //float size = 0.34f;

                        API.SetScriptGfxAlign(76, 84);
                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                        API.BeginTextCommandDisplayText("STRING");
                        API.SetTextFont(font);
                        API.SetTextScale(size, size);
                        API.SetTextJustification(2);
                        API.AddTextComponentSubstringPlayerName("~HUD_COLOUR_HB_BLUE~" + counterText.ToUpper());
                        if (LeftAligned)
                        {
                            API.SetTextWrap(0f, 485f / MenuController.ScreenWidth);
                            API.EndTextCommandDisplayText(10f / MenuController.ScreenWidth,
                                y - (API.GetTextScaleHeight(size, font) / 2f + 4f / MenuController.ScreenHeight));
                        }
                        else
                        {
                            API.SetTextWrap(0f, API.GetSafeZoneSize() - 10f / MenuController.ScreenWidth);
                            API.EndTextCommandDisplayText(0f,
                                y - (API.GetTextScaleHeight(size, font) / 2f + 4f / MenuController.ScreenHeight));
                        }

                        API.ResetScriptGfxAlign();
                    }

                    if (!string.IsNullOrEmpty(MenuSubtitle) || CounterPreText != null || MaxItemsOnScreen < Size)
                        MenuItemsYOffset += bgHeight - 1f;

                    #endregion
                }

                #endregion

                #region Draw menu items background gradient

                if (Size > 0)
                {
                    API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
                    API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                    var bgHeight = 38f * MathUtil.Clamp(_MenuItems.Count, 0, MaxItemsOnScreen);


                    var x = (Position.X + headerSize.Width / 2f) / MenuController.ScreenWidth;
                    var y = (Position.Y + MenuItemsYOffset + (bgHeight + 1f) / 2f) / MenuController.ScreenHeight;
                    var width = headerSize.Width / MenuController.ScreenWidth;
                    var height = (bgHeight + 1f) / MenuController.ScreenHeight;

                    //DrawSprite(MenuController._texture_dict, "gradient_bgd", x, y, width, height, 0f, 255, 255, 255, 255);
                    API.DrawRect(x, y, width, height, 0, 0, 0, 180);
                    API.ResetScriptGfxAlign();
                    MenuItemsYOffset += bgHeight - 1f;
                }

                #endregion

                #region Draw menu items that are visible in the current view.

                if (Size > 0)
                    foreach (var item in VisibleMenuItems)
                        item.Draw(ViewIndexOffset);

                #endregion

                #region Up Down overflow Indicator

                var descriptionYOffset = 0f;
                if (Size > 0)
                    if (Size > MaxItemsOnScreen)
                    {
                        #region background

                        var width = Width / MenuController.ScreenWidth;
                        var height = 60f / MenuController.ScreenWidth;
                        var x = (Position.X + Width / 2f) / MenuController.ScreenWidth;
                        var y = MenuItemsYOffset / MenuController.ScreenHeight + height / 2f +
                                6f / MenuController.ScreenHeight;

                        API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                        API.DrawRect(x, y, width, height, 0, 0, 0, 180);
                        descriptionYOffset = height; // + (1f / MenuController.ScreenHeight);
                        API.ResetScriptGfxAlign();

                        #endregion

                        #region up/down icons

                        API.SetScriptGfxAlign(76, 84);
                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
                        var xMin = 0f;
                        var xMax = Width / MenuController.ScreenWidth;
                        var xCenter = 250f / MenuController.ScreenWidth;
                        var yTop = y - 20f / MenuController.ScreenHeight;
                        var yBottom = y - 10f / MenuController.ScreenHeight;

                        API.BeginTextCommandDisplayText("STRING");
                        API.AddTextComponentSubstringPlayerName("↑");

                        API.SetTextFont(0);
                        API.SetTextScale(1f, 14f * 27f / MenuController.ScreenHeight);
                        //SetTextScale(0.35f, 0.35f);
                        API.SetTextJustification(0);
                        if (LeftAligned)
                        {
                            API.SetTextWrap(xMin, xMax);
                            API.EndTextCommandDisplayText(xCenter, yTop);
                        }
                        else
                        {
                            xMin = API.GetSafeZoneSize() - (Width - 10f) / MenuController.ScreenWidth;
                            xMax = API.GetSafeZoneSize() - 10f / MenuController.ScreenWidth;
                            xCenter = API.GetSafeZoneSize() - 250f / MenuController.ScreenWidth;
                            API.SetTextWrap(xMin, xMax);
                            API.EndTextCommandDisplayText(xCenter, yTop);
                        }

                        API.BeginTextCommandDisplayText("STRING");
                        API.AddTextComponentSubstringPlayerName("↓");

                        API.SetTextFont(0);
                        API.SetTextScale(1f, 14f * 27f / MenuController.ScreenHeight);
                        //SetTextScale(0.35f, 0.35f);
                        API.SetTextJustification(0);
                        if (LeftAligned)
                        {
                            API.SetTextWrap(xMin, xMax);
                            API.EndTextCommandDisplayText(xCenter, yBottom);
                        }
                        else
                        {
                            API.SetTextWrap(xMin, xMax);
                            API.EndTextCommandDisplayText(xCenter, yBottom);
                        }

                        API.ResetScriptGfxAlign();

                        #endregion
                    }

                #endregion

                #region Draw Description

                if (Size > 0)
                {
                    if (!string.IsNullOrEmpty(_MenuItems[CurrentIndex].Description))
                    {
                        #region description text

                        var font = 0;
                        var textSize = 14f * 27f / MenuController.ScreenHeight;
                        //float textSize = 0.35f;

                        var textMinX = 0f + 10f / MenuController.ScreenWidth;
                        var textMaxX = Width / MenuController.ScreenWidth - 10f / MenuController.ScreenWidth;
                        var textY = MenuItemsYOffset / MenuController.ScreenHeight + 16f / MenuController.ScreenHeight +
                                    descriptionYOffset;

                        API.SetScriptGfxAlign(76, 84);
                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                        API.BeginTextCommandDisplayText("CELL_EMAIL_BCON");
                        API.SetTextFont(font);
                        API.SetTextScale(textSize, textSize);
                        API.SetTextJustification(1);
                        var text = _MenuItems[CurrentIndex].Description;
                        foreach (var s in Screen.StringToArray(text)) API.AddTextComponentSubstringPlayerName(s);
                        var textHeight = API.GetTextScaleHeight(textSize, font);
                        if (LeftAligned)
                        {
                            API.SetTextWrap(textMinX, textMaxX);
                            API.EndTextCommandDisplayText(textMinX, textY);
                        }
                        else
                        {
                            textMinX = API.GetSafeZoneSize() - (Width - 10f) / MenuController.ScreenWidth;
                            textMaxX = API.GetSafeZoneSize() - 10f / MenuController.ScreenWidth;
                            API.SetTextWrap(textMinX, textMaxX);
                            API.EndTextCommandDisplayText(textMinX, textY);
                        }

                        API.ResetScriptGfxAlign();

                        API.SetScriptGfxAlign(76, 84);
                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                        API.BeginTextCommandLineCount("CELL_EMAIL_BCON");
                        API.SetTextScale(textSize, textSize);
                        API.SetTextJustification(1);
                        API.SetTextFont(font);
                        var lineCount = 1;
                        foreach (var s in Screen.StringToArray(text)) API.AddTextComponentSubstringPlayerName(s);
                        if (LeftAligned)
                        {
                            API.SetTextWrap(textMinX, textMaxX);
                            lineCount = API.GetTextScreenLineCount(textMinX, textY);
                        }
                        else
                        {
                            API.SetTextWrap(textMinX, textMaxX);
                            lineCount = API.GetTextScreenLineCount(textMinX, textY);
                        }

                        API.ResetScriptGfxAlign();

                        #endregion

                        #region background

                        var descWidth = Width / MenuController.ScreenWidth;
                        var descHeight = (textHeight + 0.005f) * lineCount + 8f / MenuController.ScreenHeight +
                                         2.5f / MenuController.ScreenHeight;
                        var descX = (Position.X + Width / 2f) / MenuController.ScreenWidth;
                        var descY = textY - 6f / MenuController.ScreenHeight + descHeight / 2f;

                        API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
                        API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);

                        API.DrawRect(descX, descY - descHeight / 2f + 2f / MenuController.ScreenHeight, descWidth,
                            4f / MenuController.ScreenHeight, 0, 0, 0, 200);
                        API.DrawRect(descX, descY, descWidth, descHeight, 0, 0, 0, 180);

                        API.ResetScriptGfxAlign();

                        #endregion

                        descriptionYOffset += descY + descHeight / 2f - 4f / MenuController.ScreenHeight;
                    }
                    else
                    {
                        descriptionYOffset += MenuItemsYOffset / MenuController.ScreenHeight +
                                              2f / MenuController.ScreenHeight + descriptionYOffset;
                    }
                }

                #endregion

                #region Draw Color and opacity palletes

                if (Size > 0)
                {
                    var currentItem = _MenuItems[CurrentIndex];
                    if (currentItem is MenuListItem listItem)
                    {
                        /// OPACITY PANEL
                        if (listItem.ShowOpacityPanel)
                        {
                            API.PushScaleformMovieFunction(OpacityPanelScaleform, "SET_TITLE");
                            API.PushScaleformMovieMethodParameterString("Opacity");
                            API.PushScaleformMovieMethodParameterString("");
                            API.PushScaleformMovieMethodParameterInt(listItem.ListIndex * 10); // opacity percent
                            API.EndScaleformMovieMethod();

                            var width = Width / MenuController.ScreenWidth;
                            var height = 700f / 500f * Width / MenuController.ScreenHeight;
                            var x = Width / 2f / MenuController.ScreenWidth;
                            var y = descriptionYOffset + height / 2f + 4f / MenuController.ScreenHeight;
                            if (Size > MaxItemsOnScreen) y -= 30f / MenuController.ScreenHeight;

                            API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
                            API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
                            API.DrawScaleformMovie(OpacityPanelScaleform, x, y, width, height, 255, 255, 255, 255, 0);
                            API.ResetScriptGfxAlign();
                        }
                        /// COLOR PALLETE
                        else if (listItem.ShowColorPanel)
                        {
                            API.PushScaleformMovieFunction(ColorPanelScaleform, "SET_TITLE");
                            API.PushScaleformMovieMethodParameterString("Opacity");
                            API.BeginTextCommandScaleformString("FACE_COLOUR");
                            API.AddTextComponentInteger(listItem.ListIndex + 1);
                            API.AddTextComponentInteger(listItem.ItemsCount);
                            API.EndTextCommandScaleformString();
                            API.PushScaleformMovieMethodParameterInt(0); // opacity percent unused
                            API.PushScaleformMovieMethodParameterBool(true);
                            API.EndScaleformMovieMethod();

                            API.PushScaleformMovieFunction(ColorPanelScaleform, "SET_DATA_SLOT_EMPTY");
                            API.EndScaleformMovieMethod();

                            for (var i = 0; i < 64; i++)
                            {
                                var r = 0;
                                var g = 0;
                                var b = 0;
                                if (listItem.ColorPanelColorType == MenuListItem.ColorPanelType.Hair)
                                    API.GetHairRgbColor(i, ref r, ref g, ref b); // _GetHairRgbColor
                                else
                                    API.GetMakeupRgbColor(i, ref r, ref g, ref b); // _GetMakeupRgbColor

                                API.PushScaleformMovieFunction(ColorPanelScaleform, "SET_DATA_SLOT");
                                API.PushScaleformMovieMethodParameterInt(i); // index
                                API.PushScaleformMovieMethodParameterInt(r); // r
                                API.PushScaleformMovieMethodParameterInt(g); // g
                                API.PushScaleformMovieMethodParameterInt(b); // b
                                API.EndScaleformMovieMethod();
                            }

                            API.PushScaleformMovieFunction(ColorPanelScaleform, "DISPLAY_VIEW");
                            API.EndScaleformMovieMethod();

                            API.PushScaleformMovieFunction(ColorPanelScaleform, "SET_HIGHLIGHT");
                            API.PushScaleformMovieMethodParameterInt(listItem.ListIndex);
                            API.EndScaleformMovieMethod();

                            API.PushScaleformMovieFunction(ColorPanelScaleform, "SHOW_OPACITY");
                            API.PushScaleformMovieMethodParameterBool(false);
                            API.PushScaleformMovieMethodParameterBool(true);
                            API.EndScaleformMovieMethod();

                            var width = Width / MenuController.ScreenWidth;
                            var height = 700f / 500f * Width / MenuController.ScreenHeight;
                            var x = Width / 2f / MenuController.ScreenWidth;
                            var y = descriptionYOffset + height / 2f + 4f / MenuController.ScreenHeight;
                            if (Size > MaxItemsOnScreen) y -= 30f / MenuController.ScreenHeight;

                            API.SetScriptGfxAlign(LeftAligned ? 76 : 82, 84);
                            API.SetScriptGfxAlignParams(0f, 0f, 0f, 0f);
                            API.DrawScaleformMovie(ColorPanelScaleform, x, y, width, height, 255, 255, 255, 255, 0);
                            API.ResetScriptGfxAlign();
                        }
                    }
                }

                #endregion
            }
        }

        #endregion

        #region Setting up events

        #region delegates

        /// <summary>
        ///     Triggered when a <see cref="MenuItem" /> is selected.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this event occurred.</param>
        /// <param name="menuItem">The <see cref="MenuItem" /> that was selected.</param>
        /// <param name="itemIndex">The <see cref="MenuItem.Index" /> of this <see cref="MenuItem" />.</param>
        public delegate void ItemSelectEvent(Menu menu, MenuItem menuItem, int itemIndex);

        /// <summary>
        ///     Triggered when a <see cref="MenuCheckboxItem" /> was toggled.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this event occurred.</param>
        /// <param name="menuItem">The <see cref="MenuCheckboxItem" /> that was toggled.</param>
        /// <param name="itemIndex">The <see cref="MenuItem.Index" /> of this <see cref="MenuCheckboxItem" />.</param>
        /// <param name="newCheckedState">
        ///     The new <see cref="MenuCheckboxItem.Checked" /> state of this
        ///     <see cref="MenuCheckboxItem" />.
        /// </param>
        public delegate void CheckboxItemChangeEvent(Menu menu, MenuCheckboxItem menuItem, int itemIndex,
            bool newCheckedState);

        /// <summary>
        ///     Triggered when a <see cref="MenuListItem" /> is selected.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnListItemSelect" /> event occurred.</param>
        /// <param name="listItem">The <see cref="MenuListItem" /> that was selected.</param>
        /// <param name="selectedIndex">The <see cref="MenuListItem.ListIndex" /> of the <see cref="MenuListItem" />.</param>
        /// <param name="itemIndex">
        ///     The <see cref="MenuItem.Index" /> of the <see cref="MenuListItem" /> in the <see cref="Menu" />
        ///     .
        /// </param>
        public delegate void ListItemSelectedEvent(Menu menu, MenuListItem listItem, int selectedIndex, int itemIndex);

        /// <summary>
        ///     Triggered when a <see cref="MenuListItem" />'s index was changed.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnListIndexChange" /> event occurred.</param>
        /// <param name="listItem">The <see cref="MenuListItem" /> that was changed.</param>
        /// <param name="oldSelectionIndex">The old <see cref="MenuListItem.ListIndex" /> of the <see cref="MenuListItem" />.</param>
        /// <param name="newSelectionIndex">The new <see cref="MenuListItem.ListIndex" /> of the <see cref="MenuListItem" />.</param>
        /// <param name="itemIndex">
        ///     The <see cref="MenuItem.Index" /> of the <see cref="MenuListItem" /> in the <see cref="Menu" />
        ///     .
        /// </param>
        public delegate void ListItemIndexChangedEvent(Menu menu, MenuListItem listItem, int oldSelectionIndex,
            int newSelectionIndex, int itemIndex);

        /// <summary>
        ///     Triggered when a <see cref="Menu" /> is closed.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> that was closed.</param>
        public delegate void MenuClosedEvent(Menu menu);

        /// <summary>
        ///     Triggered when a <see cref="Menu" /> is opened.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> that has been opened.</param>
        public delegate void MenuOpenedEvent(Menu menu);

        /// <summary>
        ///     Triggered when the <see cref="CurrentIndex" /> changes.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnIndexChange" /></param>
        /// event occurred.
        /// <param name="oldItem">The old <see cref="MenuItem" /> that was previously selected.</param>
        /// <param name="newItem">The new <see cref="MenuItem" /> that is now selected.</param>
        /// <param name="oldIndex">The old <see cref="MenuItem.Index" /> of this item.</param>
        /// <param name="newIndex">The new <see cref="MenuItem.Index" /> of this item.</param>
        public delegate void IndexChangedEvent(Menu menu, MenuItem oldItem, MenuItem newItem, int oldIndex,
            int newIndex);

        /// <summary>
        ///     Triggered when the <see cref="MenuSliderItem.Position" /> changes.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnSliderPositionChange" /> event occurred.</param>
        /// <param name="sliderItem">The <see cref="MenuSliderItem>" /> that was changed.</param>
        /// <param name="oldPosition">The old position of the slider bar.</param>
        /// <param name="newPosition">The new position of the slider bar.</param>
        /// <param name="itemIndex">The index of this <see cref="MenuSliderItem" />.</param>
        public delegate void SliderPositionChangedEvent(Menu menu, MenuSliderItem sliderItem, int oldPosition,
            int newPosition, int itemIndex);

        /// <summary>
        ///     Triggered when a <see cref="MenuSliderItem" /> was selected.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnSliderItemSelect" /> event occurred.</param>
        /// <param name="sliderItem">The <see cref="MenuSliderItem>" /> that was pressed.</param>
        /// <param name="sliderPosition">The current position of the slider bar.</param>
        /// <param name="itemIndex">The index of this <see cref="MenuSliderItem" />.</param>
        public delegate void SliderItemSelectedEvent(Menu menu, MenuSliderItem sliderItem, int sliderPosition,
            int itemIndex);

        #endregion

        #region events

        /// <summary>
        ///     Triggered when a <see cref="MenuItem" /> is selected.
        ///     Parameters: <see cref="Menu" /> parentMenu, <see cref="MenuItem" /> menuItem, <see cref="int" /> itemIndex.
        /// </summary>
        public event ItemSelectEvent OnItemSelect;

        /// <summary>
        ///     Triggered when a <see cref="MenuCheckboxItem" /> was toggled.
        ///     Parameters: <see cref="Menu" /> parentMenu, <see cref="MenuCheckboxItem" /> menuItem, <see cref="int" /> itemIndex,
        ///     <see cref="bool" /> newCheckedState.
        /// </summary>
        public event CheckboxItemChangeEvent OnCheckboxChange;

        /// <summary>
        ///     Triggered when a <see cref="MenuListItem" /> is selected.
        ///     Parameters: <see cref="Menu" /> menu, <see cref="MenuListItem" /> listItem, <see cref="MenuListItem.ListIndex" />
        ///     selectedIndex, <see cref="int" /> itemIndex.
        /// </summary>
        public event ListItemSelectedEvent OnListItemSelect;

        /// <summary>
        ///     Triggered when a <see cref="MenuListItem" />'s index was changed.
        ///     Parameters: <see cref="Menu" /> menu, <see cref="MenuListItem" /> listItem, <see cref="MenuListItem.ListIndex" />
        ///     oldSelectionIndex, <see cref="int" /> newSelectionIndex, <see cref="int" /> itemIndex.
        /// </summary>
        public event ListItemIndexChangedEvent OnListIndexChange;

        /// <summary>
        ///     Triggered when a <see cref="Menu" /> is closed.
        ///     Parameters: <see cref="Menu" /> closedMenu.
        /// </summary>
        public event MenuClosedEvent OnMenuClose;

        /// <summary>
        ///     Triggered when a <see cref="Menu" /> is opened.
        ///     Parameters: <see cref="Menu" /> openedMenu.
        /// </summary>
        public event MenuOpenedEvent OnMenuOpen;

        /// <summary>
        ///     Triggered when the <see cref="CurrentIndex" /> changes.
        ///     Parameters: <see cref="Menu" /> menu, <see cref="MenuItem" /> oldSelectedItem, <see cref="MenuItem" />
        ///     newSelectedItem, <see cref="int" /> oldIndex, <see cref="int" /> newIndex.
        /// </summary>
        public event IndexChangedEvent OnIndexChange;

        /// <summary>
        ///     Triggered when the <see cref="MenuSliderItem.Position" /> changes.
        ///     Parameters: <see cref="Menu" /> menu, <see cref="MenuSliderItem" /> sliderItem, <see cref="int" /> oldPosition,
        ///     <see cref="int" /> newPosition, <see cref="int" /> itemIndex
        /// </summary>
        public event SliderPositionChangedEvent OnSliderPositionChange;

        /// <summary>
        ///     Triggered when a <see cref="MenuSliderItem" /> was selected.
        ///     Parameters: <see cref="Menu" /> menu, <see cref="MenuSliderItem" /> sliderItem, <see cref="int" /> sliderPosition,
        ///     <see cref="int" /> itemIndex.
        /// </summary>
        public event SliderItemSelectedEvent OnSliderItemSelect;

        #endregion

        #region virtual voids

        /// <summary>
        ///     Triggered when a <see cref="MenuItem" /> is selected.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this event occurred.</param>
        /// <param name="menuItem">The <see cref="MenuItem" /> that was selected.</param>
        /// <param name="itemIndex">The <see cref="MenuItem.Index" /> of this <see cref="MenuItem" />.</param>
        protected virtual void ItemSelectedEvent(MenuItem menuItem, int itemIndex)
        {
            OnItemSelect?.Invoke(this, menuItem, itemIndex);
        }

        /// <summary>
        ///     Triggered when a <see cref="MenuCheckboxItem" /> was toggled.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this event occurred.</param>
        /// <param name="menuItem">The <see cref="MenuCheckboxItem" /> that was toggled.</param>
        /// <param name="itemIndex">The <see cref="MenuItem.Index" /> of this <see cref="MenuCheckboxItem" />.</param>
        /// <param name="newCheckedState">
        ///     The new <see cref="MenuCheckboxItem.Checked" /> state of this
        ///     <see cref="MenuCheckboxItem" />.
        /// </param>
        protected virtual void CheckboxChangedEvent(MenuCheckboxItem menuItem, int itemIndex, bool _checked)
        {
            OnCheckboxChange?.Invoke(this, menuItem, itemIndex, _checked);
        }

        /// <summary>
        ///     Triggered when a <see cref="MenuListItem" /> is selected.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnListItemSelect" /> event occurred.</param>
        /// <param name="listItem">The <see cref="MenuListItem" /> that was selected.</param>
        /// <param name="selectedIndex">The <see cref="MenuListItem.ListIndex" /> of the <see cref="MenuListItem" />.</param>
        /// <param name="itemIndex">
        ///     The <see cref="MenuItem.Index" /> of the <see cref="MenuListItem" /> in the <see cref="Menu" />
        ///     .
        /// </param>
        protected virtual void ListItemSelectEvent(Menu menu, MenuListItem listItem, int selectedIndex, int itemIndex)
        {
            OnListItemSelect?.Invoke(menu, listItem, selectedIndex, itemIndex);
        }

        /// <summary>
        ///     Triggered when a <see cref="MenuListItem" />'s index was changed.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnListIndexChange" /> event occurred.</param>
        /// <param name="listItem">The <see cref="MenuListItem" /> that was changed.</param>
        /// <param name="oldSelectionIndex">The old <see cref="MenuListItem.ListIndex" /> of the <see cref="MenuListItem" />.</param>
        /// <param name="newSelectionIndex">The new <see cref="MenuListItem.ListIndex" /> of the <see cref="MenuListItem" />.</param>
        /// <param name="itemIndex">
        ///     The <see cref="MenuItem.Index" /> of the <see cref="MenuListItem" /> in the <see cref="Menu" />
        ///     .
        /// </param>
        protected virtual void ListItemIndexChangeEvent(Menu menu, MenuListItem listItem, int oldSelectionIndex,
            int newSelectionIndex, int itemIndex)
        {
            OnListIndexChange?.Invoke(menu, listItem, oldSelectionIndex, newSelectionIndex, itemIndex);
        }

        /// <summary>
        ///     Triggered when a <see cref="Menu" /> is closed.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> that was closed.</param>
        protected virtual void MenuCloseEvent(Menu menu)
        {
            OnMenuClose?.Invoke(menu);
        }

        /// <summary>
        ///     Triggered when a <see cref="Menu" /> is opened.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> that has been opened.</param>
        protected virtual void MenuOpenEvent(Menu menu)
        {
            OnMenuOpen?.Invoke(menu);
        }

        /// <summary>
        ///     Triggered when the <see cref="CurrentIndex" /> changes.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnIndexChange" /> event occurred.</param>
        /// <param name="oldItem">The old <see cref="MenuItem" /> that was previously selected.</param>
        /// <param name="newItem">The new <see cref="MenuItem" /> that is now selected.</param>
        /// <param name="oldIndex">The old <see cref="MenuItem.Index" /> of this item.</param>
        /// <param name="newIndex">The new <see cref="MenuItem.Index" /> of this item.</param>
        protected virtual void IndexChangeEvent(Menu menu, MenuItem oldItem, MenuItem newItem, int oldIndex,
            int newIndex)
        {
            OnIndexChange?.Invoke(menu, oldItem, newItem, oldIndex, newIndex);
        }

        /// <summary>
        ///     Triggered when the <see cref="MenuSliderItem.Position" /> changes.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnSliderPositionChange" /> event occurred.</param>
        /// <param name="sliderItem">The <see cref="MenuSliderItem>" /> that was changed.</param>
        /// <param name="oldPosition">The old position of the slider bar.</param>
        /// <param name="newPosition">The new position of the slider bar.</param>
        /// <param name="itemIndex">The index of this <see cref="MenuSliderItem" />.</param>
        protected virtual void SliderItemChangedEvent(Menu menu, MenuSliderItem sliderItem, int oldPosition,
            int newPosition, int itemIndex)
        {
            OnSliderPositionChange?.Invoke(menu, sliderItem, oldPosition, newPosition, itemIndex);
        }

        /// <summary>
        ///     Triggered when a <see cref="MenuSliderItem" /> was selected.
        /// </summary>
        /// <param name="menu">The <see cref="Menu" /> in which this <see cref="OnSliderItemSelect" /> event occurred.</param>
        /// <param name="sliderItem">The <see cref="MenuSliderItem>" /> that was pressed.</param>
        /// <param name="sliderPosition">The current position of the slider bar.</param>
        /// <param name="itemIndex">The index of this <see cref="MenuSliderItem" />.</param>
        protected virtual void SliderSelectedEvent(Menu menu, MenuSliderItem sliderItem, int sliderPosition,
            int itemIndex)
        {
            OnSliderItemSelect?.Invoke(menu, sliderItem, sliderPosition, itemIndex);
        }

        #endregion

        #endregion

        #region private variables

        private static readonly SizeF headerSize = new SizeF(Width, 110f);

        public int ViewIndexOffset { get; private set; }

        private List<MenuItem> VisibleMenuItems
        {
            get
            {
                // Create a duplicate list, just in case the original list is modified while we're looping through it.
                var items = GetMenuItems().ToList()
                    .GetRange(ViewIndexOffset, Math.Min(MaxItemsOnScreen, Size - ViewIndexOffset));
                return items;
            }
        }

        private List<MenuItem> _MenuItems { get; } = new List<MenuItem>();

        private readonly int
            ColorPanelScaleform =
                API.RequestScaleformMovie(
                    "COLOUR_SWITCHER_02"); // Could probably be improved, but was getting some glitchy results if it wasn't pre-loaded.

        private readonly int
            OpacityPanelScaleform =
                API.RequestScaleformMovie(
                    "COLOUR_SWITCHER_01"); // Could probably be improved, but was getting some glitchy results if it wasn't pre-loaded.

        #endregion

        #region Public Variables

        public string MenuTitle { get; set; }

        public string MenuSubtitle { get; set; }

        public bool IgnoreDontOpenMenus { get; set; } = false;

        public int MaxItemsOnScreen { get; internal set; } = 10;

        public int Size => _MenuItems.Count;

        public bool Visible { get; set; }

        public bool LeftAligned => MenuController.MenuAlignment == MenuController.MenuAlignmentOption.Left;

        public PointF Position { get; } = new PointF(0f, 0f);

        public float MenuItemsYOffset { get; private set; }

        public string CounterPreText { get; set; }

        public Menu ParentMenu { get; set; } = null;

        public int CurrentIndex { get; internal set; }

        public bool EnableInstructionalButtons { get; set; } = true;

        public Dictionary<Control, string> InstructionalButtons = new Dictionary<Control, string>
        {
            {Control.FrontendAccept, API.GetLabelText("HUD_INPUT28")},
            {Control.FrontendCancel, API.GetLabelText("HUD_INPUT53")}
        };

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new <see cref="Menu" />.
        /// </summary>
        /// <param name="name"></param>
        public Menu(string name) : this(name, null)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="Menu" />.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subtitle"></param>
        public Menu(string name, string subtitle)
        {
            MenuTitle = name;
            MenuSubtitle = subtitle;
        }

        #endregion

        #region Public functions

        /// <summary>
        ///     Sets the max amount of visible items on screen at a time.
        ///     Min = 3, max = 10.
        /// </summary>
        /// <param name="max">A value between 3 and 10 (inclusive).</param>
        public void SetMaxItemsOnScreen(int max)
        {
            if (max < 11 && max > 2) MaxItemsOnScreen = max;
        }

        /// <summary>
        ///     Resets the index to 0
        /// </summary>
        public void RefreshIndex()
        {
            RefreshIndex(0, 0);
        }

        public void RefreshIndex(int index)
        {
            RefreshIndex(index, index > MaxItemsOnScreen ? index - MaxItemsOnScreen : 0);
        }

        public void RefreshIndex(int index, int viewOffset)
        {
            CurrentIndex = index;
            ViewIndexOffset = viewOffset;
        }

        /// <summary>
        ///     Returns the menu items in this menu.
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetMenuItems()
        {
            return _MenuItems.ToList();
        }

        /// <summary>
        ///     Removes all menu items.
        /// </summary>
        public void ClearMenuItems()
        {
            CurrentIndex = 0;
            ViewIndexOffset = 0;
            _MenuItems.Clear();
        }

        /// <summary>
        ///     Removes all menu items.
        /// </summary>
        public void ClearMenuItems(bool dontResetIndex)
        {
            if (!dontResetIndex)
            {
                CurrentIndex = 0;
                ViewIndexOffset = 0;
            }

            _MenuItems.Clear();
        }

        /// <summary>
        ///     Adds a <see cref="MenuItem" /> to this <see cref="Menu" />.
        /// </summary>
        /// <param name="item"></param>
        public void AddMenuItem(MenuItem item)
        {
            _MenuItems.Add(item);
            item.PositionOnScreen = item.Index;
            item.ParentMenu = this;
        }

        /// <summary>
        ///     Removes the item at that index.
        /// </summary>
        /// <param name="itemIndex"></param>
        public void RemoveMenuItem(int itemIndex)
        {
            if (CurrentIndex >= itemIndex)
            {
                if (Size > CurrentIndex)
                    CurrentIndex--;
                else
                    CurrentIndex = 0;
            }

            if (itemIndex < Size && itemIndex > -1)
            {
                RemoveMenuItem(_MenuItems[itemIndex]);
                RemoveMenuItem(_MenuItems[itemIndex]);
            }
        }

        /// <summary>
        ///     Removes the specified <see cref="MenuItem" /> from this <see cref="Menu" />.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveMenuItem(MenuItem item)
        {
            if (_MenuItems.Contains(item))
            {
                if (CurrentIndex >= item.Index)
                {
                    if (Size > CurrentIndex)
                        CurrentIndex--;
                    else
                        CurrentIndex = 0;
                }

                _MenuItems.Remove(item);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="ItemSelectedEvent(MenuItem, int)" /> event function.
        /// </summary>
        /// <param name="index"></param>
        public void SelectItem(int index)
        {
            if (index > -1 && _MenuItems.Count - 1 >= index) SelectItem(_MenuItems[index]);
        }

        /// <summary>
        ///     Triggers the <see cref="ItemSelectedEvent(MenuItem, int)" /> event function.
        /// </summary>
        /// <param name="index"></param>
        public void SelectItem(MenuItem item)
        {
            if (item != null && item.Enabled)
            {
                if (item is MenuCheckboxItem checkbox)
                {
                    checkbox.Checked = !checkbox.Checked;
                    CheckboxChangedEvent(checkbox, item.Index, checkbox.Checked);
                }
                else if (item is MenuListItem listItem)
                {
                    ListItemSelectEvent(this, listItem, listItem.ListIndex, listItem.Index);
                }
                else if (item is MenuSliderItem slider)
                {
                    SliderSelectedEvent(this, slider, slider.Position, slider.Index);
                }
                else
                {
                    ItemSelectedEvent(item, item.Index);
                }

                API.PlaySoundFrontend(-1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                if (MenuController.MenuButtons.ContainsKey(item))
                {
                    // this updates the parent menu.
                    MenuController.AddSubmenu(MenuController.GetCurrentMenu(), MenuController.MenuButtons[item]);

                    MenuController.GetCurrentMenu().CloseMenu();
                    MenuController.MenuButtons[item].OpenMenu();
                }
            }
            else if (item != null && !item.Enabled)
            {
                API.PlaySoundFrontend(-1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
            }
        }

        /// <summary>
        ///     Returns to the parent menu. If there's no parent menu, then the current menu just closes.
        /// </summary>
        public void GoBack()
        {
            API.PlaySoundFrontend(-1, "BACK", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
            CloseMenu();
            if (ParentMenu != null) ParentMenu.OpenMenu();
        }

        /// <summary>
        ///     Closes the menu. Also triggers the <see cref="OnMenuClose" /> event.
        /// </summary>
        public void CloseMenu()
        {
            Visible = false;
            MenuCloseEvent(this);
        }

        /// <summary>
        ///     Opens the menu and triggers the <see cref="OnMenuOpen" /> event.
        /// </summary>
        public void OpenMenu()
        {
            Visible = true;
            MenuOpenEvent(this);
        }

        /// <summary>
        ///     Goes up one menu item if possible.
        /// </summary>
        public void GoUp()
        {
            if (Visible && Size > 1)
            {
                var oldItem = _MenuItems[CurrentIndex];
                CurrentIndex--;
                if (CurrentIndex < 0) CurrentIndex = Size - 1;
                if (!VisibleMenuItems.Contains(_MenuItems[CurrentIndex]))
                {
                    ViewIndexOffset--;
                    if (ViewIndexOffset < 0) ViewIndexOffset = Math.Max(Size - MaxItemsOnScreen, 0);
                }

                IndexChangeEvent(this, oldItem, _MenuItems[CurrentIndex], oldItem.Index, CurrentIndex);
                API.PlaySoundFrontend(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
            }
        }

        /// <summary>
        ///     Goes down one menu item if possible.
        /// </summary>
        public void GoDown()
        {
            if (Visible && Size > 1)
            {
                var oldItem = _MenuItems[CurrentIndex];
                CurrentIndex++;
                if (CurrentIndex >= Size) CurrentIndex = 0;
                if (!VisibleMenuItems.Contains(_MenuItems[CurrentIndex]))
                {
                    ViewIndexOffset++;
                    if (CurrentIndex == 0) ViewIndexOffset = 0;
                }

                IndexChangeEvent(this, oldItem, _MenuItems[CurrentIndex], oldItem.Index, CurrentIndex);
                API.PlaySoundFrontend(-1, "NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
            }
        }

        /// <summary>
        ///     If the item is a <see cref="MenuListItem" /> or a <see cref="MenuSliderItem" /> then it'll go left if possible.
        /// </summary>
        public void GoLeft()
        {
            if (MenuController.AreMenuButtonsEnabled)
            {
                var item = _MenuItems.ElementAt(CurrentIndex);
                if (item.Enabled && item is MenuListItem listItem)
                {
                    if (listItem.ItemsCount > 0)
                    {
                        var oldIndex = listItem.ListIndex;
                        var newIndex = oldIndex;
                        if (listItem.ListIndex < 1)
                            newIndex = listItem.ItemsCount - 1;
                        else
                            newIndex--;
                        listItem.ListIndex = newIndex;
                        ListItemIndexChangeEvent(this, listItem, oldIndex, newIndex, listItem.Index);
                        API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                    }
                }
                else if (item.Enabled && item is MenuSliderItem slider)
                {
                    if (slider.Position > slider.Min)
                    {
                        SliderItemChangedEvent(this, slider, slider.Position, slider.Position - 1, slider.Index);
                        slider.Position--;
                        API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                    }
                    else
                    {
                        API.PlaySoundFrontend(-1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                    }
                }
                else if (item.Enabled && item is MenuDynamicListItem dynList)
                {
                    var newSelectedItem = dynList.Callback(dynList, true);
                    dynList.CurrentItem = newSelectedItem;
                    API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                }
            }
        }

        /// <summary>
        ///     If the item is a <see cref="MenuListItem" /> or a <see cref="MenuSliderItem" /> then it'll go right if possible.
        /// </summary>
        public void GoRight()
        {
            if (MenuController.AreMenuButtonsEnabled)
            {
                var item = _MenuItems.ElementAt(CurrentIndex);
                if (item.Enabled && item is MenuListItem listItem)
                {
                    if (listItem.ItemsCount > 0)
                    {
                        var oldIndex = listItem.ListIndex;
                        var newIndex = oldIndex;
                        if (listItem.ListIndex >= listItem.ItemsCount - 1)
                            newIndex = 0;
                        else
                            newIndex++;
                        listItem.ListIndex = newIndex;
                        ListItemIndexChangeEvent(this, listItem, oldIndex, newIndex, listItem.Index);
                        API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                    }
                }
                else if (item.Enabled && item is MenuSliderItem slider)
                {
                    if (slider.Position < slider.Max)
                    {
                        SliderItemChangedEvent(this, slider, slider.Position, slider.Position + 1, slider.Index);
                        slider.Position++;
                        API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                    }
                    else
                    {
                        API.PlaySoundFrontend(-1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                    }
                }
                else if (item.Enabled && item is MenuDynamicListItem dynList)
                {
                    var newSelectedItem = dynList.Callback(dynList, false);
                    dynList.CurrentItem = newSelectedItem;
                    API.PlaySoundFrontend(-1, "NAV_LEFT_RIGHT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                }
            }
        }

        /// <summary>
        ///     Allows you to sort the menu items using your own compare function.
        /// </summary>
        /// <param name="compare"></param>
        public void SortMenuItems(Comparison<MenuItem> compare)
        {
            _MenuItems.Sort(compare);
        }

        #endregion
    }
}