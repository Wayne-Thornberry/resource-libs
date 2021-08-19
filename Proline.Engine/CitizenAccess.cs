
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class CitizenAccess
    {
        private static IScriptSource _scriptSource;

        private CitizenAccess()
        {

        }

        public static void SetScriptSource(IScriptSource source)
        {
            _scriptSource = source;
        }

        public static IScriptSource GetInstance()
        {
            return _scriptSource; 
        }
    }
}
