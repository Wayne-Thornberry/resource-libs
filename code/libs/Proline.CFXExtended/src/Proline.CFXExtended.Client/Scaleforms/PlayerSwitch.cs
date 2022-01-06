
using System.Threading.Tasks;

namespace Proline.CFXExtended.Core.Scaleforms
{
    enum PlayerSwitchChar
    {
        MICHEAL,
        TREVOR,
        FRANKLIN,
        MP_CHAR,
        CHOP,
    }

    enum PlayerSwitchState
    {
        NOTHING,
        AVALIBLE,
        LOCKED,
        HIDDEN,
    }

    public class PlayerSwitch : ScaleformUI
    {
        public PlayerSwitch() : base("PLAYER_SWITCH")
        {

        }
        public void SetSwitchVisible(bool v)
        {
            CallFunction("SET_SWITCH_VISIBLE", v);
        }
        public void SetSwitchSlot(int index, int Stateenum, int Charenum, bool Selected, string PedheadshotTxtString)
        {
            CallFunction("SET_SWITCH_SLOT", index, Stateenum, Charenum, Selected, PedheadshotTxtString);
        }
        public void SetMultiplayerHead(int Newtxd)
        {

        }
        public void SetSwitchHinted(int index, int Hinted)
        {

        }
        public void SetSwitchHintedAll(int hinted0, int Hinted1, int Hinted2, int Hinted3)
        {

        }
        public void SetPlayerDamage(int index, bool Bvisible, bool Bflash)
        {

        }
        public void SetSwitchCounterAll(int count0, int Count1, int Count2, int Count3)
        {

        }
        public void SetPlayerSelected(int sindex)
        {
            CallFunction("SET_PLAYER_SELECTED", sindex);
        }
        public void SetMpLabel(string str)
        {

        }
        public async Task<int> GetSwitchSelected()
        {
            return await CallFunction<int>("GET_SWITCH_SELECTED");
        }
        public void AddTxdRefResponse(int txd, string Uniquestr, bool Success)
        {

        }
    }
}
