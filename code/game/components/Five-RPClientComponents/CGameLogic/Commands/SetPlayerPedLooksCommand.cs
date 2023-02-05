using CitizenFX.Core;


using Proline.Resource.Framework;

namespace CGameLogic.Commands
{
    public class SetPlayerPedLooksCommand : ResourceCommand
    {
        public SetPlayerPedLooksCommand() : base("SetPlayerPedLooks")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length == 3)
            {
                var api = new CGameLogicAPI();
                var Father = int.Parse(args[0].ToString());
                var Mother = int.Parse(args[1].ToString());
                var Resemblence = float.Parse(args[2].ToString());
                api.SetPedLooks(Game.PlayerPed.Handle, Mother, Father, Resemblence, 0f);
            }

        }
    }
}
