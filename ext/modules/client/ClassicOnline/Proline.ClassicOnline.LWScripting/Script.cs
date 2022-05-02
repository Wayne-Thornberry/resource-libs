using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting
{
    public static class Script
    {
        private static Log _log => new Log();
        public static int StartNewScript(string scriptName, params object[] args)
        {
            var sm = LWScriptManager.GetInstance();
            if (sm.DoesScriptExist(scriptName))
            {
                var id = sm.StartNewScriptInstance(scriptName, args);
                return id;
            }
            _log.Debug($"Could not start {scriptName}");
            return -1;
        }

        public static void MarkScriptAsNoLongerNeeded()
        {
            throw new ScriptTerminatedException();
        }
    }
}
