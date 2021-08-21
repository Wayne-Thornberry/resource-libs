
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class CitizenAccess
    {
        private static IScriptSource _scriptSource;

        private CitizenAccess()
        {

        }

        internal static void SetScriptSource(IScriptSource source)
        {
            _scriptSource = source;
        }

        public static IScriptSource GetInstance()
        {
            return _scriptSource; 
        }
    }
}
