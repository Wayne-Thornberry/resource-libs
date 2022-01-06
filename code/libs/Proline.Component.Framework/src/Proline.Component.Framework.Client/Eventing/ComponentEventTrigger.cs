using Proline.Resource.CFX;
using Proline.Resource.Client.Component;
using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Component
{
    public static class ComponentEventTrigger
    {
        public static void TriggerContextEvent()
        {

        }

        public static void TriggerComponentEvent()
        {

        }

        public static void TriggerGlobalEvent()
        {

        }


        // if this is called client side, the process will be to call all resource client contexts then all resource server contexts
        // if this is called server side, the process will be to call all resource server contexts, then all resource client contexts
        public static void TriggerComponentGlobalEvent(string componentName, string eventName, params object[] args)
        {
            var cfxM = CFXManager.GetInstance();
            var trigger = cfxM.Build<IFXEventTrigger>();
            var cc = ComponentContainer.GetInstance();
            var x = string.Format("global::{0}:{1}", componentName, eventName);
            trigger.TriggerEvent(x);
            trigger.TriggerSidedEvent(x);
        }

        public static void SubscribeToAComponentGlobalEvent(string componentName, string eventName, Delegate eventDelegate)
        {
            var cfxM = CFXManager.GetInstance();
            var handler = cfxM.Build<IFXEventHandler>();
            var cc = ComponentContainer.GetInstance();
            var x = string.Format("global::{0}:{1}", componentName, eventName);
            if (eventName.Contains("global::"))
                handler.RegisterEventMethod(x, eventDelegate);
        }


        public static void SubscribeToAComponentGlobalEvent(string eventName, Delegate eventDelegate)
        {
            var cfxM = CFXManager.GetInstance();
            var handler = cfxM.Build<IFXEventHandler>();
            var cc = ComponentContainer.GetInstance();
            var x = string.Format("global::{0}:{1}", cc.GetComponentName(), eventName);
            if (eventName.Contains("global::"))
                handler.RegisterEventMethod(x, eventDelegate);
        }

        public static void SubscribeToAComponentLocalEvent(string componentName, string eventName, Delegate eventDelegate)
        {
            var cfxM = CFXManager.GetInstance();
            var handler = cfxM.Build<IFXEventHandler>();
            var cc = ComponentContainer.GetInstance();
            var x = string.Format("local::{0}:{1}", componentName, eventName);
            if (eventName.Contains("local::"))
                handler.RegisterEventMethod(x, eventDelegate);
        }

        // if this is called client side, the process will be to call this resource client contexts then this resource server contexts
        // if this is called server side, the process will be to call this resource server contexts, then this resource client contexts


        // Context Event
        // Component Event
        // Global Event
        public static void TriggerComponentLocalEvent(string eventName, params object[] args)
        {
            var cfxM = CFXManager.GetInstance();
            var trigger = cfxM.Build<IFXEventTrigger>();
            var cc = ComponentContainer.GetInstance();
            var x = string.Format("local::{0}:{1}", cc.GetComponentName(), eventName);
            trigger.TriggerEvent(x);
            trigger.TriggerSidedEvent(x);
        }
    }
}
