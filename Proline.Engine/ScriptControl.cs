using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
   internal static class ScriptControl
    {
        internal static void StopExistingScript(string scriptName)
        {
            var scripts = ScriptManager.GetScripts(scriptName);
            foreach (var item in scripts)
            {
                item.Stage = -1;
            }
        }
    }
}
