using CGameLogic;
using CitizenFX.Core;

using Proline.Resource.Framework;

namespace CNetConnection.Commands
{
    public class BuyRandomOutfitCommand : ResourceCommand
    {
        public BuyRandomOutfitCommand() : base("BuyRandomOutfit")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var api = new CGameLogicAPI();
            if (api.GetCharacterBankBalance() > 250)
            {
                Game.Player.Character.Style.RandomizeOutfit();
                Game.Player.Character.Style.RandomizeProps();
                api.SetCharacterBankBalance(250);
            }
        }
    }
}
