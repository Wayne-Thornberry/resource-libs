

using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CExampleComponent
{
    public class ExampleAPI : ComponentAPI
    {

        [Client]
        [ComponentAPI]
        public void ExampleControl(string x, string y, string z)
        {
            Debugger.LogDebug(x);
            Debugger.LogDebug(y);
            Debugger.LogDebug(z);
        }


        [Client]
        [ComponentAPI]
        public void SetPlayerAsPartOfPoliceGroup()
        {
            //var vehicles = World.GetAllPeds();
            //var entity = World.GetClosest<Ped>(Game.PlayerPed.Position, vehicles.ToArray());
            //Debugger.LogDebug(entity.PedGroup);
            //var id = // API.GetPedGroupIndex(entity.Handle);
            //Debugger.LogDebug(id);
            //// API.SetPedAsGroupMember(Game.PlayerPed.Handle, id);
            //Debugger.LogDebug("ids");
        }

        [Client]
        [ComponentAPI]
        public void UnlockNeareastVehicle()
        {
            //var vehicles = World.GetAllVehicles();
            //var entity = World.GetClosest<Vehicle>(Game.PlayerPed.Position, vehicles.ToArray());
            //Debugger.LogDebug(entity.Handle);

            //Native// API.CallNativeAPI((ulong)Hash.SET_VEHICLE_NEEDS_TO_BE_HOTWIRED, entity.Handle, false);
            //// API.SetVehicleIsWanted(entity.Handle, false);
            //// API.SetVehicleIsStolen(entity.Handle, false);
            //// API.SetVehicleDoorsLocked(entity.Handle, 1);
            //// API.SetVehicleDoorsLockedForAllPlayers(entity.Handle, false);
            //// API.SetVehicleHasBeenDrivenFlag(entity.Handle, true);
            //// API.SetVehicleHasBeenOwnedByPlayer(entity.Handle, true);
            //Debugger.LogDebug("Donedsad");
        }
    }
}
