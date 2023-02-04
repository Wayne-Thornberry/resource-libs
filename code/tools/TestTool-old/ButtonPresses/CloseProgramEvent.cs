using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTool.ButtonPresses
{
    internal class CloseProgramEvent : NativeEvent
    {
        public CloseProgramEvent() : base("__cfx_nui:closeFunction")
        {
        }
    }
}
