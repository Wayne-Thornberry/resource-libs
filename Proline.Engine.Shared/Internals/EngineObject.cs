using Proline.Engine.Data;
using Proline.Engine.Debugging;

namespace Proline.Engine.Internals
{
    public abstract class EngineObject
    {
        internal Log _log;
        internal string _type;
        protected InternalManager im;

        public string Type => _type;
        protected bool IsClient => EngineConfiguration.IsClient;

        protected EngineObject(string typeName)
        { 
            _log = new Log();
            _type = typeName;
        }

        protected void LogDebug(object data)
        {
            im = InternalManager.GetInstance();
            //Debugger.LogDebug($"[{_type}] " + data);
        }
        protected void LogWarn(object data)
        {
           // Debugger.LogWarn($"[{_type}] " + data);
        }
        protected void LogError(object data)
        {
           // Debugger.LogError($"[{_type}] " + data);
        }

        public void DumpLog()
        {
            //_console.WriteLine("--------------------------- Begin log dump ------------------------------------");
            //foreach (var item in _log.GetLog())
            //{
            //    _console.WriteLine(item);
            //}
            //_console.WriteLine("--------------------------- End log dump ------------------------------------");
        }
    }
}
