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
    public enum ComponentStatus
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
        private string _simpleComponentClass;
        private int _status;
        private bool _isDebug;
        private string _assembly;
        private string _apiClass;
        private string _handlerClass;
        private string _controllerClass;
        private string _commandClass;

        private ComponentController _controller;
        private ComponentHandler _handler;
        private ComponentAPI _api;
        private ComponentCommands _commands;
        private List<ComponentScript> _scripts;

        private List<APIKeyPair> _apis;
        private Dictionary<string, MethodInfo> _controlActions;
        private Dictionary<string, MethodInfo> _commandActions;
        private long _ticks; 
        private SimpleComponent _simpleComponent;

        public string Name => _name;
        public ComponentStatus Status => (ComponentStatus)_status;

        public EngineComponent(ComponentDetails details)
        {
            _isDebug = details.DebugOnly;
            _assembly = details.Assembly;
            _apiClass = details.APIClass;
            _handlerClass = details.HandlerClass; 
            _controllerClass = details.ControllerClass; 
            _commandClass = details.CommandClass;
            _name = details.ComponentName;
            _scriptClasses = details.ScriptClasses;
            _simpleComponentClass = details.SimpleComponentClass;

            _controlActions = new Dictionary<string, MethodInfo>();
            _commandActions = new Dictionary<string, MethodInfo>();
            _apis = new List<APIKeyPair>();
            _scripts = new List<ComponentScript>();
            _status = 0; // Created not started
        } 
         

        internal APIKeyPair GetAPI(string apiName)
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
            if (_status != 2) throw new Exception("Component not started");
            if (!_commandActions.ContainsKey(command)) return;
            _commandActions[command].Invoke(_commands, args);
        }

        public object ExecuteControl(string methodName, object[] args)
        {
            if (_status != 2) throw new Exception("Component not started");
            if (_controlActions == null || _controlActions.Count == 0 || !_controlActions.ContainsKey(methodName)) return null;
            var method = _controlActions[methodName]; 
            var parameters = method.GetParameters();
            var paras = new object[args.Length];
            var log = new Log();
            for (int i = 0; i < paras.Length; i++)
            {
                var methodType = parameters[i].ParameterType;
                Debugger.LogDebug(methodType.IsPrimitive);
                Debugger.LogDebug(args[i].ToString());
                try
                { 
                    paras[i] = Convert.ChangeType(args[i], methodType);
                }
                catch (Exception)
                { 
                    paras[i] = JsonConvert.DeserializeObject(args[i].ToString(), methodType);
                    throw;
                } 
            }
            if(_simpleComponent != null)
            {
                return method.Invoke(_simpleComponent, paras); 
            }
            else
            { 
                return method.Invoke(_controller, paras);
            }
        }

        internal IEnumerable<string> GetCommands()
        {
            return _commandActions.Keys;
        }

        public async Task Tick()
        {
            if (EngineConfiguration.IsIsolated) return;
            if (_status == 2)
            {
                    foreach (var item in _scripts)
                {
                    if(item.Status == 0)
                    {
                        item.Start(); 
                        item.Status = 1;
                    }

                    if(item.Status == 1)
                    { 
                        item.Update();
                        if(_ticks % 100 == 0)
                        {
                            item.FixedUpdate();
                        }
                    }
                }
                _ticks++;
            } 
        }

        internal ComponentAPI GetAPI()
        {
            return _api;
        }

        public void Start()
        { 
            if (_status != 1 && _status != 3) 
                throw new Exception("Component cannot be started, component not ready or stopped");
            if (_handler != null)
            {
                if (!EngineConfiguration.IsIsolated)
                    _handler.OnComponentStart();
            }
            if (_simpleComponent != null)
            {
                if(!EngineConfiguration.IsIsolated)
                    _simpleComponent.OnComponentStart();
            }
            _status = 2; // Started;
        }


        public void Stop()
        {
            if (_status != 2) throw new Exception("Component cannot be stopped, component not started");
            if (_handler != null)
                _handler.OnComponentStop();
            _status = 3; // Stopped;
        }

        public IEnumerable<MethodInfo> GetCommandMethod()
        {
            return _commandActions.Values;
        }

        public void Load()
        {
            if (_status != 0) throw new Exception("Component has already loaded");
            var assembly = Assembly.Load(_assembly); 
            var am = APIManager.GetInstance();

            if (!string.IsNullOrEmpty(_simpleComponentClass))
            {
                var controllerType = assembly.GetType(_simpleComponentClass);
                _simpleComponent = CreateComponentObject<SimpleComponent>(controllerType);
                var methods = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ControllerControlAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods)
                {
                   // Debugger.LogDebug(item.Name);
                    _controlActions.Add(item.Name, item);
                }
                var methods2 = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods2)
                {
                   // Debugger.LogDebug(item.Name);
                    _commandActions.Add(item.Name, item);
                }
                var methods3 = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods3)
                {
                    //Debugger.LogDebug(item.Name);
                    _apis.Add(new APIKeyPair(_simpleComponent, item));
                    //am.RegisterAPI(_simpleComponent, item, item.Name);
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(_apiClass))
                {
                    var api = assembly.GetType(_apiClass);
                    if (api != null)
                    {
                        _api = CreateComponentObject<ComponentAPI>(api);
                        var methods3 = api.GetMethods()
                          .Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                          .ToArray();
                        foreach (var item in methods3)
                        {
                            Debugger.LogDebug(item.Name);
                            _apis.Add(new APIKeyPair(_api, item)); 
                            //am.RegisterAPI(_api, item, item.Name);
                        }
                    } 
                }

                if (!string.IsNullOrEmpty(_commandClass))
                {
                    var commandType = assembly.GetType(_commandClass);
                    if (commandType != null)
                    {
                        _commands = CreateComponentObject<ComponentCommands>(commandType);
                        var commandTypes = _commands.GetType();
                        var methods = commandType.GetMethods()
                          .Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
                          .ToArray();
                        foreach (var item in methods)
                        {
                            //Debugger.LogDebug(item);
                            _commandActions.Add(item.Name, item);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(_handlerClass))
                {
                    var handlerType = assembly.GetType(_handlerClass);
                    if (handlerType != null)
                    {
                        _handler = CreateComponentObject<ComponentHandler>(handlerType);
                        _handler.OnComponentInitialized();
                    }
                }

                if (!string.IsNullOrEmpty(_controllerClass))
                {
                    var controllerType = assembly.GetType(_controllerClass);
                    if (controllerType != null)
                    {
                        _controller = CreateComponentObject<ComponentController>(controllerType);
                        var methods = controllerType.GetMethods()
                          .Where(m => m.GetCustomAttributes(typeof(ControllerControlAttribute), false).Length > 0)
                          .ToArray();
                        foreach (var item in methods)
                        {
                            //Debugger.LogDebug(item);
                            _controlActions.Add(item.Name, item);
                        }
                    }
                }

                if (_scriptClasses != null)
                {
                    foreach (var item in _scriptClasses)
                    {
                        var controllerType = assembly.GetType(item);
                        var script = (ComponentScript)CreateComponentObject<ComponentScript>(controllerType);
                        _scripts.Add(script);
                    }
                }
            }


            _status = 1; // Ready Not Started
        }

        internal IEnumerable<string> GetAPIs()
        {
            return _apis.Select(e=>e.Name);
        }

        private T CreateComponentObject<T>(Type commandType)
        {
            return (T)Activator.CreateInstance(commandType, null);
            //_handler.OnComponentInitialized();
        }
    }
}
