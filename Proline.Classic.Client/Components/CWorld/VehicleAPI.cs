using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine;
using Proline.Engine.Five;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.APIs
{
    public static class VehicleAPI
    {
        public static void UnlockNeareastVehicle()
        {
            var vehicles = World.GetAllVehicles();
            var entity = World.GetClosest<Vehicle>(Game.PlayerPed.Position, vehicles.ToArray());
            Debugger.LogDebug(entity.Handle);

            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_NEEDS_TO_BE_HOTWIRED, entity.Handle, false);
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_IS_WANTED, entity.Handle, false);
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_IS_STOLEN, entity.Handle, false);
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_DOORS_LOCKED, entity.Handle, 1);
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_DOORS_LOCKED_FOR_ALL_PLAYERS, entity.Handle, false);
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_HAS_BEEN_DRIVEN_FLAG, entity.Handle, true);
            NativeAPI.CallNativeAPI(Hash.SET_VEHICLE_HAS_BEEN_OWNED_BY_PLAYER, entity.Handle, true);
            Debugger.LogDebug("Donedsad");
        }

    }
}
