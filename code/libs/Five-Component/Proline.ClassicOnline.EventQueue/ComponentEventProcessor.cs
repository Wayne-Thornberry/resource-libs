using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.EventQueue
{
    public class ComponentEventProcessor
    {  
        public async Task ProcessQueue()
        { 
            while (ComponentEvent.HasQueueGotItems())
            {
                var eventCall = ComponentEvent.GetNextQueuedEventCall();
                eventCall.Invoke();
            }
        }
    }
}
