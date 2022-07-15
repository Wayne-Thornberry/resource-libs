using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.GCharacter;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class SetWalletBalanceCommand : ModuleCommnad
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
