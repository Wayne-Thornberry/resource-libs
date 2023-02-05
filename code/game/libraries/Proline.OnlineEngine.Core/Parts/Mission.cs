using CitizenFX.Core;
using CMissionManager;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static bool BeginMission()
        {
            var api = new CMissionManagerAPI();
            return api.BeginMission();
        }

        public static void EndMission()
        {
            var api = new CMissionManagerAPI();
            api.EndMission();


        }
        public static bool GetMissionFlag()
        {

            var api = new CMissionManagerAPI();
            return api.GetMissionFlag();

        }
        public static bool IsInMissionVehicle()
        {

            var api = new CMissionManagerAPI();
            return api.IsInMissionVehicle();

        }
        public static void SetMissionFlag(bool enable)
        {

            var api = new CMissionManagerAPI();
            api.SetMissionFlag(enable);
        }
        public static void TrackPoolObjectForMission(PoolObject obj)
        {

            var api = new CMissionManagerAPI();
            api.TrackPoolObjectForMission(obj);

        }
    }
}
