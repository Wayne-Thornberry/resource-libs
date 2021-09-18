
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
    internal class ScriptTaskManager
    {
        private static ScriptTaskManager _instance;
        private Dictionary<Task, string> _scriptTasks;

        private ScriptTaskManager()
        { 
            _scriptTasks = new Dictionary<Task, string>();
        }

        internal static ScriptTaskManager GetInstance()
        { 
            if (_instance == null)
                _instance = new ScriptTaskManager();
            return _instance;
        }  

        internal void RegisterScriptTask(Task task, string scriptName)
        {
            _scriptTasks.Add(task, scriptName);
        }

        internal void UnregisterScriptTask(Task task)
        {
            _scriptTasks.Remove(task);
        }

        internal IEnumerable<Task> GetScriptTasks(string name)
        {
            return _scriptTasks
   .Where(pair => pair.Value.Equals(name))
   .Select(pair => pair.Key)
   .ToArray();
        }
    }
}
