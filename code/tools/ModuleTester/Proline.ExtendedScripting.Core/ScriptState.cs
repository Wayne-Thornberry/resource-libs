using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ExtendedScripting.Core
{
    public enum ScriptState
    {
        Inactive = 0,
        AwaitingStart = 5,
        Loading = 1,
        Running = 2,
        Paused = 3,
        Stopped = 4,
    }
}
