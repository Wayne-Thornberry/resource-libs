using CitizenFX.Core;
using Proline.ResourceFramework;
using Proline.ResourceFramework.Eventing;
using Proline.ResourceFramework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Events
{
    [EventContainer]
    public class ExampleEvents
    {
        protected Log _log = Logger.GetInstance().GetLog();
        [FiveMEvent("ExampleEvent")]
        public void OnExampleEvent()
        {
            _log.Debug("EventCalled");
        }
    }
}
