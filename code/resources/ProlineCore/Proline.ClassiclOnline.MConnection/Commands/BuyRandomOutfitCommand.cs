using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.CFXExtended.Core;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class BuyRandomOutfitCommand : ModuleCommand
    {
        public BuyRandomOutfitCommand() : base("BuyRandomOutfit")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");

            if (stat2.GetValue() > 250)
            {
                stat2.SetValue(stat2.GetValue() - 250);
                Game.Player.Character.Style.RandomizeOutfit();
                Game.Player.Character.Style.RandomizeProps();
            }
        }
    }
}
