using System.Collections.Generic;

namespace Proline.ClassicOnline.SClassic.UI.Menu
{
    public class MenuListItem : MenuItem
    {
        public enum ColorPanelType
        {
            Hair,
            Makeup
        }

        public ColorPanelType ColorPanelColorType = ColorPanelType.Hair;

        public MenuListItem(string text, List<string> items, int index) : this(text, items, index, null)
        {
        }

        public MenuListItem(string text, List<string> items, int index, string description) : base(text, description)
        {
            ListItems = items;
            ListIndex = index;
        }

        public int ListIndex { get; set; }
        public List<string> ListItems { get; set; } = new List<string>();
        public bool HideArrowsWhenNotSelected { get; set; } = false;
        public bool ShowOpacityPanel { get; set; } = false;
        public bool ShowColorPanel { get; set; } = false;
        public int ItemsCount => ListItems.Count;

        public string GetCurrentSelection()
        {
            if (ItemsCount > 0 && ListIndex >= 0 && ListIndex < ItemsCount) return ListItems[ListIndex];
            return null;
        }

        internal override void Draw(int indexOffset)
        {
            while (ListIndex < 0) ListIndex += ItemsCount;
            while (ListIndex >= ItemsCount) ListIndex -= ItemsCount;

            if (HideArrowsWhenNotSelected && !Selected)
                Label = GetCurrentSelection() ?? "~r~N/A";
            else
                Label = $"~s~← {GetCurrentSelection() ?? "~r~N/A~s~"} ~s~→";

            base.Draw(indexOffset);
        }
    }
}