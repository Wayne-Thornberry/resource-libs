using Proline.Framework;

namespace Proline.Engine
{
    public abstract class GameScript : LevelScript
    {

        public GameScript()
        {
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
    }
}
