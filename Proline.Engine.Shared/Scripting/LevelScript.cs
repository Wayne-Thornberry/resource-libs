using System;
using System.Threading.Tasks;
using Proline.Engine.Internals;

namespace Proline.Engine.Scripting
{
    internal enum ScriptExitCode : int
    {
        SUCCESS = 0,
        ERROR = 1,
    }

    public abstract class LevelScript : EngineObject
    {
        protected LevelScript() : base("LevelScript")
        {

        }
         
        public object[] Parameters { get; set; }
        public int Stage { get; set; }

        internal async Task Run(InternalScript script, params object[] args)
        {
            try
            {
                script.BeginRunInstance(this);
                await Execute(args);
                script.EndRunInstance(this);
            }
            catch (Exception e)
            {
                LogError(e.ToString());
            }
        }

        protected void StartNewScript(string scriptName, params object[] args)
        {
            var im = InternalManager.GetInstance();
            im.EnqueueStartScriptRequest(new StartScriptRequest(scriptName, args));
        }

        protected void TerminateScript()
        {
            throw new Exception("Script Terminated");
        }

        public abstract Task Execute(params object[] args);
    }
}
