using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MWorld;
using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class TpToNeariestEntrance : ResourceCommand
    {
        public TpToNeariestEntrance() : base("TpToNeariestEntrance")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var entrance = WorldAPI.GetBuildingPosition(WorldAPI.GetNearestBuilding());
            Game.PlayerPed.Position = entrance;
        }
    }
}
