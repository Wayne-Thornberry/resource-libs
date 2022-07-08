using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection
{
    internal partial class PlayerJoinedEvent : LoudEvent
    {
        public static void InvokeEvent(string username)
        {  
            _event.Invoke(username); 
        }

        protected override object OnEventTriggered(params object[] args)
        {
            return null;
        }
    }
}
