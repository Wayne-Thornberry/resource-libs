using Proline.ClassicOnline.MissionManager.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MissionManager
{
    public static partial class MissionAPIs
    {
        public static bool BeginMission()
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
    }
}
