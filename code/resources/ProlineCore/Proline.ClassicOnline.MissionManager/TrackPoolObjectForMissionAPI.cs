using CitizenFX.Core;
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
        public static void TrackPoolObjectForMission(PoolObject obj)
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
