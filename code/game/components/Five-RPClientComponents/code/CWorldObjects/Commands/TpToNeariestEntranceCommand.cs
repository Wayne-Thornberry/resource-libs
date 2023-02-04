using CitizenFX.Core;

using Proline.Resource.Framework;

namespace CWorldObjects.Commands
{
    public class TpToNeariestEntrance : ResourceCommand
    {
        public TpToNeariestEntrance() : base("TpToNeariestEntrance")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var api = new CWorldObjectsAPI();
            var entrance = api.GetBuildingPosition(api.GetNearestBuilding());
            Game.PlayerPed.Position = entrance;
        }
    }
}
