using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proline.Engine.AAPI;
using Proline.Engine.Data;
using Proline.Engine.Debugging;
using Proline.Engine.Eventing;
using Proline.Engine.Internals;
using Proline.Engine.Networking;
using Proline.Engine.Scripting;

namespace Proline.Engine.Componentry
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
        protected bool IsClient => EngineConfiguration.IsClient;

        private List<APIMethod> _apis;
        private List<ConsoleCommand> _commands;
        private Dictionary<string, MethodInfo> _events;
        private Queue<KeyValuePair<string, object[]>> _eventQueue;

        private int _hostId;
        private long _lastCheck;
        protected internal List<PropertyInfo> SyncedProperties { get; internal set; }
        protected internal bool HasChanged { get; internal set; }
        protected internal bool IsOutOfSync { get; internal set; }

        internal int Td { get; set; }
        internal string Name => _name;
        public ComponentStatus Status => (ComponentStatus)_status;
         
        public EngineComponent() : base("Component")
        {
            _commands = new List<ConsoleCommand>();
            _apis = new List<APIMethod>();
            SyncedProperties = new List<PropertyInfo>();
            _events = new Dictionary<string, MethodInfo>();
            _eventQueue = new Queue<KeyValuePair<string, object[]>>();
            _status = 0; // Created not started 
        }
        protected void StartNewScript(string scriptName, params object[] args)
        {
            var im = InternalManager.GetInstance();
            im.EnqueueStartScriptRequest(new StartScriptRequest(scriptName, args));
        }

        internal APIMethod GetAPI(string apiName)
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
         

        internal IEnumerable<ConsoleCommand> GetCommands()
        {
            return _commands;
        }

        protected void TriggerComponentEvent(string eventName, params object[] args)
        {
            var im = InternalManager.GetInstance();
            if (args == null) args = new object[0];
            im.EnqueueComponentEvent(eventName, args);
            //if (_handler != null)
            //   ((ComponentHandler) _handler).OnComponentEvent(eventName, args);
        }

        internal void EnqueueEvent(KeyValuePair<string, object[]> keyValuePair)
        {
            _eventQueue.Enqueue(keyValuePair);
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
               // await Sync();
                while (_eventQueue.Count > 0)
                {
                    var item = _eventQueue.Dequeue();
                    if (!_events.ContainsKey(item.Key)) continue;
                    _events[item.Key].Invoke(this, item.Value);
                }

                if (_status == 2)
                {
                    OnStart();
                    _status = 3;
                }

                if (_status == 3)
                {
                    OnUpdate();
                    if (DateTime.UtcNow.Ticks - _lastCheck > 1000000)
                    {
                        OnFixedUpdate();
                        _lastCheck = DateTime.UtcNow.Ticks;
                    }
                }

                if (_status == 4)
                {
                    OnStop();
                    _status = 5;
                }

                var obj = new object[SyncedProperties.Count];
                for (int i = 0; i < obj.Length; i++)
                {
                    obj[i] = SyncedProperties[i].GetValue(this);
                }
            }
        }


        internal async Task Sync()
        {
            await Push();
            await Pull();
        }

        internal void MarkAsOutOfSync()
        {
            IsOutOfSync = true;
        }

        internal void InvokeComponentEvent(KeyValuePair<string, object[]> events)
        {
            //LogDebug(events.Key);
            if (!_events.ContainsKey(events.Key)) return;
            _events[events.Key].Invoke(this, events.Value);
        }

        internal void Initalize()
        {
            OnInitialize();
            LogDebug($"Initializing Component " + Name);
            OnInitialized();
        } 

        public void Start()
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


        public void Stop()
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
         
        internal static EngineComponent Load(ComponentDetails details)
        {
            if (EngineConfiguration.IsClient && details.EnvType == 1)
            { 
                var isDebug = details.DebugOnly;
                var assemblyClass = details.Assembly;
                var name = details.ComponentName;

                var componentClass = details.ComponentClass;

                var assembly = Assembly.Load(assemblyClass);

                if (!string.IsNullOrEmpty(componentClass))
                {
                    var componentType = assembly.GetType(componentClass);
                    if (componentType != null)
                    {
                        var component = CreateComponentObject<EngineComponent>(componentType);
                        component.Td = details.EnvType;
                        component.Load(name, isDebug);
                        return component;
                    }
                }
                throw new Exception("Failed to load component " + name + " " + componentClass);
            }
            else
            {
                return null;
            }

        }
        protected abstract Task Push(); 
        protected abstract Task Pull();
        private void Load(string name, bool debug)
        {
            try
            {
                _isDebug = debug;
                _name = name;
                LogDebug($"Attempting to load component {Name}");
                var methods = this.GetType().GetMethods();//.Where(m => m.GetCustomAttributes(lookFor, false).Length > 0); 
                var filer = methods.Where(m => m.GetCustomAttributes(typeof(APIMethodAttribute), false).Length > 0)
                 .ToArray();

                foreach (var item in filer)
                {
                    var type = 0;//IsServerMethod(item) ? -1 : 0;
                    var debugOnly = IsDebugMethod(item);
                    _apis.Add(new APIMethod(this, item, type, debugOnly));
                }

                filer = methods.Where(m => m.GetCustomAttributes(typeof(ConsoleCommandAttribute), false).Length > 0)
                 .ToArray();

                foreach (var item in filer)
                {
                    var type = 0;// IsServerMethod(item) ? -1 : 0;
                    var debugOnly = IsDebugMethod(item);
                    _commands.Add(new ConsoleCommand(this, item, type, debugOnly));
                }


                var filer2 = this.GetType().GetProperties().Where(m => m.GetCustomAttributes(typeof(SyncedPropertyAttribute), false).Length > 0)
                 .ToArray();

                foreach (var item in filer2)
                {
                    //var type = IsServerMethod(item) ? -1 : 0;
                    //var debugOnly = IsDebugMethod(item);
                    SyncedProperties.Add(item);
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
                LogDebug(string.Format("Component {0} loaded sucessfully, {1} APIs loaded, {2} Commands Loaded {3} Synced Properties, {4} Events ", Name, _apis.Count(), _commands.Count(), SyncedProperties.Count(), _events.Count()));
                _status = 1; // Ready Not Started
            }
            catch (Exception e)
            {
                LogDebug(e);
                throw;
            } 
        }

        public void PushData(string data)
        {
            if (IsOutOfSync)
            { 
                var objs = JsonConvert.DeserializeObject<object[]>(data);
                for (int i = 0; i < objs.Length; i++)
                {
                    var type = SyncedProperties[i].GetType();
                    object value = null;
                    if (type.IsClass)
                    {
                        value = JsonConvert.DeserializeObject(SyncedProperties[i].ToString(), type);
                    }
                    else
                    {
                        value = objs[i];
                    }
                    SyncedProperties[i].SetValue(this, value);
                }
            }
        }

        public object PullData()
        {
            var obj = new object[SyncedProperties.Count];
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i] = SyncedProperties[i].GetValue(this);
            }
            return JsonConvert.SerializeObject(obj);
        }

        private static bool IsDebugMethod(MethodInfo item)
        {
            return true;
            //foreach (var attribute in item.GetCustomAttributes())
            //{
            //    if (attribute.GetType() == typeof(DebugAttribute))
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

        //private static bool IsServerMethod(MethodInfo item)
        //{
        //    foreach (var attribute in item.GetCustomAttributes())
        //    {
        //        if (attribute.GetType() == typeof(ServerAttribute))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        internal IEnumerable<string> GetAPINames()
        {
            return _apis.Select(e=>e.Name);
        }

        internal IEnumerable<APIMethod> GetAPIs()
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

        internal void SetHost(int hostId)
        {
            _hostId = hostId;
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
            if (im.IsComponentRegistered(component.Name)) return;
            foreach (ConsoleCommand command in component.GetCommands())
            {
                ConsoleCommand.RegisterCommand(command);
            }

            foreach (APIMethod apiName in component.GetAPIs())
            {
                APIMethod.RegisterAPI(apiName);
            }
           // ca.AddTick(component.Tick);
            im.AddComponent(component);
        }

        internal static void UnregisterComponent(EngineComponent component)
        {
            var im = InternalManager.GetInstance(); 
            if (!im.IsComponentRegistered(component.Name)) return;
            foreach (ConsoleCommand command in component.GetCommands())
            {
                ConsoleCommand.UnregisterCommand(command);
            }

            foreach (APIMethod apiName in component.GetAPIs())
            {
                APIMethod.UnregisterAPI(apiName);
            }
         //   ca.RemoveTick(component.Tick);
            im.RemoveComponent(component);
        }
    }
}
