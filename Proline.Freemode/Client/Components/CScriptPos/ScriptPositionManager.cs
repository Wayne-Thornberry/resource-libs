using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CScriptPos
{
    internal static class ScriptPositionManager
    {
        private static ScriptPositionsPair[] _scriptPositionPairs;

        internal static IEnumerable<ScriptPositionsPair> GetScriptPositionsPairs()
        {
            return _scriptPositionPairs;
        }

        internal static bool HasScriptPositionPairs()
        {
            return _scriptPositionPairs.Length > 0;
        }

        internal static void AddScriptPositionPairs(ScriptPositionsPair[] scriptPositionPairs)
        {
            _scriptPositionPairs = scriptPositionPairs;
        }
    }
}
