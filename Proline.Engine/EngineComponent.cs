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
        private int _status;
        private bool _isDebug;
        private string _assembly; 
        private long _ticks;

        private List<ComponentScript> _scripts;
        private List<ComponentAPI> _apis; 
        private List<ComponentCommand> _commandActions;
        private AbstractComponent _component;

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
             
            _commandActions = new List<ComponentCommand>();
            _apis = new List<ComponentAPI>();
            _scripts = new List<ComponentScript>();
            _status = 0; // Created not started

            Type = "Component";
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
            if (_status != 0) throw new Exception("Component has already loaded");
            Debugger.LogDebug("Attempting to load " + _assembly);
            var assembly = Assembly.Load(_assembly); 
            var am = APIManager.GetInstance();

            if (!string.IsNullOrEmpty(_componentClass))
            {
                var controllerType = assembly.GetType(_componentClass);
                _component = CreateComponentObject<AbstractComponent>(controllerType); 
                var methods2 = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods2)
                {
                   // Debugger.LogDebug(item.Name);
                    _commandActions.Add(new ComponentCommand(_component, item));
                }
                var methods3 = controllerType.GetMethods()
                  .Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                  .ToArray();
                foreach (var item in methods3)
                {
                    //Debugger.LogDebug(item.Name);
                    _apis.Add(new ComponentAPI(_component, item));
                    //am.RegisterAPI(_simpleComponent, item, item.Name);
                }
            }
            //else
            //{

            //    if (!string.IsNullOrEmpty(_apiClass))
            //    {
            //        var api = assembly.GetType(_apiClass);
            //        if (api != null)
            //        {
            //            _api = CreateComponentObject<ComponentAPI>(api);
            //            var methods3 = api.GetMethods()
            //              .Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
            //              .ToArray();
            //            foreach (var item in methods3)
            //            {
            //                Debugger.LogDebug(item.Name);
            //                _apis.Add(new ComponentAP(_api, item)); 
            //                //am.RegisterAPI(_api, item, item.Name);
            //            }
            //        } 
            //    }

            //    if (!string.IsNullOrEmpty(_commandClass))
            //    {
            //        var commandType = assembly.GetType(_commandClass);
            //        if (commandType != null)
            //        {
            //            _commands = CreateComponentObject<ComponentCommands>(commandType);
            //            var commandTypes = _commands.GetType();
            //            var methods = commandType.GetMethods()
            //              .Where(m => m.GetCustomAttributes(typeof(ComponentCommandAttribute), false).Length > 0)
            //              .ToArray();
            //            foreach (var item in methods)
            //            {
            //                //Debugger.LogDebug(item);
            //                _commandActions.Add(new ComponentCommand(_commands, item));
            //            }
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(_handlerClass))
            //    {
            //        var handlerType = assembly.GetType(_handlerClass);
            //        if (handlerType != null)
            //        {
            //            _handler = CreateComponentObject<ComponentHandler>(handlerType);
            //            _handler.OnComponentInitialized();
            //        }
            //    } 
            //}


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

            _status = 1; // Ready Not Started
        }

        internal IEnumerable<string> GetAPINames()
        {
            return _apis.Select(e=>e.Name);
        }

        internal IEnumerable<ComponentAPI> GetAPIs()
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
