using Proline.CScripting.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.CScripting
{
    internal class ScriptWrapper 
    {
        internal static List<StartScriptRequest> _scriptRequest;

        private string _assembly;
        private string _class;
        private List<ScriptInstance> _instances;
        private string _name;
        private object[] _addionalArgs;
        private int _status;
        private bool _isDebug;
        private int _envType;
        private int _handles;
        public string Type { get; }
        public string Name => _name;

        private ScriptWrapper(string name, string assembly, string classType, ScriptConfig details, int envType = 0) 
        {
            //_name = details.Name;
            _assembly = assembly;
            _class = classType;
            _instances = new List<ScriptInstance>();
            _name = name;
            _addionalArgs = details.AddionalArgs;
            _isDebug = details.DebugOnly;
            _envType = envType;
            Type = "LevelScript";
        }

        internal static ScriptWrapper CreateNewScript(string name, Assembly assembly, Type type, ScriptConfig details, int envType = 0)
        { 
            return new ScriptWrapper(name, assembly.FullName, type.FullName, details, envType); 
        }

        internal ScriptInstance InitializeNewInstance(object[] args)
        {
            //if (_isDebug && !EngineConfiguration.IsDebugEnabled) throw new Exception("Cannot intialize a new instance, engine not in debug");
            if (_status != 0) throw new Exception("Cannot inialize script, script not reset");
            var assembly = Assembly.Load(_assembly);
            Type type = assembly.GetType(_class);
            ScriptInstance script = (ScriptInstance)Activator.CreateInstance(type);
            script.Parameters = args;
            //_script = ScriptFactory.CreateScriptInstance(_name, _addionalArgs);
            //InternalManager.RegisterScript(this);
            _status = 1;

            //var em = InternalManager.GetInstance();
            //var extensions = em.GetExtensions();
            //foreach (var extension in extensions)
            //{
            //    extension.OnScriptInitialized(_name);
            //}
            return script;
            // Notify extensions here that the script has intialized
        }

        private int ExecuteInstance(ScriptInstance levelScript, object[] args)
        {
            //if(_status != 1) throw new Exception("Script not ready to be started");
            var task = Run(levelScript, args);
            return _handles;
            // Notify extensions here that the script has started
        }

        internal async Task Run(ScriptInstance script, params object[] args)
        {
            try
            {
                //script.BeginRunInstance(this);
                await script.Execute(args);
                //script.EndRunInstance(this);
            }
            catch (Exception e)
            {
                //LogError(e);
            }
        }

        internal void BeginRunInstance(ScriptInstance scriptInstance)
        {
            _instances.Add(scriptInstance);
            _handles++;
        }

        internal void EndRunInstance(ScriptInstance scriptInstance)
        {
            var em = InternalManager.GetInstance();
            var extensions = em.GetExtensions();
            _instances.Remove(scriptInstance);
            _handles++;
            foreach (var extension in extensions)
            {
                extension.OnScriptStarted(_name);
            }
        }

        internal int StartNewInstance(object[] args)
        {
            if (!EngineConfiguration.IsClient && _envType == 1) return -1;
            try
            { 
                _status = 0;
                //KillAllInstances();
                if (args == null)
                    args = new object[0];

                var ls = InitializeNewInstance(args);
                var arg = new List<object>(args);
                if (_addionalArgs != null)
                    arg.AddRange(_addionalArgs);
                return ExecuteInstance(ls, arg.ToArray());
            }
            catch (Exception e)
            {
                LogDebug(e.ToString());
                throw;
            }
        }

        internal void KillAllInstances()
        {
            _instances.Clear();
        }

        internal static void RegisterScript(ScriptWrapper script)
        {
            var sm = InternalManager.GetInstance();
            sm.AddScript(script);
        }

        internal static void UnregisterScript(ScriptWrapper scriptName)
        {
            var sm = InternalManager.GetInstance();
            sm.RemoveScript(scriptName);
        }

        internal static int StartScript(StartScriptRequest startScriptRequest)
        {
            var em = InternalManager.GetInstance();
            var extensions = em.GetExtensions();
            var wrapper = em.GetScript(startScriptRequest.ScriptName);
            if (wrapper == null) return -1;
            return wrapper.StartNewInstance(null);
        }

        internal static int[] StartScripts(StartScriptRequest[] startScriptRequests)
        {
            if (startScriptRequests == null) return new int[0];
            var ids = new int[startScriptRequests.Length];
            for (int i = 0; i < startScriptRequests.Length; i++)
            {
                ids[i] = StartScript(startScriptRequests[i]);
            }
            return ids;
        }
    }
}
