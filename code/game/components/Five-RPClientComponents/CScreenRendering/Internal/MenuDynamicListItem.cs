namespace Proline.ClassicOnline.SClassic.UI.Menu
{
    public class MenuDynamicListItem : MenuItem
    {
        public delegate string ChangeItemCallback(MenuDynamicListItem item, bool left);

        public MenuDynamicListItem(string text, string initialValue, ChangeItemCallback callback) : this(text,
            initialValue, callback, null)
        {
        }

        public MenuDynamicListItem(string text, string initialValue, ChangeItemCallback callback, string description) :
            base(text, description)
        {
            CurrentItem = initialValue;
            Callback = callback;
        }

        public bool HideArrowsWhenNotSelected { get; set; } = false;
        public string CurrentItem { get; set; }

        public ChangeItemCallback Callback { get; set; }

        internal override void Draw(int indexOffset)
        {
            if (HideArrowsWhenNotSelected && !Selected)
                Label = CurrentItem ?? "~r~N/A";
            else
                Label = $"~s~← {CurrentItem ?? "~r~N/A~s~"} ~s~→";

            base.Draw(indexOffset);
        }
    }
}