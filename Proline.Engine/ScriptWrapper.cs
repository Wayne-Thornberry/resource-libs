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
    internal class ScriptWrapper
    {
        private Task _task;
        private LevelScript _script;

        private string _assembly;
        private string _class;

        private int _instanceCount;
        private string _name;
        private object[] _addionalArgs;
        private int _status;
        private bool _isDebug; 

        internal ScriptWrapper(string assembly, string classType, ScriptConfig details)
        {
            //_name = details.Name;
            _assembly = assembly;
            _class = classType;

            _addionalArgs = details.AddionalArgs;
            _isDebug = details.DebugOnly;
        }

        internal LevelScript InitializeNewInstance(object[] args)
        {
            if (_isDebug && !EngineConfiguration.IsDebugEnabled) throw new Exception("Cannot intialize a new instance, engine not in debug");
            if (_status != 0) throw new Exception("Cannot inialize script, script not reset");
            var assembly = Assembly.Load(_assembly);
            Type type = assembly.GetType(_class);
            LevelScript script = (LevelScript)Activator.CreateInstance(type);
            script.Parameters = args;

            //_script = ScriptFactory.CreateScriptInstance(_name, _addionalArgs);
            //ScriptManager.RegisterScript(this);
            _status = 1;

            var em = ExtensionManager.GetInstance();
            var extensions = em.GetExtensions();
            foreach (var extension in extensions)
            {
                extension.OnScriptInitialized(_name);
            }
            return script;
            // Notify extensions here that the script has intialized
        }

        private void ExecuteInstance(LevelScript levelScript, object[] args)
        {
            //if(_status != 1) throw new Exception("Script not ready to be started");
            _task = Task.Run(() => Execute(levelScript, args));
            _task.ContinueWith(x => Finish());
            var em = ExtensionManager.GetInstance();
            var sm = ScriptTaskManager.GetInstance();
            sm.RegisterScriptTask(_task, _name);  
            var extensions = em.GetExtensions();
            foreach (var extension in extensions)
            {
                extension.OnScriptStarted(_name);
            }
            // Notify extensions here that the script has started
        }

        internal void StartNew(object[] args)
        {
            _status = 0;
            KillAllInstances();
            var ls = InitializeNewInstance(args);
            var arg = new List<object>(args);
            if (_addionalArgs != null)
                arg.AddRange(_addionalArgs);
            ExecuteInstance(ls, arg.ToArray());
            _instanceCount++;
        }

        internal void KillAllInstances()
        {
            //if (_status != 2) throw new Exception("Script not ready to be started");
            if (_task != null)
            {
                _task.Dispose();
                _script = null;
            }
        }
         

        private async Task Finish()
        {
            _task.Dispose();
            var em = ExtensionManager.GetInstance();
            var sm = ScriptTaskManager.GetInstance();
            sm.UnRegisterScriptTask(_task);
            var extensions = em.GetExtensions();
            foreach (var extension in extensions)
            {
                extension.OnScriptFinished(_name);
            }
        }

        internal async Task Execute(LevelScript script, object[] args)
        {
            try
            {
                if (EngineConfiguration.IsIsolated) return;
                await script.Execute(args);
            }
            catch (Exception e)
            {
                Debugger.LogError(e, true);
                throw;
            }
        } 
    }
}
