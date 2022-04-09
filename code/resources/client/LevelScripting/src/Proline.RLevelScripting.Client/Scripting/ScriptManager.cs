
using Proline.CScripting.Framework;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Client.Engine.CScripting
{

    public class ScriptManager
    {
        private long _nextId;
        private ScriptLibrary _library;
        private static ScriptManager _instance;
        private Dictionary<long, Task> _scriptTasks;
        private Dictionary<long, ScriptInstance> _instances;

        public static ScriptManager GetInstance()
        {
            return _instance;
        }

        public ScriptManager(ScriptLibrary library)
        {
            _scriptTasks = new Dictionary<long, Task>();
            _instances = new Dictionary<long, ScriptInstance>();
            _library = library;
            _instance = this;
        }

        private async Task StartInstance(ScriptInstance instance, params object[] args)
        { 
            var task = ExecuteScriptExecute(instance, args);
            _nextId++;
            _scriptTasks.Add(_nextId, task);
            _instances.Add(_nextId, instance);
            instance.Id = _nextId;
            //task.Start();
        }

        private async Task ExecuteScriptExecute(ScriptInstance instance, params object[] args)
        {
            var log = Logger.GetInstance().GetLog();
            await instance.Execute(args);
            _scriptTasks.Remove(instance.Id);
            _instances.Remove(instance.Id);
            log.Debug(instance.Name + " Finished");
        }

        private void StopInstance(ScriptInstance instance)
        {
            if (_instances == null)
                _instances = new Dictionary<long, ScriptInstance>();
            var task = _scriptTasks[instance.Id]; 
            /// terminate the task somwhow???

            _scriptTasks.Remove(instance.Id);
            _instances.Remove(instance.Id);
        }

        /// <summary>
        /// Starts exeactly 1 instance of a script matching the passed script name. Will look in all assemblys that are targeted to have scripts in them. 
        /// </summary>
        /// <param name="scriptName"></param>
        public void StartScript(string scriptName, params object[] args)
        {
            var sl = new ScriptLoader(_library);
            var si = sl.LoadScript(scriptName); 
            StartInstance(si, args); 
        }

        /// <summary>
        /// Terminates all instances of a script matching the passed script name. Will look locally for any instance and assosicated task to termiante
        /// </summary>
        /// <param name="scriptName"></param>
        public void TerminateScript(string scriptName)
        {
            var instances = _instances.Select(e => e.Value).Where(e=>e.Name.Equals(scriptName));
            foreach (var item in instances)
            {
                TerminateScript(item.Id);
            } 
        }

        /// <summary>
        /// Terminates exactly 1 instance of a script matching the id of the script instance. Will look locally for any instance running matching that id
        /// </summary>
        /// <param name="id"></param>
        public void TerminateScript(long id)
        {
            var instance = _instances[id];
            StopInstance(instance); 
        }
    }
}
