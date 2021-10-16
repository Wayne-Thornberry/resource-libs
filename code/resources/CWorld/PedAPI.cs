using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native; 
using Proline.Game.Debugging;

namespace Proline.Classic.Engine.Components.CWorld
{
    public static class PedAPI
    {
        public static void SetPlayerAsPartOfPoliceGroup()
        {
            var vehicles = World.GetAllPeds();
            var entity = World.GetClosest<Ped>(CitizenFX.Core.Game.PlayerPed.Position, vehicles.ToArray());
            //var log = new Log(new DebugConsole());
            //log.LogDebug(entity.PedGroup);
            var id = API.GetPedGroupIndex(entity.Handle);
            //log.LogDebug(id);
            API.SetPedAsGroupMember(CitizenFX.Core.Game.PlayerPed.Handle, id);
            //log.LogDebug("ids");
        }
    }
}
