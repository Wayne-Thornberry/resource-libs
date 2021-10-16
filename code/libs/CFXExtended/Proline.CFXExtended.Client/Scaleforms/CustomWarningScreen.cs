

namespace Proline.CFXExtended.Core.Scaleforms
{
    public class CustomWarningScreen : ScaleformUI
    {
        public CustomWarningScreen() : base("custom_warning_screen")
        {

        }

        public void ShowCustomScreen(string title, string subtitle, params string[] items)
        {
            CallFunction("SHOW_CUSTOM_WARNING_SCREEN", items[0], items[1], items[2], items[3], items[4], items[5], items.Length, title, subtitle);
        }

        public void SetSelectedIndex(int index)
        {
            CallFunction("SET_SELECTED_INDEX", index);
        }

        public void HideCustomWarningScreen()
        {
            CallFunction("HIDE_CUSTOM_WARNING_SCREEN");
        }
    }
}