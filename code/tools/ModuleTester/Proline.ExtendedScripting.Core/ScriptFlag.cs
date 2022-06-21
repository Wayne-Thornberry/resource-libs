using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ExtendedScripting.Core
{
    [Flags]
    public enum ScriptFlag : short
    {
        None = 0,
        Loaded = 1,
        Started = 2,
        MarkedForTermination = 4,
        RequestToStart = 8,
        Terminated = 16
    }
}
