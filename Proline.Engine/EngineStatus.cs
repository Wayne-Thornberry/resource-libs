using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal static class EngineStatus
    {
        internal static bool IsExtensionsInitialized { get; set; }
        internal static bool IsScriptsInitialized { get; set; }
        internal static bool IsComponentsInitialized { get; set; }
        internal static bool IsEngineInitialized { get; set; }
    }
}
