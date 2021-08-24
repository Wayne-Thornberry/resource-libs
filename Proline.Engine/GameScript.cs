using Proline.Engine;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public abstract class GameScript : ScriptInstance
    {
        private IScriptSource _ca;
        internal Log _log;

        public GameScript()
        {
            _ca = EngineService.GetInstance();
            _log = new Log();
        } 

        protected void LogDebug(object data)
        {
            _log.LogDebug(Type + data);
        }
        protected void LogWarn(object data)
        {
            _log.LogWarn(Type + data);
        }
        protected void LogError(object data)
        {
            _log.LogError(Type + data);
        }

        protected void StartNewScript(string scriptName, object[] args)
        {
            EngineAccess.StartNewScript(scriptName); 
        }

        protected async Task Delay(int ms)
        {
            await _ca.Delay(ms);
        }
    }
}
