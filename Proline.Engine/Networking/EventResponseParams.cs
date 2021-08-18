using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Networking
{
    public class EventResponseParams
    {
        public string GUID { get; set; }
        public object Value { get; set; }
        public bool IsException { get; set; }
    }
}
