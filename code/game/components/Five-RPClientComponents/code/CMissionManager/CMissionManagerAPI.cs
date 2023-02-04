using CitizenFX.Core;
using CMissionManager.Internal;
using System;
using Console = Proline.Resource.Console;

namespace CMissionManager
{
    public partial class CMissionManagerAPI : ICMissionManagerAPI
    {
        public bool BeginMission()
        {
            try
            {
                if (GetMissionFlag()) return false;
                var instance = PoolObjectTracker.GetInstance();
                if (instance.IsTrackingObjects())
                {
                    instance.DeleteAllPoolObjects();
                    instance.ClearPoolObjects();
                }
                SetMissionFlag(true);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

        public void EndMission()
        {
            try
            {
                if (!GetMissionFlag()) return;
                var instance = PoolObjectTracker.GetInstance();
                if (instance.IsTrackingObjects())
                {
                    instance.DeleteAllPoolObjects();
                    instance.ClearPoolObjects();
                }
                SetMissionFlag(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public bool GetMissionFlag()
        {
            try
            {
                return MissionFlags.IsOnMission;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }
        public bool IsInMissionVehicle()
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
        public void SetMissionFlag(bool enable)
        {
            try
            {
                MissionFlags.IsOnMission = enable;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void TrackPoolObjectForMission(PoolObject obj)
        {
            try
            {
                if (!GetMissionFlag()) return;
                var instance = PoolObjectTracker.GetInstance();
                instance.TrackPoolObject(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return;
        }
    }
}
