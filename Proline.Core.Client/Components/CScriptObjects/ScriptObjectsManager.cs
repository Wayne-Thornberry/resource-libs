using CitizenFX.Core.Native;
using Newtonsoft.Json;

using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CScriptObjects
{
    internal static class ScriptObjectsManager
    {
        private static ScriptObjectPair[] _scriptObjectPairs;

        internal static IEnumerable<ScriptObjectPair> GetScriptObjectPairs()
        {
            return _scriptObjectPairs;
        }

        internal static bool HasScriptObjectPairs()
        {
            return _scriptObjectPairs.Length > 0;
        }

        internal static void AddScriptObjectPairs(ScriptObjectPair[] scriptObjectPairs)
        {
            _scriptObjectPairs = scriptObjectPairs;
        }
    }
}
