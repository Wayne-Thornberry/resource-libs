using Newtonsoft.Json;

using Proline.Engine.Data;
using Proline.Engine.Framework;
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

    public class EngineComponent
    { 
        private string _name;
        private int _status;

        internal bool HasAPI(string apiName)
        {
            return _apiActions.ContainsKey(apiName);
        }

        internal object CallAPI(string apiName, object[] inputParameters)
        { 
            if (_status != 2) throw new Exception("Component not started");
            if (!_apiActions.ContainsKey(apiName)) return null;
           return _apiActions[apiName].Invoke(_simpleComponent, inputParameters);
        }

        private ComponentDetails _details;
        private ComponentController _controller;
        private ComponentHandler _handler;
        private ComponentAPI _api;
        private ComponentCommands _commands;
        private List<ComponentScript> _scripts;

        private Dictionary<string, MethodInfo> _apiActions;
        private Dictionary<string, MethodInfo> _controlActions;
        private Dictionary<string, MethodInfo> _commandActions;
        private long _ticks; 
        private SimpleComponent _simpleComponent;

        public string Name => _name;
        public ComponentStatus Status => (ComponentStatus)_status;

        public EngineComponent(ComponentDetails componentName)
        {
            _details = componentName;
            _controlActions = new Dictionary<string, MethodInfo>();
            _commandActions = new Dictionary<string, MethodInfo>();
            _apiActions = new Dictionary<string, MethodInfo>();
            _scripts = new List<ComponentScript>();
            _status = 0; // Created not started
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
            if (EngineConfiguration.IsEngineConsoleApp()) return;
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
                if (!EngineConfiguration.IsEngineConsoleApp())
                    _handler.OnComponentStart();
            }
            if (_simpleComponent != null)
            {
                if(!EngineConfiguration.IsEngineConsoleApp())
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
            var assembly = Assembly.Load(_details.Assembly); 
            _name = _details.ComponentName;

            if (!string.IsNullOrEmpty(_details.SimpleComponentClass))
            {
                var controllerType = assembly.GetType(_details.SimpleComponentClass);
                _simpleComponent = CreateComponentObject<SimpleComponent>(controllerType);
                var methods = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ControllerControlAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods)
                {
                    Debugger.LogDebug(item.Name);
                    _controlActions.Add(item.Name, item);
                }
                var methods2 = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods2)
                {
                    Debugger.LogDebug(item.Name);
                    _commandActions.Add(item.Name, item);
                }
                var methods3 = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods3)
                {
                    Debugger.LogDebug(item.Name);
                    _apiActions.Add(item.Name, item);
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(_details.APIClass))
                {
                    var api = assembly.GetType(_details.APIClass);
                    if (api != null)
                    {
                        _api = CreateComponentObject<ComponentAPI>(api);
                    }
                }

                if (!string.IsNullOrEmpty(_details.CommandClass))
                {
                    var commandType = assembly.GetType(_details.CommandClass);
                    if (commandType != null)
                    {
                        _commands = CreateComponentObject<ComponentCommands>(commandType);
                        var commandTypes = _commands.GetType();
                        var methods = commandType.GetMethods()
                          .Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
                          .ToArray();
                        foreach (var item in methods)
                        {
                            Debugger.LogDebug(item);
                            _commandActions.Add(item.Name, item);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(_details.HandlerClass))
                {
                    var handlerType = assembly.GetType(_details.HandlerClass);
                    if (handlerType != null)
                    {
                        _handler = CreateComponentObject<ComponentHandler>(handlerType);
                        _handler.OnComponentInitialized();
                    }
                }

                if (!string.IsNullOrEmpty(_details.ControllerClass))
                {
                    var controllerType = assembly.GetType(_details.ControllerClass);
                    if (controllerType != null)
                    {
                        _controller = CreateComponentObject<ComponentController>(controllerType);
                        var methods = controllerType.GetMethods()
                          .Where(m => m.GetCustomAttributes(typeof(ControllerControlAttribute), false).Length > 0)
                          .ToArray();
                        foreach (var item in methods)
                        {
                            Debugger.LogDebug(item);
                            _controlActions.Add(item.Name, item);
                        }
                    }
                }

                if (_details.ScriptClasses != null)
                {
                    foreach (var item in _details.ScriptClasses)
                    {
                        var controllerType = assembly.GetType(item);
                        var script = (ComponentScript)CreateComponentObject<ComponentScript>(controllerType);
                        _scripts.Add(script);
                    }
                }
            }



            _status = 1; // Ready Not Started
        }

        private T CreateComponentObject<T>(Type commandType)
        {
            return (T)Activator.CreateInstance(commandType, null);
            //_handler.OnComponentInitialized();
        }
    }
}
