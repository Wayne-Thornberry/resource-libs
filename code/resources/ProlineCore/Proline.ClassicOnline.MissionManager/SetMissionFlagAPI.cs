using System;

namespace Proline.ClassicOnline.MissionManager
{
    public static partial class MissionAPIs
    {
        public static void SetMissionFlag(bool enable)
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
    }
}
