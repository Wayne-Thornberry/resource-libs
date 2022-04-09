using CitizenFX.Core;
using Proline.ResourceFramework;
using Proline.ResourceFramework.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Events
{
    [EventController]
    public class ExampleEvents
    {
        protected Log _log = new Log();
        [ControllerMethod("ExampleEvent", "Event")]
        public void OnExampleEvent()
        {
            _log.Debug("EventCalled");
        }
    }
}
