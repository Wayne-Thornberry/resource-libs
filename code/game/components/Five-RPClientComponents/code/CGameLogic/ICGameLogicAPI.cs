using CGameLogic.Data;
using CitizenFX.Core;

namespace CGameLogic
{
    public interface ICGameLogicAPI
    {
        void AddValueToBankBalance(long value);
        void AddValueToBankBalance(object payout);
        void AddValueToWalletBalance(long value);
        void DeletePersonalVehicle();
        long GetCharacterBankBalance();
        long GetCharacterMaxWalletBalance();
        long GetCharacterWalletBalance();
        CharacterStats GetCharacterStats();
        CharacterLooks GetPedLooks(int pedHandle);
        Entity GetPersonalVehicle();
        bool HasBankBalance(long price);
        bool HasCharacter();
        bool IsInPersonalVehicle();
        void SetCharacter(int character);
        void SetCharacterBankBalance(long value);
        void SetCharacterMaxWalletBalance(int value);
        void SetCharacterPersonalVehicle(int handle);
        void SetCharacterWalletBalance(long value);
        void SetPedGender(int handle, char gender);
        void SetPedLooks(int handle, int mother, int father, float parentResemblence, float skinResemblence);
        void SetPedOutfit(string outfitName, int handle);
        void SubtractValueFromBankBalance(long value);
        void SubtractValueFromWalletBalance(long value);
        int CreateNewCharacter();
        void SetStatLong(string statName, long value);
    }
}