using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace CCoreSystem.Internal.Events
{
    public class CEventNetworkPedDied
    {
        public void OnEventInvoked(object[] args)
        {
            Console.WriteLine(GetType().Name + " Invoked");
        }
    }
}
