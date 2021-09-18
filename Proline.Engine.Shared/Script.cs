using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.Engine.Internals;

namespace Proline.Engine
{
    public static class Script
    {
        /// <summary>
        /// Starts a new script instance from a script name
        /// </summary>
        /// <param name="scriptName"></param>
        public static int StartScript(string scriptName, params object[] args)
        {  
            var em = InternalManager.GetInstance();
            while (!em.IsScriptRequestsQueueEmpty())
            {
                var requests = em.DequeueStartScriptRequest();
                Script.StartScript(requests.ScriptName);
            }
            var extensions = em.GetExtensions();
            var wrapper = em.GetScript(scriptName);
            if (wrapper == null) return -1;
            return wrapper.StartNewInstance(args); 
        }

        /// <summary>
        /// Terminates all scripts given a scriptName
        /// </summary>
        /// <param name="scriptName"></param>
        public static void TerminateScript(string scriptName)
        {

        }

        /// <summary>
        /// Terminates one specific script given an Id
        /// </summary>
        /// <param name="scriptId"></param>
        public static void TerminateScript(int scriptId)
        {

        }
    }
}
