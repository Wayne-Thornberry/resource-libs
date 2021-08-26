

using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CWorld
{
    public class WorldAPI : ComponentAPI
    {
        [Client]
        [ComponentAPI]
        public void GetNearbyEntities(out int[] entities)
        { 
            var _ht = HandleTracker.GetInstance();
            entities = _ht.Get().ToArray();
        }
    }
}
