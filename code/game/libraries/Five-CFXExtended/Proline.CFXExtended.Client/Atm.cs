using System.Threading.Tasks;

namespace Proline.ClassicOnline.Scaleforms
{
    public class Atm : ScaleformUI
    {
        public Atm() : base("atm")
        {
        }

        public void InputForward()
        {
            CallFunction("SET_INPUT_SELECT");
        }

        public void InputBackward()
        {
            CallFunction("SET_INPUT_SELECT");
        }

        public void InputMoveUp()
        {
            CallFunction("SET_INPUT_EVENT", 1);
        }

        public void InputMoveRight()
        {
            CallFunction("SET_INPUT_EVENT", 2);
        }

        public void InputMoveDown()
        {
            CallFunction("SET_INPUT_EVENT", 3);
        }

        public void InputMoveLeft()
        {
            CallFunction("SET_INPUT_EVENT", 4);
        }

        public async Task<int> GetCurrentSelection()
        {
            return await CallFunction<int>("GET_CURRENT_SELECTION");
        }

        public void DisplayTransactions()
        {
            CallFunction("DISPLAY_TRANSACTIONS");
        }

        public void DisplayMessage()
        {
            CallFunction("DISPLAY_MESSAGE");
        }

        public void DisplayBalance(string name, string subtitle, int bankBalance)
        {
            CallFunction("DISPLAY_BALANCE", name, subtitle, bankBalance);
        }

        public void DisplayMenu()
        {
            CallFunction("DISPLAY_MENU");
        }

        public void DisplayCashOptions()
        {
            CallFunction("DISPLAY_CASH_OPTIONS");
        }

        public void SetDataSlot(int slotId, object arg)
        {
            CallFunction("SET_DATA_SLOT", slotId, arg);
        }

        public void SetDataSlotEmpty()
        {
            CallFunction("SET_DATA_SLOT_EMPTY");
        }

        public void SetInputSelect()
        {
            CallFunction("SET_INPUT_SELECT");
        }

        public void SetInputEvent(int input)
        {
            CallFunction("SET_INPUT_EVENT", input);
        }

        public void ShowCursor(bool v)
        {
            CallFunction("SHOW_CURSOR", v);
        }

        public void SetMouseInput(float v1, float v2)
        {
            CallFunction("SET_MOUSE_INPUT", v1, v2);
        }

        public void SetCursorState(int v)
        {
            CallFunction("SET_CURSOR_STATE", v);
        }
    }
}