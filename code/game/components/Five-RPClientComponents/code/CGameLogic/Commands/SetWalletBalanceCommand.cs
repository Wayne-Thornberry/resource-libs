
using Proline.Resource.Framework;

namespace CGameLogic.Commands
{
    public class SetWalletBalanceCommand : ResourceCommand
    {
        public SetWalletBalanceCommand() : base("SetWalletBalance")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var api = new CGameLogicAPI();
            if (args.Length > 0)
            {
                long.TryParse(args[0].ToString(), out var value);
                api.SetCharacterWalletBalance(value);
            }

        }
    }
}
