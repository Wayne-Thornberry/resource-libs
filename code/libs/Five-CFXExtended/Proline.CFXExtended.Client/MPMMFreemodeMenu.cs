namespace Proline.ClassicOnline.Scaleforms
{
    public class MPMMFreemodeMenu : ScaleformUI
    {
        public MPMMFreemodeMenu() : base("MP_MM_CARD_FREEMODE")
        {

        }

        public void Collapse(bool t)
        {
            CallFunction("COLLAPSE", t);
        }

        public void SetIcon(int index, int iconEnum, int rank)
        {
            CallFunction("SET_ICON", index, iconEnum, rank);
        }

        public void DisplayMic()
        {
            CallFunction("DISPLAY_MIC");
        }

        public void SetTitle(string title, string subtitle, int icon)
        {
            CallFunction("SET_TITLE", title, subtitle, icon);
        }

        public void SetDataSlot(int index, params object[] args)
        {
            CallFunction("SET_DATA_SLOT", index, args);
        }

        public void UpdateSlot(int index)
        {
            CallFunction("UPDATE_SLOT", index);
        }

        public void SetHighlight(int index)
        {
            CallFunction("SET_HIGHLIGHT", index);
        }

        public void DisplayView()
        {
            CallFunction("DISPLAY_VIEW");
        }

        public void SetDataSlotEmpty(int viewIndex, int itemIndex)
        {
            CallFunction("SET_DATA_SLOT_EMPTY", viewIndex, itemIndex);
        }

        //public bool TXTHasLoaded()
        //{

        //}

        //public void TXTAlreadyLoaded()
        //{

        //}

        //public void AddTXTRefResponse()
        //{

        //}
    }
}
