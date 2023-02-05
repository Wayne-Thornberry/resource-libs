namespace Proline.ClassicOnline.Scaleforms
{
    public class CellPhoneIFruit : ScaleformUI
    {
        public CellPhoneIFruit() : base("cellphone_ifruit")
        {

        }

        public void SetTitleBarTime(int x, int y, int z)
        {
            CallFunction("SET_TITLEBAR_TIME", x, y, z);
        }

        public void SetSleepMode(bool v)
        {
            CallFunction("SET_SLEEP_MODE", v);
        }

        public void SetHeader(string header)
        {
            CallFunction("SET_HEADER", header);
        }
        public void SetSoftKeys(int i)
        {
            CallFunction("SET_SOFT_KEYS", i, true, i);
        }

        public void SetSoftKeysColour(int i)
        {
            CallFunction("SET_SOFT_KEYS_COLOUR", i, 255, 255, 255);
        }
        public void SetSignalStrength(int x)
        {
            CallFunction("SET_SIGNAL_STRENGTH", x);
        }
        public void SetTheme(int x)
        {
            CallFunction("SET_THEME", x);
        }
        public void DisplayView(int x, int y)
        {
            CallFunction("DISPLAY_VIEW", x, y);
        }
    }
}