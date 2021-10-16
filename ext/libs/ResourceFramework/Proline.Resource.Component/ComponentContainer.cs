using Proline.Common.Logging; 
using Proline.Resource.Component.Framework;
using Proline.Resource.Common.CFX;
using System.Threading.Tasks;
using Proline.Resource.Common.Component;
using Newtonsoft.Json;
using Proline.Resource.Common.Script;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace Proline.Resource.Component
{
    public class ComponentContainer
    {
        private static ComponentContainer _instance; 
        internal static ComponentContainer GetInstance()
        {
            return _instance;
        }

        public ComponentEnviromentType ComponentEnv { get; } 
        public ComponentType ComponentType { get; }
        public ComponentFlag ComponentFlags { get; }
         
        private Log _log => Logger.GetInstance().GetLog();
        private ComponentEnviroment _current;
        private List<ComponentEnviroment> _clients;
        private ComponentEnviroment _server;
        private IScriptSource _source;
        private object _result;

        public ComponentEnviroment Current => _current;
        public ComponentEnviroment Server => _server;
        public List<ComponentEnviroment> Clients => _clients;

        public ComponentContainer(IScriptSource source)
        {
            _source = source;
            _clients = new List<ComponentEnviroment>();
            _instance = this;
        }

        public ComponentEnviroment LoadEnviroment(IComponentConfigEnv config, ComponentEnviromentType env)
        {  
            if(env == ComponentEnviromentType.CLIENT)
            { 

                var client = Load(config);
                client.Load();
                client.Initialize();
                foreach (var api in client.GetAPIs())
                {
                    _log.LogDebug(api.GetAPIMethodHash().ToString());
                }
                _clients.Add(client);

                // Heartbeat to the server, once it gets something back, setup the server env

                _current = client;
                client.Start();
                return client;
            }
            else if(env == ComponentEnviromentType.SERVER)
            {
                _server = Load(config);
                _server.Load();
                _server.Initialize();
                foreach (var api in _server.GetAPIs())
                {
                    _log.LogDebug(api.GetAPIMethodHash().ToString());
                }
                _server.Start();

                // Wait for heartbeats from the clients and add them

                _current = _server;
                return _server;
            }
            return null; 
        }
        public ComponentConfig LoadConfig()
        {
            string json = ReadComponentConfig();
            return JsonConvert.DeserializeObject<ComponentConfig>(json);
        }
        private string ReadComponentConfig()
        {
            return _source.Resource.LoadFile("component.json");
        }

        public ComponentEnviroment Load(IComponentConfigEnv config)
        {
            var assembly = Assembly.Load(config.Assembly);
            var type = assembly.GetType(config.Handler);
            var handler = (IComponentHandler)Activator.CreateInstance(type);
            var compoent = new ComponentEnviroment(this, handler);
            Type type1 = handler.GetType();

            var propetyInfos = type1
                .GetProperties()
                .Where(m => m.GetCustomAttributes(typeof(ComponentPropertyAttribute), false).Length > 0)
                      .ToArray();

            foreach (var item in propetyInfos)
            {
                compoent.SyncProperty(item);
            }

            var methodInfos = type1.GetMethods();
            foreach (var item in methodInfos
                      .Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                      .ToArray())
            {
                var api = new ComponentAPI(handler, item);
                compoent.RegisterAPI(api);
            }
            return compoent;
        }

        public async Task<object> CallServerAPI(long v1, string v2)
        {
            _source.EventTrigger.TriggerSidedEvent("networkComponentCallAPI", v1, v2);
            int ticks = 0;
            while (ticks < 1000)
            {
                if (_result != null)
                {
                    var result = _result;
                    _result = null;
                    return result;
                }
                ticks++;
                await _source.Task.Wait(0);
            }
            return default;
        }

        public async Task<object> CallComponentAPI(long v1, string v2)
        {
            _source.EventTrigger.TriggerEvent("componentCallAPI", v1, v2);
            int ticks = 0;
            while (ticks < 1000)
            {
                if (_result != null)
                {
                    var result = _result;
                    _result = null;
                    return result;
                }
                ticks++;
                await _source.Task.Wait(0);
            }
            return default;
        }

        public async Task<object> CallAPI(long v1, string v2)
        {
            int ticks = 0;
            while (ticks < 1000)
            {
                if (_result != null)
                {
                    var result = _result;
                    _result = null;
                    return result;
                }
                ticks++;
                await _source.Task.Wait(0);
            }
            return default;
        }
    }
}
