using System.Linq;
using CitizenFX.Core;
using Proline.Game;
using Proline.Game.Debugging;

namespace Proline.Classic.Engine.Components.CWorld
{
    public static class VehicleAPI
    {
        public static void UnlockNeareastVehicle()
        {
            var vehicles = World.GetAllVehicles();
            var entity = World.GetClosest<Vehicle>(CitizenFX.Core.Game.PlayerPed.Position, vehicles.ToArray());
            //var console = new Log(new DebugConsole());
            //console.LogDebug(entity.Handle);

            //NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_NEEDS_TO_BE_HOTWIRED, entity.Handle, false);
            //NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_IS_WANTED, entity.Handle, false);
            //NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_IS_STOLEN, entity.Handle, false);
            //NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_DOORS_LOCKED, entity.Handle, 1);
            //NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_DOORS_LOCKED_FOR_ALL_PLAYERS, entity.Handle, false);
            //NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_HAS_BEEN_DRIVEN_FLAG, entity.Handle, true);
            //NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_HAS_BEEN_OWNED_BY_PLAYER, entity.Handle, true);
            //console.LogDebug("Donedsad");
        }

    }
}
