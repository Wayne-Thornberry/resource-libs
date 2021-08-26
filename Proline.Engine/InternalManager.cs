using Proline.Engine.Data;
using Proline.Engine.Networking;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class InternalManager : EngineObject
    {
        private static InternalManager _instance;

        private Dictionary<int, ComponentAPI> _apis;
        private Dictionary<string, ComponentCommand> _commands;
        private Dictionary<string, EngineComponent> _components;
        private Dictionary<string, NetworkRequest> _requests;
        private Dictionary<string, NetworkResponse> _responses;
        private Dictionary<string, Script> _scripts;
        //private Dictionary<Task, string> _tasks;

        private List<EngineExtension> _extensions;
        private List<ScriptPackage> _packages;
        private List<StartScriptRequest> _scriptRequest;

        public InternalManager() : base("InternalManager")
        {
            _apis = new Dictionary<int, ComponentAPI>();
            _commands = new Dictionary<string, ComponentCommand>();
            _components = new Dictionary<string, EngineComponent>();
            _requests = new Dictionary<string, NetworkRequest>();
            _responses = new Dictionary<string, NetworkResponse>();
            _scripts = new Dictionary<string, Script>();
            //_tasks = new Dictionary<Task, string>();
            _extensions = new List<EngineExtension>();
            _packages = new List<ScriptPackage>();
            _scriptRequest = new List<StartScriptRequest>();
        }


        internal static InternalManager GetInstance()
        {
            if (_instance == null)
                _instance = new InternalManager();
            return _instance;
        }

        internal ComponentAPI GetAPI(int apiName)
        {
            if (_apis.ContainsKey(apiName))
                return _apis[apiName];
            return null;
        }

        internal IEnumerable<EngineComponent> GetComponents()
        {
            return _components.Values;
        }
        internal IEnumerable<ScriptPackage> GetPackages()
        {
            return _packages;
        }


        internal EngineComponent GetComponent(string componentName)
        {
            return _components[componentName];
        }

        internal bool IsComponentRegistered(string componentName)
        {
            return _components.ContainsKey(componentName);
        }

        internal IEnumerable<EngineExtension> GetExtensions()
        {
            return _extensions;
        }

   //     internal IEnumerable<Task> GetScriptTasks(string name)
   //     {
   //         return _tasks
   //.Where(pair => pair.Value.Equals(name))
   //.Select(pair => pair.Key)
   //.ToArray();
   //     }


        internal int GetScriptCount()
        {
            return _scripts.Count;
        }

        internal Script GetScript(string scriptName)
        {
            if (_scripts.ContainsKey(scriptName))
                return _scripts[scriptName];
            else
                return null;
        }

        internal IEnumerable<ComponentCommand> GetCommands()
        {
            return _commands.Values;
        }

        internal IEnumerable<ComponentAPI> GetAPIs()
        {
            return _apis.Values;
        }

        internal ComponentCommand GetCommand(string command)
        {
            return _commands[command];
        }

        internal void RemoveCommand(ComponentCommand command)
        {
            _commands.Remove(command.Name);
        }

        internal void RemovePackage(ScriptPackage sp)
        {
            _packages.Remove(sp);
        }

        internal void RemoveAPI(ComponentAPI api)
        {
            _apis.Remove(api.GetHashCode());
        }
        internal void RemoveScript(Script scriptName)
        {
            _scripts.Remove(scriptName.Name);
        }
        internal void RemoveComponent(EngineComponent component)
        {
            _components.Remove(component.Name);
        }

        internal void AddStartScriptRequest(StartScriptRequest startScriptRequest)
        {
            if (_scriptRequest.Count >= 30) return;
            _scriptRequest.Add(startScriptRequest);
        }

        internal StartScriptRequest[] GetStartScriptRequest()
        {
            return _scriptRequest.ToArray();
        }

        internal void RemoveStartScriptRequest(StartScriptRequest startScriptRequest)
        { 
            _scriptRequest.Remove(startScriptRequest);
        }

        internal void AddCommand(ComponentCommand command)
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

        internal void AddScript(Script script)
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

        internal void AddAPI(ComponentAPI api)
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
