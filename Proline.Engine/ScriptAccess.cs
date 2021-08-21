using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
   internal static class ScriptAccess
    {
        internal static void StartStartupScripts()
        {
            var sm = ScriptPackageManager.GetInstance();
            foreach(ScriptPackage package in sm.GetScriptPackages())
            {
                package.StartStartupScripts();
            }
        }
    }
}
