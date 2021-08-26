using System;
using System.Threading.Tasks;

namespace Proline.Engine
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

        internal async Task Run(Script script, params object[] args)
        {
            try
            {
                script.BeginRunInstance(this);
                await Execute(args);
                script.EndRunInstance(this);
            }
            catch (Exception e)
            {
                LogError(e);
            }
        }

        protected void StartNewScript(string scriptName, params object[] args)
        {
            var im = InternalManager.GetInstance();
            im.EnqueueStartScriptRequest(new StartScriptRequest(scriptName, args));
        }

        public abstract Task Execute(params object[] args);
    }
}
