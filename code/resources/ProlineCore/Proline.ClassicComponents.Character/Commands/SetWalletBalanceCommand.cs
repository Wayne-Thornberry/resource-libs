using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MGame.Commands
{
    public class SetWalletBalanceCommand : ModuleCommand
    {
        public SetWalletBalanceCommand() : base("SetWalletBalance")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length > 0)
            {
                long.TryParse(args[0].ToString(), out var value);
                MGame.MGameAPI.SetCharacterWalletBalance(value);
            }

        }
    }
}
