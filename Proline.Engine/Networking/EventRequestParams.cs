using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Networking
{
    public class EventRequestParams
    {
        public string GUID { get; set; }
        public string ComponentName { get; set; }
        public string MethodName { get; set; }
        public string MethodArgs { get; set; }
    }
}
