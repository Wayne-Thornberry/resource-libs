using System.Collections.Generic;

namespace Proline.Classic.Engine.Components.CScriptPosition
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
