using CGameLogic;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static void SetCharacterBankBalance(long value)
        {
            var api = new CGameLogicAPI();
            api.SetCharacterBankBalance(value);
        }

        public static long GetCharacterMaxWalletBalance()
        {
            var api = new CGameLogicAPI();
            return api.GetCharacterMaxWalletBalance();
        }
        public static void SetCharacterMaxWalletBalance(int value)
        {

            var api = new CGameLogicAPI();
            api.SetCharacterMaxWalletBalance(value);
        }


        public static bool HasBankBalance(long price)
        {

            var api = new CGameLogicAPI();
            return api.HasBankBalance(price);
        }

        public static void AddValueToBankBalance(long value)
        {
            var api = new CGameLogicAPI();
            api.AddValueToBankBalance(value);

        }


        public static void SubtractValueFromBankBalance(long value)
        {
            var api = new CGameLogicAPI();
            api.SubtractValueFromBankBalance(value);

        }

        public static void SetCharacterWalletBalance(long value)
        {
            var api = new CGameLogicAPI();
            api.SetCharacterWalletBalance(value);

        }

        public static void AddValueToWalletBalance(long value)
        {
            var api = new CGameLogicAPI();
            api.AddValueToWalletBalance(value);

        }

        public static void SubtractValueFromWalletBalance(long value)
        {
            var api = new CGameLogicAPI();
            api.SubtractValueFromWalletBalance(value);

        }


        public static long GetCharacterWalletBalance()
        {

            var api = new CGameLogicAPI();
            return api.GetCharacterWalletBalance();
        }

        public static long GetCharacterBankBalance()
        {
            var api = new CGameLogicAPI();
            return api.GetCharacterBankBalance();

        }
    }
}
