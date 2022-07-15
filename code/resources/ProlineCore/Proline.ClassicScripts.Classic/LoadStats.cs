using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class LoadStats
    {
        public async Task Execute(object[] args, CancellationToken token)
        { 
            if(CharacterGlobals.Character != null)
            {
                if(CharacterGlobals.Character.Stats != null)
                {
                    var stats = CharacterGlobals.Character.Stats;
                    var walletBalanceStat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                    var bankBalanceStat = MPStat.GetStat<long>("BANK_BALANCE");


                    var walletBalance = stats.GetStat("WALLET_BALANCE");
                    var bankBalance = stats.GetStat("BANK_BALANCE");

                    MDebug.MDebugAPI.LogDebug(walletBalance);
                    MDebug.MDebugAPI.LogDebug(bankBalance);

                    walletBalanceStat.SetValue(Convert.ToInt64(CharacterGlobals.Character.WalletBalance));
                    bankBalanceStat.SetValue(Convert.ToInt64(CharacterGlobals.Character.BankBalance));
                }
            }
        }
    }
}
