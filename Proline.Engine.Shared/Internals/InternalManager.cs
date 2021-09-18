using System;
using System.Collections.Generic;
using Proline.Engine.AAPI;
using Proline.Engine.Componentry;
using Proline.Engine.Debugging;
using Proline.Engine.Extension;
using Proline.Engine.Scripting;

namespace Proline.Engine.Internals
{
    public class InternalManager : EngineObject
    {
        private static InternalManager _instance;

        private Dictionary<int, APIMethod> _apis;
        private Dictionary<string, ConsoleCommand> _commands;
        private Dictionary<string, EngineComponent> _components; 
        private Dictionary<string, InternalScript> _scripts;
        //private Dictionary<Task, string> _tasks;

        private Queue<KeyValuePair<string, object[]>> _eventQueue;
        private Queue<StartScriptRequest> _scriptRequest;

        private List<EngineExtension> _extensions;
        private List<ScriptPackage> _packages;

        internal InternalManager() : base("InternalManager")
        {
            _apis = new Dictionary<int, APIMethod>();
            _commands = new Dictionary<string, ConsoleCommand>();
            _components = new Dictionary<string, EngineComponent>(); 
            _scripts = new Dictionary<string, InternalScript>();

            _extensions = new List<EngineExtension>();
            _packages = new List<ScriptPackage>();

            _scriptRequest = new Queue<StartScriptRequest>();
            _eventQueue = new Queue<KeyValuePair<string, object[]>>();
        }


        public static InternalManager GetInstance()
        {
            if (_instance == null)
                _instance = new InternalManager();
            return _instance;
        }

        internal APIMethod GetAPI(int apiName)
        {
            //LogDebug("Getting API " + apiName);
            if (_apis.ContainsKey(apiName))
                return _apis[apiName];
            //LogDebug("API does not exist " + apiName);
            return null;
        }

        internal void EnqueueComponentEvent(string eventName, object[] args)
        {
            foreach (var item in _components.Values)
            {
                item.EnqueueEvent(new KeyValuePair<string, object[]>(eventName, args));
            } 
        }

        internal KeyValuePair<string,object[]> DequeueComponentEvent()
        {
            return _eventQueue.Dequeue();
        }

        public IEnumerable<EngineComponent> GetComponents()
        {
            return _components.Values;
        }
        internal IEnumerable<ScriptPackage> GetPackages()
        {
            return _packages;
        }

        internal bool IsScriptRequestsQueueEmpty()
        {
            return _scriptRequest.Count == 0;
        }

        public EngineComponent GetComponent(string componentName)
        {
            return _components[componentName];
        }

        internal bool IsComponentRegistered(string componentName)
        {
            return _components.ContainsKey(componentName);
        }

        internal bool IsComponentEventQueueEmpty()
        {
            return _eventQueue.Count == 0;
        }

        internal IEnumerable<EngineExtension> GetExtensions()
        {
            return _extensions;
        } 


        internal int GetScriptCount()
        {
            return _scripts.Count;
        }

        internal InternalScript GetScript(string scriptName)
        {
            if (_scripts.ContainsKey(scriptName))
                return _scripts[scriptName];
            else
                return null;
        }

        internal IEnumerable<ConsoleCommand> GetCommands()
        {
            return _commands.Values;
        }

        internal IEnumerable<APIMethod> GetAPIs()
        {
            return _apis.Values;
        }

        internal ConsoleCommand GetCommand(string command)
        {
            return _commands[command];
        }

        internal void RemoveCommand(ConsoleCommand command)
        {
            _commands.Remove(command.Name);
        }

        internal void RemovePackage(ScriptPackage sp)
        {
            _packages.Remove(sp);
        }

        internal void RemoveAPI(APIMethod api)
        {
            _apis.Remove(api.GetHashCode());
        }
        internal void RemoveScript(InternalScript scriptName)
        {
            _scripts.Remove(scriptName.Name);
        }
        internal void RemoveComponent(EngineComponent component)
        {
            _components.Remove(component.Name);
        }

        internal void EnqueueStartScriptRequest(StartScriptRequest startScriptRequest)
        {
            if (_scriptRequest.Count >= 30) return;
            _scriptRequest.Enqueue(startScriptRequest);
        }


        internal StartScriptRequest DequeueStartScriptRequest()
        { 
            return _scriptRequest.Dequeue();
        }

        internal void AddCommand(ConsoleCommand command)
        {
            LogDebug("Registered " + command.Type + " " + command.Name);
            _commands.Add(command.Name, command);
        }

        internal void AddPackage(ScriptPackage sp)
        {
            try
            {
                LogDebug("Registered " + sp.ToString());
                _packages.Add(sp);
            }
            catch (ArgumentException e)
            {

            }
        }

        internal void AddScript(InternalScript script)
        {
            try
            {
                LogDebug("Registered " + script.Type + " " + script.Name);
                _scripts.Add(script.Name, script);
            }
            catch (ArgumentException e)
            {

            }
        }

        internal void AddAPI(APIMethod api)
        {
            try
            {
                LogDebug("Registered " + api.ToString());
                _apis.Add(api.GetHashCode(), api);
            }
            catch (ArgumentException e)
            {

            }
        }


        internal void AddExtension(EngineExtension extension)
        {
            LogDebug("Registered " + extension.Type + " ");
            _extensions.Add(extension);
        }

        internal void AddComponent(EngineComponent component)
        {
            LogDebug("Registered " + component.Type + " " + component.Name);
            _components.Add(component.Name, component);
        }
    }
}
