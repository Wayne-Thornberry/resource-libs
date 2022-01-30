using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public class NetworkListener
    { 
        private Guid _guid;
        private Delegate _action; 
        protected string GUID => _guid.ToString();

        public bool IsListening { get; private set; }

        public NetworkListener()
        {
            _guid = Guid.NewGuid(); 
        } 

        public void BeginListening()
        {
            //CFXEventHandler.RegisterEventHandler(GUID, _action);
            IsListening = true;
        }

        public void EndListening()
        {
            //CFXEventHandler.UnregisterEventHandler(GUID);
            IsListening = false;
        }

        internal void SetCallback(Delegate action)
        {
            _action = action;
        }
    }
}
