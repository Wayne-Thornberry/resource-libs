using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.EngineFramework.Scripting
{
    public abstract class StartupScript : EngineScript
    {
        public StartupScript()
        {
            ScriptName = this.GetType().Name;
        }
    }
}
