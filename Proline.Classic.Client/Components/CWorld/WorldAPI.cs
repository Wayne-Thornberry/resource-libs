using CitizenFX.Core;
using Proline.Classic.Managers;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.APIs
{
    public static class WorldAPI
    {
        public static void AttachBlipsToGasStations()
        {
            //foreach (var item in _x)
            //{
            //    var vector = new Vector3(item.X, item.Y, item.Z);
            //    Debugger.LogDebug(item.Name);
            //    _blips.Add(World.CreateBlip(vector));
            //}
        }

        public static void GetNearbyEntities(out int[] entities)
        {
            var _ht = HandleTracker.GetInstance();
            entities = _ht.Get().ToArray();
        }
    }
}
