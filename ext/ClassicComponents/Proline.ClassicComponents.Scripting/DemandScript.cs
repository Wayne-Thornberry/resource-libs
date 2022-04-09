using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.EngineFramework.Scripting
{
    public abstract class DemandScript : EngineScript
    {
        public DemandScript(string name)
        {
            ScriptName = name;
        }
    }
}
