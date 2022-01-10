using CitizenFX.Core;
using Proline.Component.Framework.Client.Access;
using Proline.Resource.Client.Component;
using Proline.Resource.Client.Logging;
using Proline.Resource.Component;
using Proline.Resource.Engine.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace Proline.CScripting.Framework
{

    public abstract class ScriptInstance 
    {
        private Log _log => Logger.GetInstance().GetLog();
        protected ScriptInstance() 
        {
            Name = this.GetType().Name;
        }
         
        public object[] Parameters { get; set; }
        public int Stage { get; set; }
        public long Id { get; set; }
        public object Name { get; set; }

        public abstract Task Execute(params object[] args);

        public void StartNewScript(string name, params object[] args)
        {
            EngineAPI.StartNewScript(name, args);
        }

        public void LogDebug(object data)
        {
            _log.Debug($"[{Name}] " + data.ToString());
        }

        public void LogError(object data)
        {
            _log.Error($"[{Name}] " + data.ToString());
        }

        public void LogInfo(object data)
        {
            _log.Info($"[{Name}] " + data.ToString());
        }

        public void LogWarn(object data)
        {
            _log.Warn($"[{Name}] " + data.ToString());
        }
    }
}
