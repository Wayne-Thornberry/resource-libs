using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ResourceFramework.Logging;
using Proline.ResourceFramework.Networking;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.ResourceFramework
{
    public abstract class ComponentContext : BaseScript, IBaseScriptMethods
    {
        protected Log _log = new Log(); 
        private Assembly _sourcAssembly;   

        public string Name { get; internal set; } 

        public ComponentContext() 
        {
            _sourcAssembly = Assembly.GetCallingAssembly(); 

            //_context = ResourceType.CLIENT;
            //_env.AddAPI<DebugAPI>();
            //_socket = new EventSocket(API.GetCurrentResourceName());
            Tick += InternalOnTick;
        }

         
        public void RegisterExport(string eventName, Delegate callback)
        {
            Exports.Add(eventName, callback);
        }

        public void AddGlobal(string key, object value)
        { 
            GlobalState.Set(key, value, true);
        }

        public object GetGlobal(string key)
        { 
            return GlobalState.Get(key);
        }
        public dynamic GetResourceExport(string name)
        {
            return Exports[name];
        }

        public void AddTick(Func<Task> x)
        {
            Tick += x;
        }

        public void RemoveTick(Func<Task> x)
        {
            Tick += x;
        }

        public void AddEventListener(string eventName, Delegate delegat)
        {
            EventHandlers.Add(eventName, delegat);
        }

        public void RemoveEventListener(string eventName)
        { 
            EventHandlers.Remove(eventName);
        }

        public async Task InternalOnTick()
        { 
            try
            {
                var types = _sourcAssembly.GetTypes().First(e => e.Name.Equals("Resource"));
                types.GetMethod("Main").Invoke(null, new object[] { (int)ResourceType.CLIENT });
            }
            catch (Exception e)
            {
                _log.Error(e.ToString(), true);
                throw;
            }
            finally
            {
                Tick -= InternalOnTick;
            }
        }

    }
}
