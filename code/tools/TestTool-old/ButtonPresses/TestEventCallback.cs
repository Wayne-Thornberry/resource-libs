using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTool
{
    internal class TestEventCallback : NativeEvent
    {
        public TestEventCallback() : base("__cfx_nui:testCallback")
        {
        }
    }
}
