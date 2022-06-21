using CitizenFX.Core;
using Proline.Resource.Logging; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Server.Events
{
    ///[EventContainer]
    public class ExampleEvents
    {
        protected Log _log => new Log();
        //[FiveMEvent("ExampleEvent")]
        public void OnExampleEvent()
        {
            _log.Debug("EventCalled");
        }
    }
}
