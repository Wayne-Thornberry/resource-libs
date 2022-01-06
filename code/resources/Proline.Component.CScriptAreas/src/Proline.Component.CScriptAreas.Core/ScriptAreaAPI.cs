using CitizenFX.Core;
using Proline.Component.Framework.Client.Access;
using System;

namespace Proline.Component.CScriptAreas.Core
{
    public static class ScriptAreaAPI
    {
        public static bool IsPointWithinActivationRange(Vector3 position)
        {
            var exports = ExportManager.GetExports();
            return exports["Proline.Component.CScriptAreas"].IsPointWithinActivationRange(position);
        }
    }
}
