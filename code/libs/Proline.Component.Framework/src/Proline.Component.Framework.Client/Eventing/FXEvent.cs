using CitizenFX.Core;
using Proline.Resource.CFX;
using Proline.Resource.Client.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Component.Events
{
    public class FXEvent
    {
        public string Name => _eventName;

        private string _eventName;
        private List<Delegate> _callbacks;
     //   public int CallBackCount => FXEventManager.GetHandlerCollection().Values.Where(e=>e.)

        internal FXEvent(string eventName)
        {
            _callbacks = new List<Delegate>();
        }

        internal void AddListener(Delegate p)
        { 
            FXEventManager.GetHandlerCollection().Add(_eventName, p);
        }

        internal void Invoke(params object[] args)
        {
            BaseScript.TriggerEvent(_eventName, args);
        }

        internal IEnumerable<Delegate> GetCallbacks()
        {
            return _callbacks;
        }

        //~FXEvent()
        //{
        //    RemoveAllCallbacks();
        //}

        //internal void RemoveAllCallbacks()
        //{
        //    foreach (var item in _callbacks)
        //    {
        //        RemoveCallback(item);
        //    }
        //}

        //private void RemoveCallback(Delegate e)
        //{
        //    _callbacks.Remove(e); 
        //    if (_callbacks.Count == 0 && IsActive)
        //    {
        //        CFXEventHandler.UnregisterEventHandler(_eventName);
        //        IsActive = false;
        //    }
        //}

        //internal void AddCallback(Delegate e)
        //{
        //    _callbacks.Add(e);
        //    if (_callbacks.Count > 0 && !IsActive)
        //    {
        //        CFXEventHandler.RegisterEventHandler(_eventName, new Action<object[]>(EventCallback));
        //        IsActive = true;
        //    }
        //} 

        //public void TriggerEvent(ComponentContextType type)
        //{
        //    CFXBaseScript.TriggerEvent(_eventName);
        //    // if on server
        //    if (type == ComponentContextType.SERVER)
        //        CFXBaseScript.TriggerClientEvent(_eventName);
        //    // if on client
        //    if(type == ComponentContextType.CLIENT)
        //        CFXBaseScript.TriggerServerEvent(_eventName);
        //}

        //private void EventCallback(object[] arg2)
        //{
        //    foreach (var item in _callbacks)
        //    {
        //        item.DynamicInvoke(arg2);
        //    }
        //}
    }
}
