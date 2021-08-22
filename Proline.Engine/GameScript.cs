using Proline.Framework;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public abstract class GameScript : LevelScript
    {
        private IScriptSource _ca;

        public GameScript()
        {
            _ca = CitizenAccess.GetInstance();
            //Header = Util.GenerateCoreHeader(this);
        }
        public void DoesEntityExist(int handle, out bool exists)
        {
            exists = false;
            //var engine = (Engine)AppEnviroment.GetEnviorment(); 
            //return engine.DoesEntityExist(Header, handle, out exists);
        }
        public void LogDebug(object data)
        { 
            Debugger.LogDebug(this, data);
            //var engine = (Engine)AppEnviroment.GetEnviorment();
            //return engine._Debugger.LogDebug(Header, data);
        }
        public void StartNewScript(string scriptName, object[] args)
        {
            EngineAccess.StartNewScript("CScripting"); 
            //var engine = (Engine)AppEnviroment.GetEnviorment();
            //return engine.StartNewScript(Header, scriptName, args);
        }

        protected async Task Delay(int ms)
        {
            await _ca.Delay(ms);
        }
    }
}
