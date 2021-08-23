using Newtonsoft.Json;

using Proline.Engine.Data;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal enum ComponentStatus
    {
        CREATED,
        READY,
        STARTED,
        STOPPED,
    }

    internal class EngineComponent
    { 
        private string _name;
        private string[] _scriptClasses;
        private string _componentClass;
        private string _apiClass;
        private string _commanderClass;
        private string _handlerClass;
        private int _status;
        private bool _isDebug;
        private string _assembly; 
        private long _ticks;

        private List<ComponentScript> _scripts;
        private List<APIInvoker> _apis;


        private List<ComponentCommand> _commandActions; 
        private ComponentAPI _api; 
        private ComponentHandler _handler;
        private ComponentCommander _commander;
        private long _lastCheck;

        public string Name => _name;
        public ComponentStatus Status => (ComponentStatus)_status;

        public string Type { get; internal set; }

        public EngineComponent(ComponentDetails details)
        {
            _isDebug = details.DebugOnly;
            _assembly = details.Assembly; 
            _name = details.ComponentName; 

            _apiClass = details.APIClass;
            _commanderClass = details.CommanderClass;
            _handlerClass = details.HandlerClass;
            _scriptClasses = details.ScriptClasses;

            _commandActions = new List<ComponentCommand>();
            _apis = new List<APIInvoker>();
            _scripts = new List<ComponentScript>();
            _status = 0; // Created not started

            Type = "Component";
        } 
         

        internal APIInvoker GetAPI(string apiName)
        {
            foreach (var item in _apis)
            {
                if (item.Name.Equals(apiName))
                    return item;
            }
            return null;
        }

        public void ExecuteCommand(string command, object[] args)
        {
            var cm = InternalManager.GetInstance();
            cm.GetCommand(command).Invoke(args); 
        }
         

        internal IEnumerable<ComponentCommand> GetCommands()
        {
            return _commandActions;
        }

        internal void TriggerComponentEvent(string eventName, object[] args)
        {
            if (_handler != null)
                _handler.OnComponentEvent(eventName, args);
        }

        internal void OnEngineEvent(string eventName, object[] args)
        {
            foreach (var item in _scripts)
            {
                item.OnEngineEvent(eventName, args);
            }
        }

        internal async Task Tick()
        {
            Update();
            if (DateTime.UtcNow.Ticks - _lastCheck > 1000000)
            {
                FixedUpdate(); 
                _lastCheck = DateTime.UtcNow.Ticks;
            }
        }

        internal void FixedUpdate()
        {
            foreach (var item in _scripts)
            {
                item.FixedUpdate();
            }
        }

        internal IEnumerable<ComponentScript> GetScripts()
        {
            return _scripts;
        }

        internal void Update()
        {
            if (EngineConfiguration.IsIsolated) return;
            if (_status == 2)
            {
                foreach (var item in _scripts)
                { 
                    if (item.Status == 1)
                    {
                        item.Update();
                    }
                }
            } 
        }

        internal void Start()
        { 
            if (_status != 1 && _status != 3) 
                throw new Exception("Component cannot be started, component not ready or stopped"); 
            if (_handler != null)
            {
                if(!EngineConfiguration.IsIsolated)
                    _handler.OnComponentStart();
            }

            foreach (var item in _scripts)
            { 
                if (item.Status == 0)
                {
                    item.Start();
                    item.Status = 1;
                }
            }
            _status = 2; // Started;
        }


        internal void Stop()
        {
            if (_status != 2) throw new Exception("Component cannot be stopped, component not started");
            //if (_handler != null)
            //    _handler.OnComponentStop();
            if (_handler != null)
            {
                if (!EngineConfiguration.IsIsolated)
                    _handler.OnComponentStop();
            }
            _status = 3; // Stopped;
        }

        internal void Load()
        {
            try
            {
                if (_status != 0) throw new Exception("Component has already loaded");
                Debugger.LogDebug("Attempting to load " + _name + " From assembly " + _assembly);
                var assembly = Assembly.Load(_assembly);
                var am = InternalManager.GetInstance();
               
                var lookFor = EngineConfiguration.IsClient ? typeof(ClientAttribute) : typeof(ServerAttribute);

                Type handler = null;
                Type commander = null;
                Type api =null;
                if (!string.IsNullOrEmpty(_handlerClass))
                    handler = assembly.GetType(_handlerClass);
                if (!string.IsNullOrEmpty(_commanderClass))
                    commander = assembly.GetType(_commanderClass);
                if (!string.IsNullOrEmpty(_apiClass))
                    api = assembly.GetType(_apiClass);
               

                if (handler != null)
                    _handler = CreateComponentObject<ComponentHandler>(handler);
                if (commander != null)
                    _commander = CreateComponentObject<ComponentCommander>(commander);
                if (api != null)
                    _api = CreateComponentObject<ComponentAPI>(api);

               
                if (_handler != null)
                {
                    Debugger.LogDebug($"[{_name}] Attempting to load the handler");
                    _handler.OnComponentInitialized();
                }

               

                if (_api != null)
                {
                    Debugger.LogDebug($"[{_name}] Attempting to load the API");
                    var methods = api.GetMethods();//.Where(m => m.GetCustomAttributes(lookFor, false).Length > 0); 
                    var filer = methods.Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                     .ToArray();
                   
                    foreach (var item in filer)
                    {
                        var type = IsAPIServer(item) ? -1 : 0;
                        var debugOnly = IsAPIDebug(item);
                        _apis.Add(new APIInvoker(_api, item, type, debugOnly));
                    }
                }

               

                if (_scriptClasses != null)
                {
                    Debugger.LogDebug($"[{_name}] Attempting to load the Scripts");
                    foreach (var item in _scriptClasses)
                    {
                        var scriptType = assembly.GetType(item);
                        if (scriptType == null) continue;
                        var script = CreateComponentObject<ComponentScript>(scriptType);
                        _scripts.Add(script);
                    }
                }

                if (_handler != null)
                {
                    _handler.OnComponentLoad();
                }

                _status = 1; // Ready Not Started
            }
            catch (Exception e)
            {
                Debugger.LogDebug(e, true);
                throw;
            }
          
        }

        private bool IsAPIDebug(MethodInfo item)
        {
            foreach (var attribute in item.GetCustomAttributes())
            {
                if (attribute.GetType() == typeof(DebugAttribute))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsAPIServer(MethodInfo item)
        {
            foreach (var attribute in item.GetCustomAttributes())
            {
                if (attribute.GetType() == typeof(ServerAttribute))
                {
                    return true;
                }
            }
            return false;
        }

        internal IEnumerable<string> GetAPINames()
        {
            return _apis.Select(e=>e.Name);
        }

        internal IEnumerable<APIInvoker> GetAPIs()
        {
            return _apis;
        }

        private T CreateComponentObject<T>(Type commandType)
        {
            return (T)Activator.CreateInstance(commandType, null);
            //_handler.OnComponentInitialized();
        }



        internal static void RegisterComponent(EngineComponent component)
        {
            var im = InternalManager.GetInstance();
            if (im.IsComponentRegistered(component.Name)) return;
            foreach (ComponentCommand command in component.GetCommands())
            {
                ComponentCommand.RegisterCommand(command);
            }

            foreach (APIInvoker apiName in component.GetAPIs())
            {
                APIInvoker.RegisterAPI(apiName);
            }
            Debugger.LogDebug("Registered " + component.Type + " " + component.Name);
            im.AddComponent(component);
        }

        internal static void UnregisterComponent(EngineComponent component)
        {
            var im = InternalManager.GetInstance();
            if (!im.IsComponentRegistered(component.Name)) return;
            foreach (ComponentCommand command in component.GetCommands())
            {
                ComponentCommand.UnregisterCommand(command);
            }

            foreach (APIInvoker apiName in component.GetAPIs())
            {
                APIInvoker.UnregisterAPI(apiName);
            }
            im.RemoveComponent(component);
        }
    }
}
