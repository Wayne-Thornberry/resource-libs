using System;
using System.Threading.Tasks;

namespace Proline.CScripting.Framework
{
    internal enum ScriptExitCode : int
    {
        SUCCESS = 0,
        ERROR = 1,
    }

    public abstract class ScriptInstance 
    {
        protected ScriptInstance() 
        {

        }
         
        public object[] Parameters { get; set; }
        public int Stage { get; set; }


        protected void StartNewScript(string scriptName, params object[] args)
        {
            var im = InternalManager.GetInstance();
            im.EnqueueStartScriptRequest(new StartScriptRequest(scriptName, args));
        }

        public abstract Task Execute(params object[] args);
    }
}
