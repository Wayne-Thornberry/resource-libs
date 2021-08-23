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
    internal class EngineScript
    {  
        private string _assembly;
        private string _class;

        private int _instanceCount;
        private string _name;
        private object[] _addionalArgs;
        private int _status;
        private bool _isDebug;
        private int _envType;
        private int _handles;

        public string Type { get; }

        public string Name => _name;

        private EngineScript(string name, string assembly, string classType, ScriptConfig details, int envType = 0)
        {
            //_name = details.Name;
            _assembly = assembly;
            _class = classType;

            _name = name;
            _addionalArgs = details.AddionalArgs;
            _isDebug = details.DebugOnly;
            _envType = envType;
            Type = "EngineScript";
        }

        internal static EngineScript CreateNewScript(string name, Assembly assembly, Type type, ScriptConfig details, int envType = 0)
        { 
            return new EngineScript(name, assembly.FullName, type.FullName, details, envType); 
        }

        internal ScriptInstance InitializeNewInstance(object[] args)
        {
            if (_isDebug && !EngineConfiguration.IsDebugEnabled) throw new Exception("Cannot intialize a new instance, engine not in debug");
            if (_status != 0) throw new Exception("Cannot inialize script, script not reset");
            var assembly = Assembly.Load(_assembly);
            Type type = assembly.GetType(_class);
            ScriptInstance script = (ScriptInstance)Activator.CreateInstance(type);
            script.Parameters = args;

            //_script = ScriptFactory.CreateScriptInstance(_name, _addionalArgs);
            //InternalManager.RegisterScript(this);
            _status = 1;

            var em = InternalManager.GetInstance();
            var extensions = em.GetExtensions();
            foreach (var extension in extensions)
            {
                extension.OnScriptInitialized(_name);
            }
            return script;
            // Notify extensions here that the script has intialized
        }

        private int ExecuteInstance(ScriptInstance levelScript, object[] args)
        {
            //if(_status != 1) throw new Exception("Script not ready to be started");
            var task = new Task(async () => await Execute(levelScript, args));
            _instanceCount++;
            _handles++;
            task.ContinueWith(x => Finish(task));
            var em = InternalManager.GetInstance();
            var sm = InternalManager.GetInstance();
            //sm.RegisterScriptTask(task, _name);
            task.Start();
            var extensions = em.GetExtensions();
            foreach (var extension in extensions)
            {
                extension.OnScriptStarted(_name);
            }
            return _handles;
            // Notify extensions here that the script has started
        }

        internal int StartNew(object[] args)
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
                Debugger.LogDebug(e.ToString(), true);
                throw;
            }
        }

        internal void KillAllInstances()
        {
            var stm = InternalManager.GetInstance();
            //var tasks = stm.GetScriptTasks(_name);
            //foreach (var item in tasks)
            //{
            //    Finish(item);
            //}
        }
         

        private async Task Finish(Task task)
        {
            var em = InternalManager.GetInstance();
            var sm = InternalManager.GetInstance();
            task.Dispose();
            //sm.UnregisterScriptTask(task);
            var extensions = em.GetExtensions();
            foreach (var extension in extensions)
            {
                extension.OnScriptFinished(_name);
            }
        }

        internal async Task Execute(ScriptInstance script, object[] args)
        {
            try
            {
                if (EngineConfiguration.IsIsolated) return;
                await script.Execute(args);
            }
            catch (Exception e)
            {
                Debugger.LogError(e, true); 
            }
        }

        internal static void RegisterScript(EngineScript script)
        {
            var sm = InternalManager.GetInstance();
            Debugger.LogDebug("Registered " + script.Type + " " + script.Name);
            sm.AddScript(script);
        }

        internal static void UnregisterScript(EngineScript scriptName)
        {
            var sm = InternalManager.GetInstance();
            sm.RemoveScript(scriptName);
        }
    }
}
