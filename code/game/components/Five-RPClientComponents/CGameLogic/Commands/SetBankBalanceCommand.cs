
using Proline.Resource.Framework;

namespace CGameLogic.Commands
{
    public class SetBankBalanceCommand : ResourceCommand
    {
        public SetBankBalanceCommand() : base("SetBankBalance")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var api = new CGameLogicAPI();
            if (args.Length > 0)
            {
                long.TryParse(args[0].ToString(), out var value);
                api.SetCharacterBankBalance(value);
            }

        }
    }
}
