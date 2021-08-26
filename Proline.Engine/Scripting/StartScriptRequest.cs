using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class StartScriptRequest
    {
        public StartScriptRequest(string scriptName, object[] args)
        {
            ScriptName = scriptName;
            Args = args;
        }

        internal string ScriptName { get; set; }
        internal object[] Args { get; set; }
    }
}
