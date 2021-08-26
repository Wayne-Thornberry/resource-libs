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
    public enum ComponentStatus
    {
        CREATED,
        LOADED,
        INITALIZED,
        STARTED,
        RUNNING,
        STOPPED,
    }

    public abstract class EngineComponent : EngineObject
    { 
        private string _name;  
        private int _status;
        private bool _isDebug;
        private string _assembly;  

        private List<ComponentAPI> _apis;
        private List<ComponentCommand> _commands;
        private Dictionary<string, MethodInfo> _events;
        private List<PropertyInfo> _syncedProperties;

        private long _lastCheck;

        internal string Name => _name;
        public ComponentStatus Status => (ComponentStatus)_status;
         
        public EngineComponent() : base("Component")
        {
            _commands = new List<ComponentCommand>();
            _apis = new List<ComponentAPI>();
            _syncedProperties = new List<PropertyInfo>();
            _events = new Dictionary<string, MethodInfo>();
            _status = 0; // Created not started 
        }
        protected void StartNewScript(string scriptName, params object[] args)
        {
            var im = InternalManager.GetInstance();
            im.EnqueueStartScriptRequest(new StartScriptRequest(scriptName, args));
        }

        internal ComponentAPI GetAPI(string apiName)
        {
            foreach (var item in _apis)
            {
                if (item.Name.Equals(apiName))
                    return item;
            }
            return null;
        }

        public void InvokeCommand(string command, object[] args)
        {
            var cm = InternalManager.GetInstance();
            cm.GetCommand(command).Invoke(args); 
        }
         

        internal IEnumerable<ComponentCommand> GetCommands()
        {
            return _commands;
        }

        protected void TriggerComponentEvent(string eventName, params object[] args)
        {
            var im = InternalManager.GetInstance();
            im.EnqueueComponentEvent(eventName, args);
            //if (_handler != null)
            //   ((ComponentHandler) _handler).OnComponentEvent(eventName, args);
        }

        internal void OnEngineEvent(string eventName, object[] args)
        {
            //foreach (var item in _scripts)
            //{
            //    item.OnEngineEvent(eventName, args);
            //}
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnInitialized() { }
        protected virtual void OnLoad() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnFixedUpdate() { }
        protected virtual void OnStart() { }
        protected virtual void OnStop() { }


        internal async Task Tick()
        {
            if (_status != 0 && _status != 5)
            { 
                if (_status == 2)
                {
                    OnStart();
                    _status = 3;
                }

                if(_status == 3)
                { 
                    OnUpdate();
                    if (DateTime.UtcNow.Ticks - _lastCheck > 1000000)
                    {
                        OnFixedUpdate();
                        _lastCheck = DateTime.UtcNow.Ticks;
                    }
                }

                if(_status == 4)
                {
                    OnStop();
                    _status = 5;
                }
            }
        }

        internal void InvokeComponentEvent(KeyValuePair<string, object[]> events)
        {
            if (!_events.ContainsKey(events.Key)) return;
            _events[events.Key].Invoke(this, events.Value);
        }

        internal void Initalize()
        {
            OnInitialize();
            LogDebug($"Initializing Component " +Name);
            OnInitialized();
        } 

        internal void Start()
        { 
            if (_status == 1 || _status == 5)
            { 
                _status = 2; // Started; 
                var em = InternalManager.GetInstance();
                var extensions = em.GetExtensions();
                foreach (var extension in extensions)
                {
                    extension.OnComponentStarted(Name);
                }
            }
            else
            { 
                throw new Exception("Component cannot be started, component not ready or stopped");
            }
        }


        internal void Stop()
        {
            if (_status == 3)
            {
                _status = 4; // Stopped
                var em = InternalManager.GetInstance();
                var extensions = em.GetExtensions();
                foreach (var extension in extensions)
                {
                    extension.OnComponentStopped(Name);
                }
            }
            else
            {
                throw new Exception("Component cannot be stopped, component not started");
            } 
        }

        protected void Push()
        {
            // Pushes the components data onto the server, if the data is out of sync, is pushed onto the clients with new data once the server has been set
        }

        protected void Pull()
        {
            // Pulls the servers component synced data onto its own
        } 

        internal static EngineComponent Load(ComponentDetails details)
        { 
            var isDebug = details.DebugOnly;
            var assemblyClass = details.Assembly;
            var name = details.ComponentName;

            var componentClass = details.ComponentClass;

            var assembly = Assembly.Load(assemblyClass);

            var lookFor = EngineConfiguration.IsClient ? typeof(ClientAttribute) : typeof(ServerAttribute);

            if (!string.IsNullOrEmpty(componentClass))
            {
                var componentType = assembly.GetType(componentClass);
                if (componentType != null)
                {
                    var component = CreateComponentObject<EngineComponent>(componentType);
                    component.Load(name, isDebug);
                    return component;
                }
            }
            throw new Exception("Failed to load component " + name + " " + componentClass );
        }

        private void Load(string name, bool debug)
        {
            try
            {
                _isDebug = debug;
                _name = name;
                LogDebug($"Attempting to load component {Name}");
                var methods = this.GetType().GetMethods();//.Where(m => m.GetCustomAttributes(lookFor, false).Length > 0); 
                var filer = methods.Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                 .ToArray();

                foreach (var item in filer)
                {
                    var type = IsServerMethod(item) ? -1 : 0;
                    var debugOnly = IsDebugMethod(item);
                    _apis.Add(new ComponentAPI(this, item, type, debugOnly));
                }

                filer = methods.Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
                 .ToArray();

                foreach (var item in filer)
                {
                    var type = IsServerMethod(item) ? -1 : 0;
                    var debugOnly = IsDebugMethod(item);
                    _commands.Add(new ComponentCommand(this, item, type, debugOnly));
                }


                var filer2 = this.GetType().GetProperties().Where(m => m.GetCustomAttributes(typeof(SyncedPropertyAttribute), false).Length > 0)
                 .ToArray();

                foreach (var item in filer2)
                {
                    //var type = IsServerMethod(item) ? -1 : 0;
                    //var debugOnly = IsDebugMethod(item);
                    _syncedProperties.Add(item);
                }

                 filer = this.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(ComponentEventAttribute), false).Length > 0)
                 .ToArray();

                foreach (var item in filer)
                {
                    //var type = IsServerMethod(item) ? -1 : 0;
                    //var debugOnly = IsDebugMethod(item);
                    _events.Add(item.Name, item);
                }

                OnLoad();
                LogDebug(string.Format("Component {0} loaded sucessfully, {1} APIs loaded, {2} Commands Loaded {3} Synced Properties, {4} Events ", Name, _apis.Count(), _commands.Count(), _syncedProperties.Count(), _events.Count()));
                _status = 1; // Ready Not Started
            }
            catch (Exception e)
            {
                LogDebug(e);
                throw;
            } 
        } 

        private static bool IsDebugMethod(MethodInfo item)
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

        private static bool IsServerMethod(MethodInfo item)
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

        internal IEnumerable<ComponentAPI> GetAPIs()
        {
            return _apis;
        }

        private static T CreateComponentObject<T>(Type commandType)
        {
            return (T)Activator.CreateInstance(commandType, null);
            //_handler.OnComponentInitialized();
        }


        internal static void StartAllComponents()
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot start components, engine not initilized");
            var cm = InternalManager.GetInstance(); 
            foreach (var component in cm.GetComponents())
            {
                component.Start();
            }

        }

        internal static void StopAllComponents()
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot stop components, engine not initilized");
            var cm = InternalManager.GetInstance();
            foreach (var component in cm.GetComponents())
            {
                component.Stop();
            }
        }


        internal static void StartComponent(EngineComponent component)
        {
            try
            {
                component.Start(); 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static void StopComponent(EngineComponent component)
        {
            try
            {
                component.Stop(); 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static void RegisterComponent(EngineComponent component)
        {
            var im = InternalManager.GetInstance();
            var ca = EngineService.GetInstance();
            if (im.IsComponentRegistered(component.Name)) return;
            foreach (ComponentCommand command in component.GetCommands())
            {
                ComponentCommand.RegisterCommand(command);
            }

            foreach (ComponentAPI apiName in component.GetAPIs())
            {
                ComponentAPI.RegisterAPI(apiName);
            }
            ca.AddTick(component.Tick);
            im.AddComponent(component);
        }

        internal static void UnregisterComponent(EngineComponent component)
        {
            var im = InternalManager.GetInstance();
            var ca = EngineService.GetInstance();
            if (!im.IsComponentRegistered(component.Name)) return;
            foreach (ComponentCommand command in component.GetCommands())
            {
                ComponentCommand.UnregisterCommand(command);
            }

            foreach (ComponentAPI apiName in component.GetAPIs())
            {
                ComponentAPI.UnregisterAPI(apiName);
            }
            ca.RemoveTick(component.Tick);
            im.RemoveComponent(component);
        }
    }
}
