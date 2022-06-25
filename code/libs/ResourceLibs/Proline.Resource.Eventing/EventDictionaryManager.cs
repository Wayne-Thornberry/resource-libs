using CitizenFX.Core;

namespace Proline.Resource.Eventing
{
    public class EventDictionaryManager
    {
        private static EventDictionaryManager _instance;
        private EventHandlerDictionary _eventHandlerDictionary;

        public EventDictionaryManager()
        {

        }

        public void SetEventHandlerDictionary(EventHandlerDictionary eventHandler)
        {
            _eventHandlerDictionary = eventHandler;
        }

        public EventHandlerDictionary GetEventHandlerDictionary()
        {
            return _eventHandlerDictionary;
        }

        public static EventDictionaryManager GetInstance()
        {
            if (_instance == null)
                _instance = new EventDictionaryManager();
            return _instance;
        }
    }
}

