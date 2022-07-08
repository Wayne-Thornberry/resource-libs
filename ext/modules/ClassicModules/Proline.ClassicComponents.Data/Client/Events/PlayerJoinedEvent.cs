using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData
{
    internal partial class PlayerJoinedEvent : LoudEvent
    {  
        protected override object OnEventTriggered(params object[] args)
        {
            return null;
        }
    }
}
