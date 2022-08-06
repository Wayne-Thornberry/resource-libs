using CitizenFX.Core;
using Proline.ClassicOnline.MissionManager.Internal;
using System;

namespace Proline.ClassicOnline.MissionManager
{
    public static partial class MissionAPIs
    { 
        public static bool IsInMissionVehicle()
        {
            try
            { 
                var instance = PoolObjectTracker.GetInstance();
                return Game.PlayerPed.IsInVehicle() && instance.ContainsPoolObject(Game.PlayerPed.CurrentVehicle);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }
    }
}
