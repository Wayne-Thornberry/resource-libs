using CitizenFX.Core;
using CitizenFX.Core.Native; 
using Proline.Resource.IO;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public abstract class ComponentContext : BaseScript
    {
        protected Log _log;
        public string Name { get; internal set; }

        public virtual void OnLoad() { }
        public virtual void OnStart() { }
        public virtual async Task OnTick() { }

        public ComponentContext()
        {
            EventManager.SetHandlerCollection(EventHandlers);
            ExportManager.SetExportDictionary(Exports);
            Globals.SetGlobalBag(GlobalState);
            _log = Logger.GetInstance().GetLog();
            Tick += InternalOnTick;
        }

        [EventHandler("onResourceStart")]
        public virtual void OnResourceStart(string resourceName) { }

        [EventHandler("onResourceStarting")]
        public virtual void OnResourceStarting(string resourceName) { }

        [EventHandler("onResourceStop")]
        public virtual void OnResourceStop(string resourceName) { }

        private bool _isSetup;
        
        protected void RegisterScriptContext(ResourceScript context)
        {
            foreach (var method in GetMethods(context, typeof(TickAttribute)))
            {
                if (method.IsStatic)
                    this.RegisterTick((Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), method));
                else
                    this.RegisterTick((Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), context, method.Name));
            }
        }

        protected void RegisterEventContext(ResourceEventHandler context)
        {
            foreach (var method in GetMethods(context,typeof(EventHandlerAttribute)))
            {
                var parameters = method.GetParameters().Select(p => p.ParameterType).ToArray();
                var actionType = Expression.GetDelegateType(parameters.Concat(new[] { typeof(void) }).ToArray());
                var attribute = method.GetCustomAttribute<EventHandlerAttribute>();
 

                if (method.IsStatic)
                    this.RegisterEventHandler(attribute.Name, Delegate.CreateDelegate(actionType, method));
                else
                    this.RegisterEventHandler(attribute.Name, Delegate.CreateDelegate(actionType, context, method.Name));
            }
        }
        IEnumerable<MethodInfo> GetMethods(object x, Type t)
        {
            return x.GetType().GetMethods().Where(m => m.GetCustomAttributes(t, false).Length > 0);
        }

        internal void RegisterEventHandler(string eventName, Delegate callback)
        {
            EventHandlers[eventName] += callback;
        }

        internal void RegisterTick(Func<Task> tick)
        {
            Tick += tick;
        }

        private async Task InternalOnTick()
        {
            try
            {
                if (!_isSetup)
                { 
                    OnLoad();
                    OnStart();
                    _isSetup = true;
                }
                await OnTick();
            }
            catch (Exception e)
            {
                Tick -= InternalOnTick;
                _log.Error(e.ToString(), true);
                throw;
            }
            finally
            {
                //Tick -= InternalOnTick;
            }
        }  
    }
}
