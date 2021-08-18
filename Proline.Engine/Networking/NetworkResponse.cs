using System;

namespace Proline.Engine.Networking
{
    public class NetworkResponse
    {
        public NetworkHeader Header { get; set; }
        public MethodResult Result { get; set; }
        public Exception Exception { get; set; } 
    }
}
