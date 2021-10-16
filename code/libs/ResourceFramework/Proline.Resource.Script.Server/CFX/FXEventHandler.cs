using CitizenFX.Core;
using Proline.Resource.CFX;
using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Script.CFX
{
    public class FXEventHandler : CFXObject, IFXEventHandler
    {
        private EventHandlerDictionary _eventHandlerDictionary;
        private List<string> _registeredEventNames;

        public FXEventHandler(EventHandlerDictionary ehd)
        {
            _eventHandlerDictionary = ehd;
            _registeredEventNames = new List<string>();
        }

        public void RegisterEventMethod(string eventName, Delegate x)
        {
            if (!_registeredEventNames.Contains(eventName))
            {
                _eventHandlerDictionary.Add(eventName, x);
                _registeredEventNames.Add(eventName);
            }
        }


        public void UnregisterEventMethod(string eventName)
        {
            if (_registeredEventNames.Contains(eventName))
            {
                _eventHandlerDictionary.Remove(eventName);
                _registeredEventNames.Remove(eventName);
            }
        }
    }
}
