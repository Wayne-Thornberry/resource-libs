using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.Networking
{
    public class EventRequest
    {
        public string CallbackGUID { get; set; }
        public object Content { get; set; }
    }
}
