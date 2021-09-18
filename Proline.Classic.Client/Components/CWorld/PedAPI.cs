using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.APIs
{
    public static class PedAPI
    {
        public static void SetPlayerAsPartOfPoliceGroup()
        {
            var vehicles = World.GetAllPeds();
            var entity = World.GetClosest<Ped>(Game.PlayerPed.Position, vehicles.ToArray());
            Debugger.LogDebug(entity.PedGroup);
            var id = API.GetPedGroupIndex(entity.Handle);
            Debugger.LogDebug(id);
            API.SetPedAsGroupMember(Game.PlayerPed.Handle, id);
            Debugger.LogDebug("ids");
        }
    }
}
