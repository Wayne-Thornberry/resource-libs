using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CScreenRendering
{
    public enum MenuAlignmentOption
    {
        Left,
        Right
    }


    public static partial class CScreenRenderingAPI
    {
        private static bool _isMenuButtonsEnabled;
        public const string _texture_dict = "commonmenu";
        public const string _header_texture = "interaction_bgd";

        public static float GetScreenWidth()
        {
            return 1080 * API.GetScreenAspectRatio(false);
        }

        public static float GetScreenHeight()
        {
            return 1080;
        }
        public static int GetMenuAlignment()
        {
            return 1;
        }

        public static string GetMenuTextureDict()
        {
            return _texture_dict;
        }

        public static string GetMenuHeaderTexture()
        {
            return _header_texture;
        }
        public static void SetMenuButtonsEnabled(bool menu)
        {
            _isMenuButtonsEnabled = menu;
        }

        public static bool IsMenuButtonsEnabled()
        {
            return _isMenuButtonsEnabled;
        } 

        /// <summary>
        ///     This binds the <paramref name="childMenu" /> menu to the <paramref name="menuItem" /> and sets the menu's parent to
        ///     <paramref name="parentMenu" />.
        /// </summary>
        /// <param name="parentMenu"></param>
        /// <param name="childMenu"></param>
        /// <param name="menuItem"></param>
        public static void BindMenuItem(int parentMenu, int childMenu, int menuItem)
        {
            AddSubmenu(parentMenu, childMenu);
            if (MenuButtons.ContainsKey(menuItem))
                MenuButtons[menuItem] = childMenu;
            else
                MenuButtons.Add(menuItem, childMenu);
        }

        /// <summary>
        ///     This adds the <paramref name="menu" /> <see cref="Menu" /> to the <see cref="Menus" /> list.
        /// </summary>
        /// <param name="menu"></param>
        public static int AddMenu(string name, string subtitle)
        {
            if (!Menus.Contains(menu))
            {
                Menus.Add(menu);
                // automatically set the first menu as the main menu if none is set yet, this can be changed at any time though.
                if (MainMenu == null) MainMenu = menu;
            }
        }

        /// <summary>
        ///     Adds the <paramref name="child" /> <see cref="Menu" /> to the menus list and sets the menu's parent to
        ///     <paramref name="parent" />.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static int AddSubmenu(int menu, string name, string subtitle)
        {
            if (!Menus.Contains(child))
                AddMenu(child);
            child.ParentMenu = parent;
        } 


        /// <summary>
        ///     Returns the currently opened menu.
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentMenu()
        {
            if (Menus.Any(m => m.Visible))
                return Menus.Find(m => m.Visible);
            return null;
        }

        /// <summary>
        ///     Returns true if any menu is currently open.
        /// </summary>
        /// <returns></returns>
        public static bool IsAnyMenuOpen()
        {
            return Menus.Any(m => m.Visible);
        }

        /// <summary>
        ///     Closes all menus.
        /// </summary>
        public static void CloseAllMenus()
        {
            Menus.ForEach(m =>
            {
                if (m.Visible) m.CloseMenu();
            });
        }

        public static void OpenMenu(int v)
        {
            throw new NotImplementedException();
        }

        public static void CloseMenu()
        {
            throw new NotImplementedException();
        }

        public static int GetMenuItemSize()
        {
            throw new NotImplementedException();
        }

        public static void SelectMenuItem(object currentIndex)
        {
            throw new NotImplementedException();
        }

        public static object GetCurrentSelectedIndex()
        {
            throw new NotImplementedException();
        }
    }
}
