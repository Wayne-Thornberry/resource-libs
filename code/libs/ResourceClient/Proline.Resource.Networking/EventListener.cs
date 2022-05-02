using System;

namespace Proline.Resource.Networking
{
    public class EventListener
    { 
        private Guid _guid;
        private Delegate _action;
        public Delegate Action => _action;
        public string GUID => _guid.ToString();

        public bool IsListening { get; private set; }

        public EventListener()
        {
            _guid = Guid.NewGuid(); 
        } 

        public void BeginListening()
        { 
            IsListening = true;
        }

        public void EndListening()
        { 
            IsListening = false;
        }

        public void SetCallback(Delegate action)
        {
            _action = action;
        }
    }
}
