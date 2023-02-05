using CDebugActions;
using CScriptAreas.Data;

using System.Collections.Generic;

namespace CScriptAreas.Entity
{
    internal class ScriptPositionManager
    {
        private static ScriptPositionsPair[] _scriptPositionPairs;
        private static ScriptPositionManager _instance;

        public ScriptPositionManager()
        {
            _scriptPositionPairs = new ScriptPositionsPair[0];
        }

        internal IEnumerable<ScriptPositionsPair> GetScriptPositionsPairs()
        {
            return _scriptPositionPairs;
        }

        internal bool HasScriptPositionPairs()
        {
            return _scriptPositionPairs.Length > 0;
        }

        internal static ScriptPositionManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ScriptPositionManager();
            }
            return _instance;
        }

        internal void AddScriptPositionPairs(ScriptPositionsPair[] scriptPositionPairs)
        {
            var api = new CDebugActionsAPI();
            api.LogDebug($"Added {scriptPositionPairs.Length} to track for script positions");
            _scriptPositionPairs = scriptPositionPairs;
        }
    }
}
