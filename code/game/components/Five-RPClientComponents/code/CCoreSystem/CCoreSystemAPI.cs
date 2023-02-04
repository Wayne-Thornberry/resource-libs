using CCoreSystem.Internal;
using CDebugActions;
using CitizenFX.Core; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace CCoreSystem
{
    public class CCoreSystemAPI : ICCoreSystemAPI
    {


        public void TriggerScriptEvent(string eventName, params object[] args)
        {
            try
            {
                var livescripts = ScriptManager.LiveScripts;
                foreach (var item in livescripts)
                {
                    item.EnqueueEvent(eventName, args);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool GetEventExists(object scriptInstance, string eventName)
        {
            try
            {
                var script = ScriptManager.GetLiveScript(scriptInstance);


                if (script == null)
                    return false;

                return script.HasEvent(eventName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

        public object[] GetEventData(object scriptInstance, string eventName)
        {
            try
            {
                var script = ScriptManager.GetLiveScript(scriptInstance);

                if (script == null)
                    return new object[0];

                return script.DequeueEvent(eventName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return new object[0];
        }

        public int StartNewScript(string scriptName, params object[] args)
        {
            try
            {
                // Before we start a new script, attempt to clean up old scripts that are finished  
                // This is the only way we have to cleanup old scripts that have started and finished previously
                ScriptManager.TerminateMarkedAsNoLongerNeedeScripts();

                var api = new CDebugActionsAPI();

                // Get scripting config
                var scriptingConfig = ScriptingConfigSection.ModuleConfig;

                // If we have a config, then we can load the levelscripts
                var scriptTypes = ScriptManager.GetAllScriptTypes();

                // Get type matching script name
                if (!scriptTypes.ContainsKey(scriptName))
                {
                    Console.WriteLine("Script {0} cannot be found");
                    return -1;
                }

                // Get the script type from the dictionary that was created
                var type = scriptTypes[scriptName];

                Console.WriteLine(string.Format("Creating script instance of {0}", scriptName));

                // Create an instance of that type found. There should be no constructor on these scripts
                var instance = Activator.CreateInstance(type);

                if (instance == null)
                {
                    Console.WriteLine(string.Format("Unable to create script instance of {0}, instance came back null", scriptName));
                    return -1;
                }

                // Create a shell for the script
                var id = LiveScript.StartNew(instance);

                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return -1;
        }


        public int GetInstanceCountOfScript(string scriptName)
        {
            try
            {
                var livescripts = ScriptManager.LiveScripts;
                var count = livescripts.Where(e => e.Name.Equals(scriptName)).Count();

                //  Console.WriteLine(String.Format("Getting the instance count of script {0} count: {1}", scriptName, count));
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0;
        }

        public void MarkScriptAsNoLongerNeeded(object callingClass)
        {
            try
            {
                var livescripts = ScriptManager.LiveScripts;
                var script = livescripts.FirstOrDefault(e => e.Instance == callingClass);
                Console.WriteLine(string.Format("Requesting that script instances by the name of {0} be marked as no longer needed", script.Name));
                script.IsMarkedForNolongerNeeded = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void MarkScriptAsNoLongerNeeded(string scriptName)
        {
            try
            {
                var livescripts = ScriptManager.LiveScripts;
                var cls = scriptName;
                Console.WriteLine(string.Format("Requesting that all script instances by the name of {0} be marked as no longer needed", scriptName));
                var scripts = livescripts.Where(e => e.Name.Equals(scriptName));
                foreach (var item in scripts)
                {
                    item.IsMarkedForNolongerNeeded = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Terminates all script instances with the passed scriptName
        /// </summary>
        /// <param name="scriptName"></param>
        public void TerminateScript(string scriptName)
        {
            try
            {
                var livescripts = ScriptManager.LiveScripts;
                var scripts = livescripts.Where(e => e.Name.Equals(scriptName));
                Console.WriteLine(string.Format("Requesting that all script instances by the name of {0} be terminated", scriptName));
                foreach (var script in scripts)
                {
                    script.Terminate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Terminates the passed script object task instance
        /// </summary>
        /// <param name="scriptInstance"></param>
        public void TerminateScriptInstance(object scriptInstance)
        {
            try
            {
                var livescripts = ScriptManager.LiveScripts;
                var script = livescripts.FirstOrDefault(e => e.Instance == scriptInstance);
                if (script == null)
                    return;
                Console.WriteLine(string.Format("Requesting that a specific script instances by the name of {0} be terminated", script.Name));
                script.Terminate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        public void TerminateScriptTask(int taskId)
        {
            try
            {
                var livescripts = ScriptManager.LiveScripts;
                var script = livescripts.FirstOrDefault(e => e.ExecutionTask.Id == taskId);
                if (script == null)
                    return;
                Console.WriteLine(string.Format("Requesting that a specific script instances by the name of {0} be terminated", script.Name));
                script.Terminate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Waits a specific amount using the BaseScript.Delay function, 0 ms waits till the next frame is rendered
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public async Task Delay(int ms)
        {
            try
            {
                await BaseScript.Delay(ms);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
