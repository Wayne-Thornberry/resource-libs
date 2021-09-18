using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Debugging
{
    public interface IDebugConsole 
    {
        void Write(object data, bool replicate);
        void WriteLine(object data, bool replicate);
    }
}
