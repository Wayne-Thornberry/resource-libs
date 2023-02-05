using CGameLogic.Data;
using CitizenFX.Core;

namespace CGameLogic.Internal
{
    internal static class Character
    {
        internal static int MaxWalletCapacity { get; set; }
        internal static Entity PersonalVehicle { get; set; }
        internal static PlayerCharacter PlayerCharacter { get; set; }
        internal static long BankBalance { get; set; }
        internal static long WalletBalance { get; set; }
    }
}
