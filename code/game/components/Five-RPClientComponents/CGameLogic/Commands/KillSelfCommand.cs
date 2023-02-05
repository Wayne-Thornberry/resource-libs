using CitizenFX.Core;
using Proline.Resource.Framework;

namespace CGameLogic.Commands
{
    public class KillSelfCommand : ResourceCommand
    {
        public KillSelfCommand() : base("KillSelf")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            Game.PlayerPed.Kill();
        }
    }
}
