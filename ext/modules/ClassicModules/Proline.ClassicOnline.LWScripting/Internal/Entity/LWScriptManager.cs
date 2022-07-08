using CitizenFX.Core;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting
{
    internal class LWScriptManager
    {
        private Log _log => new Log();
        private List<ScriptContainer> _liveScripts;

        private Dictionary<Task, CancellationTokenSource> _scriptTokens;
        private Dictionary<string, List<Task>> _scriptTasks;
        private Dictionary<string, Type> _scriptTypes;

        internal IEnumerable<Task> GetFinishedScriptTasks()
        {
            return _scriptTokens.Keys.Where(e => e.IsCompleted);
        }

        internal IEnumerable<Task> GetScriptTasks(string scriptName)
        {
            return _scriptTasks[scriptName];
        }

        internal void TerminateTask(string scriptName, Task task)
        {
            Console.WriteLine(String.Format("{0} Scripts removed", scriptName));
            if (_scriptTokens.ContainsKey(task))
                _scriptTokens.Remove(task);
            _scriptTasks[scriptName].Remove(task);
        }

        private static LWScriptManager _instance;

        public LWScriptManager()
        {
            _scriptTypes = new Dictionary<string, Type>();
            _scriptTasks = new Dictionary<string, List<Task>>();
            _scriptTokens = new Dictionary<Task, CancellationTokenSource>();
        }

        internal IEnumerable<string> GetRunningScripts()
        {
            return _scriptTasks.Keys;
        }

        internal static LWScriptManager GetInstance()
        {
            if (_instance == null)
                _instance = new LWScriptManager();
            return _instance;
        }


        internal Task StartScriptTask(object scriptInstance, object[] args = null)
        {
            if (args == null)
                args = new object[0];

            var scriptName = scriptInstance.GetType().Name;

            if (!_scriptTasks.ContainsKey(scriptName))
            {
                _scriptTasks.Add(scriptName, new List<Task>());
            }

            var list = _scriptTasks[scriptInstance.GetType().Name];
            var token = new CancellationTokenSource();
            var scriptTask = new Task(async () =>
            {
                try
                {
                    var executeMethod = scriptInstance.GetType().GetMethod("Execute");
                    MDebug.MDebugAPI.LogDebug(_log.Debug(string.Format("{0} Script Started", scriptName, 0)));
                    var task = (Task)executeMethod.Invoke(scriptInstance, new object[] { args, token.Token });
                    while (!task.IsCompleted)
                        await BaseScript.Delay(0);

                    if (task.Exception != null)
                    {
                        throw task.Exception;
                    }
                    //_terminationCode = 0;
                }
                catch (AggregateException e)
                {
                    //_terminationCode = 2;
                    MDebug.MDebugAPI.LogDebug(e.ToString());
                }
                catch (Exception e)
                {
                    // _terminationCode = 1;
                    MDebug.MDebugAPI.LogDebug(e.ToString());
                }
                finally
                {
                    MDebug.MDebugAPI.LogDebug(_log.Debug(string.Format("{0} Script Finished, Termination Code: {1}", scriptName, 1)));
                }
            }, token.Token);
            _scriptTokens.Add(scriptTask, token);
            list.Add(scriptTask);
            scriptTask.Start();
            return scriptTask;
        }

        internal void StopScriptTask(string scriptName)
        {
            if (string.IsNullOrEmpty(scriptName)) return;
            if (_scriptTasks.ContainsKey(scriptName))
            {
                var scriptTasks = _scriptTasks[scriptName].ToArray();
                for (int i = 0; i < scriptTasks.Length; i++)
                {
                    var task = scriptTasks[i];
                    var token = _scriptTokens[task];
                    token.Cancel();
                    _scriptTokens.Remove(task);
                }
                _scriptTasks.Remove(scriptName);
                Console.WriteLine(String.Format("{0} Scripts removed", scriptName));
            }

        }

        internal Type GetScriptType(string scriptName)
        {
            if (string.IsNullOrEmpty(scriptName)) return null; 
            if (_scriptTypes.ContainsKey(scriptName))
                return _scriptTypes[scriptName];
            return null;
        }

        internal object CreateScriptInstance(Type type)
        {
            if (type == null)
                return null;
            var instance = Activator.CreateInstance(type);
            return instance;
        }

        internal int GetScriptInstanceCount(string scriptName)
        { 
            if (!_scriptTasks.ContainsKey(scriptName))
                return 0;
            var list = _scriptTasks[scriptName];
            return list.Count;
        } 

        internal bool DoesScriptExist(string scriptName)
        {
            return _scriptTypes.ContainsKey(scriptName);
        }

        internal void ProcessAssembly(string assemblyString)
        {
            _log.Debug($"Loading assembly {assemblyString.ToString()}");
            var ass = Assembly.Load(assemblyString.ToString());
            _log.Debug($"Scanning assembly {assemblyString.ToString()} for scripts");
            var types = ass.GetTypes().Where(e => (object)e.GetMethod("Execute") != null);
            _log.Debug($"Found {types.Count()} scripts that have an execute method");
            foreach (var item in types)
            {
                _scriptTypes.Add(item.Name, item);
            }
            _log.Debug($"Loading complete");
        }

        internal void RemoveScript(ScriptContainer scriptContainer)
        {
            _liveScripts.Remove(scriptContainer);
        }
    }
}
