using Newtonsoft.Json;

using Proline.Engine.Data;
using Proline.Framework;
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
        private AbstractComponent _component;



        private ComponentAPI _api;
        private ComponentHandler _handler;
        private ComponentCommander _commander;

        public string Name => _name;
        public ComponentStatus Status => (ComponentStatus)_status;

        public string Type { get; internal set; }

        public EngineComponent(ComponentDetails details)
        {
            _isDebug = details.DebugOnly;
            _assembly = details.Assembly; 
            _name = details.ComponentName;
            _scriptClasses = details.ScriptClasses;
            _componentClass = details.ComponentClass;
            _componentClass = "d";

            _apiClass = details.APIClass;
            _commanderClass = details.CommanderClass;
            _handlerClass = details.HandlerClass;

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
            //if (_status != 2) throw new Exception("Component not started");
            //if (!_commandActions.ContainsKey(command)) return;
            //_commandActions[command].Invoke(_commands, args);
            var cm = CommandManager.GetInstance();
            cm.GetCommand(command).Invoke(args); 
        }

        // OLD IMPLEMENTATION, THIS IS REPLACED BY COMPONENTAPI FUNCTIONALITY
        //public object ExecuteControl(string methodName, object[] args)
        //{
        //    if (_status != 2) throw new Exception("Component not started");
        //    if (_controlActions == null || _controlActions.Count == 0 || !_controlActions.ContainsKey(methodName)) return null;
        //    var method = _controlActions[methodName]; 
        //    var parameters = method.GetParameters();
        //    var paras = new object[args.Length]; 
        //    for (int i = 0; i < paras.Length; i++)
        //    {
        //        var methodType = parameters[i].ParameterType;
        //        Debugger.LogDebug(methodType.IsPrimitive);
        //        Debugger.LogDebug(args[i].ToString());
        //        try
        //        { 
        //            paras[i] = Convert.ChangeType(args[i], methodType);
        //        }
        //        catch (Exception)
        //        { 
        //            paras[i] = JsonConvert.DeserializeObject(args[i].ToString(), methodType);
        //            throw;
        //        } 
        //    }
        //    if(_simpleComponent != null)
        //    {
        //        return method.Invoke(_simpleComponent, paras); 
        //    }
        //    else
        //    { 
        //        return method.Invoke(_controller, paras);
        //    }
        //}
         

        internal IEnumerable<ComponentCommand> GetCommands()
        {
            return _commandActions;
        } 

        public void FixedUpdate()
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

        public void Update()
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

        public void Start()
        { 
            if (_status != 1 && _status != 3) 
                throw new Exception("Component cannot be started, component not ready or stopped"); 
            if (_component != null)
            {
                if(!EngineConfiguration.IsIsolated)
                    _component.OnComponentStart();
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


        public void Stop()
        {
            if (_status != 2) throw new Exception("Component cannot be stopped, component not started");
            //if (_handler != null)
            //    _handler.OnComponentStop();
            if (_component != null)
            {
                if (!EngineConfiguration.IsIsolated)
                    _component.OnComponentStop();
            }
            _status = 3; // Stopped;
        } 

        public void Load()
        {
            try
            {
                if (_status != 0) throw new Exception("Component has already loaded");
                Debugger.LogDebug("Attempting to load " + _assembly);
                var assembly = Assembly.Load(_assembly);
                var am = APIManager.GetInstance();
               
                var lookFor = EngineConfiguration.IsClient ? typeof(ClientAttribute) : typeof(ServerAttribute);

                var controllerType = assembly.GetType(_componentClass);
                var handler = assembly.GetType(_handlerClass);
                var commander = assembly.GetType(_commanderClass);
                var api = assembly.GetType(_apiClass);
               

                if (handler != null)
                    _handler = CreateComponentObject<ComponentHandler>(handler);
                if (commander != null)
                    _commander = CreateComponentObject<ComponentCommander>(commander);
                if (api != null)
                    _api = CreateComponentObject<ComponentAPI>(api);
                if (controllerType != null)
                    _component = CreateComponentObject<AbstractComponent>(controllerType);

               
                if (_component != null)
                {
                    var methods = controllerType.GetMethods().Where(m => m.GetCustomAttributes(lookFor, false).Length > 0);
                    var filer = methods.Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
                     .ToArray();
                    foreach (var item in filer)
                    {
                        _commandActions.Add(new ComponentCommand(_component, item));
                    }


                    filer = methods.Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                     .ToArray();
                    foreach (var item in filer)
                    {
                        _apis.Add(new APIInvoker(_component, item));
                    }
                }


               
                if (_handler != null)
                {
                    _handler.OnComponentInitialized();
                }

               

                if (_api != null)
                {
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
    }
}
