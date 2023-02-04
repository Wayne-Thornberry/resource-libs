using CCoreSystem;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static async Task Delay(int ms)
        {
            var coreAPI = new CCoreSystemAPI();
            await coreAPI.Delay(ms);
        }

        public static int StartNewScript(string scriptName, params object[] args)
        {
            var coreAPI = new CCoreSystemAPI();
            return coreAPI.StartNewScript(scriptName, args);
        }


        public static int GetInstanceCountOfScript(string scriptName)
        {
            var coreAPI = new CCoreSystemAPI();
            return coreAPI.GetInstanceCountOfScript(scriptName);
        }

        public static void MarkScriptAsNoLongerNeeded(object callingClass)
        {
            var coreAPI = new CCoreSystemAPI();
            coreAPI.MarkScriptAsNoLongerNeeded(callingClass);
        }

        public static void MarkScriptAsNoLongerNeeded(string scriptName)
        {
            var coreAPI = new CCoreSystemAPI();
            coreAPI.MarkScriptAsNoLongerNeeded(scriptName);
        }

        /// <summary>
        /// Terminates all script instances with the passed scriptName
        /// </summary>
        /// <param name="scriptName"></param>
        public static void TerminateScript(string scriptName)
        {
            var coreAPI = new CCoreSystemAPI();
            coreAPI.TerminateScript(scriptName);
        }

        /// <summary>
        /// Terminates the passed script object task instance
        /// </summary>
        /// <param name="scriptInstance"></param>
        public static void TerminateScriptInstance(object scriptInstance)
        {
            var coreAPI = new CCoreSystemAPI();
            coreAPI.TerminateScriptInstance(scriptInstance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        public static void TerminateScriptTask(int taskId)
        {
            var coreAPI = new CCoreSystemAPI();
            coreAPI.TerminateScriptInstance(taskId);
        }
    }
}
