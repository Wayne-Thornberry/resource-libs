using System.Collections.Generic;

namespace Proline.Classic.Data
{
    internal static class PosBlacklist
    {
        private static List<ScriptPositionsPair> _blacklist;

        internal static void Add(ScriptPositionsPair positionsPair)
        {
            _blacklist.Add(positionsPair);
        }

        internal static void Remove(ScriptPositionsPair positionsPair)
        {
            _blacklist.Remove(positionsPair);
        }

        internal static bool Contains(ScriptPositionsPair positionsPair)
        {
            return _blacklist.Contains(positionsPair);
        }

        internal static void Create()
        {
            _blacklist = new List<ScriptPositionsPair>();
        }
    }
}
