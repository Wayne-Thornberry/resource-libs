using CitizenFX.Core;

namespace CMissionManager
{
    public interface ICMissionManagerAPI
    {
        bool BeginMission();
        void EndMission();
        bool GetMissionFlag();
        bool IsInMissionVehicle();
        void SetMissionFlag(bool enable);
        void TrackPoolObjectForMission(PoolObject obj);
    }
}