using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Reflection; 
using Proline.Resource.Networking;
using Proline.Resource.Logging;

namespace Proline.Resource.Framework
{
    public abstract class ComponentContext : BaseScript, IBaseScriptMethods
    {
        private ResourceEnvironment _env;
        private ResourceApplication _app;
        private EventSocket _socket;
        private ResourceType _type;
        protected Log _log;
        private Assembly _sourcAssembly;

        public virtual void ConfigureServices(IResourceEnvironment env) { }
        public virtual void Configure(IResourceApplication app) { }

        public ComponentContext()
        {
            _sourcAssembly = Assembly.GetCallingAssembly();

            _env = new ResourceEnvironment();
            _app = new ResourceApplication();
            _socket = new EventSocket(API.GetCurrentResourceName());
            _type = ResourceType.SERVER;
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


        private void OnEventResponse([FromSource] Player arg1, string arg2)
        {
            _log.Debug("Got Request");
            var request = JsonConvert.DeserializeObject<EventRequest>(arg2);
            var response = new EventResponse();
            _log.Debug(request.CallbackGUID);
            arg1.TriggerEvent(request.CallbackGUID, JsonConvert.SerializeObject(response));
        }

        private async Task InternalOnTick()
        {

            try
            {
                EventHandlers.Add(_socket.EndPoint, _socket.Action);
                _socket.SetCallback(new Action<Player, string>(OnEventResponse));

                ConfigureServices(_env);
                Configure(_app);
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                throw;
            }
            finally
            {
                Tick -= InternalOnTick;
            }
        }

    }
}
