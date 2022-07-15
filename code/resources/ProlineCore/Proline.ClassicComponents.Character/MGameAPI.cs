using CitizenFX.Core;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.MData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MGame
{
    public static class MGameAPI
    {
        public static void SaveCurrentCar()
        {
            if (Game.PlayerPed.CurrentVehicle != null)
            {
                var vehicle = Game.PlayerPed.CurrentVehicle;
                API.CreateDataFile();
                API.AddDataFileValue("VehicleModelHash", vehicle.Model.Hash);
                API.AddDataFileValue("VehiclePosition", vehicle.Position);
                API.AddDataFileValue("VehicleHeading", vehicle.Heading);
                API.AddDataFileValue("VehicleHealth", vehicle.Health);

            }
        }

        public static void SetCharacterBankBalance(long value)
        {
            try
            {
                var character = CharacterGlobals.Character;
                character.BankBalance = value;
                var bankBalanceStat = MPStat.GetStat<long>("BANK_BALANCE");
                bankBalanceStat.SetValue(value);
            }
            catch (Exception e)
            {
                MDebug.MDebugAPI.LogError(e);
            }
        }

        public static void SetCharacterWalletBalance(long value)
        {
            try
            {
                var character = CharacterGlobals.Character;
                character.WalletBalance = value;
                var walletBalanceStat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                walletBalanceStat.SetValue(value);
            }
            catch (Exception e)
            {
                MDebug.MDebugAPI.LogError(e);
            }
        }
    }
}
