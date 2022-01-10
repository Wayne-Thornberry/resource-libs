using Proline.Resource.Framework.Server.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework.Server.Eventing
{
    public class CEvent
    {
        private string _eventName;
        private string _componentName;
        protected Log _log => Logger.GetInstance().GetLog();

        public CEvent(string componentName, string eventName)
        {
            _eventName = eventName;
            _componentName = componentName;
        }

        public void Invoke(params object[] args)
        {
            EventManager.InvokeEvent(_eventName, args);
        }

        public void AddListener(Delegate action)
        {
            EventManager.AddEventListener(_eventName, action); 
        }
    }
}
