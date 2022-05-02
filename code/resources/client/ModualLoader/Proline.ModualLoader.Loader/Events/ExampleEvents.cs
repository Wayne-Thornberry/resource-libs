using Proline.Resource.Framework;
using Proline.Resource.Logging;

namespace Proline.ResourceLoader.Main.Events
{
    [ResourceEvents]
    public class ExampleEvents
    {
        protected Log _log = new Log();
        [ResourceEvent("ExampleEvent", "Event")]
        public void OnExampleEvent()
        {
            _log.Debug("EventCalled");
        }
    }
}
