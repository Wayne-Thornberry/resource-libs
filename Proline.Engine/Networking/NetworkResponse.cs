using System;

namespace Proline.Engine.Networking
{
    internal class NetworkResponse
    {
        public NetworkHeader Header { get; set; }
        public MethodResult Result { get; set; }
        public Exception Exception { get; set; } 
    }
}
