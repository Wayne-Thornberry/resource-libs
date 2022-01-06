﻿using Proline.Resource.Client.Logging;
using System;
using System.Collections.Generic;

namespace Proline.Resource.Component.Events
{
    public class CEventHandler
    {
        protected Log _log => Logger.GetInstance().GetLog();
        private List<Delegate> _actions; 
        public string EventName { get; }
        public Delegate Callback { get; }

        public CEventHandler(string eventName)
        {
            _actions = new List<Delegate>();
            EventName = eventName;
            Callback = new Action<List<object>>(OnCallBack);
        }

        internal void OnCallBack(List<object> obj)
        {
            _log.Debug("Event Called");
            foreach (var item in _actions)
            {
                item.DynamicInvoke(obj.ToArray());
            }
        }

        internal void AddCallBack(Delegate delegat)
        {
            _actions.Add(delegat);
        }

        internal void RemoveCallbacks()
        {
            _actions.Clear();
        }
    }

}