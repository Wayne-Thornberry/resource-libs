using Proline.Resource.Client.Component;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Logging;
using Proline.Resource.Component.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Eventing
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
