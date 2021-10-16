using System.Linq;
using Proline.Classic.Engine.Components.CScriptObjects;

namespace Proline.Classic.Engine.Components.CWorld
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
