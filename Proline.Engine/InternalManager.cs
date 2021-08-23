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
    public class InternalManager
    {
        private static InternalManager _instance;

        private Dictionary<int, APIInvoker> _apis;
        private Dictionary<string, ComponentCommand> _commands;
        private Dictionary<string, EngineComponent> _components;
        private Dictionary<string, NetworkRequest> _requests;
        private Dictionary<string, NetworkResponse> _responses;
        private Dictionary<string, EngineScript> _scripts;
        //private Dictionary<Task, string> _tasks;

        private List<EngineExtension> _extensions;
        private List<ScriptPackage> _packages;

        public InternalManager()
        {
            _apis = new Dictionary<int, APIInvoker>();
            _commands = new Dictionary<string, ComponentCommand>();
            _components = new Dictionary<string, EngineComponent>();
            _requests = new Dictionary<string, NetworkRequest>();
            _responses = new Dictionary<string, NetworkResponse>();
            _scripts = new Dictionary<string, EngineScript>();
            //_tasks = new Dictionary<Task, string>();
            _extensions = new List<EngineExtension>();
            _packages = new List<ScriptPackage>();
        }


        internal static InternalManager GetInstance()
        {
            if (_instance == null)
                _instance = new InternalManager();
            return _instance;
        }

        internal APIInvoker GetAPI(int apiName)
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

        internal EngineScript GetScript(string scriptName)
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

        internal IEnumerable<APIInvoker> GetAPIs()
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

        internal void RemoveAPI(APIInvoker api)
        {
            _apis.Remove(api.GetHashCode());
        }
        internal void RemoveScript(EngineScript scriptName)
        {
            _scripts.Remove(scriptName.Name);
        }
        internal void RemoveComponent(EngineComponent component)
        {
            _components.Remove(component.Name);
        }

        internal void AddCommand(ComponentCommand command)
        {
            _commands.Add(command.Name, command);
        }

        internal void AddPackage(ScriptPackage sp)
        {
            _packages.Add(sp);
        }


        internal void AddScript(EngineScript script)
        {
            _scripts.Add(script.Name, script);
        }

        internal void AddAPI(APIInvoker api)
        {
            _apis.Add(api.GetHashCode(), api);
        }


        internal void AddExtension(EngineExtension extension)
        {
            _extensions.Add(extension);
        }

        internal void AddComponent(EngineComponent component)
        {
            _components.Add(component.Name, component);
        }

        //internal void RegisterScriptTask(Task task, string scriptName)
        //{
        //    _tasks.Add(task, scriptName);
        //}

        //internal void UnregisterScriptTask(Task task)
        //{
        //    _tasks.Remove(task);
        //}

    }
}
