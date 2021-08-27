extern alias Server;
extern alias Client;

using Newtonsoft.Json;
using Proline.Engine;
using Proline.Engine.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public abstract class EngineObject
    {
        private Log _log;
        private IScriptSource _source;
        private string _type;

        public string Type => _type;

        protected EngineObject(string typeName)
        {
            _log = new Log();
            _type = typeName;
        }
         
        protected void LogDebug(object data)
        {
            if (!EngineConfiguration.IsDebugEnabled) return;
            var d = _log.LogDebug($"[{_type}] " + data);
            //if (EngineConfiguration.IsClient)
            //    ExecuteEngineMethodServer<int>(EngineConstraints.Log, 0, data.ToString());
            F8Console.WriteLine(d);
        }
        protected void LogWarn(object data)
        {
            if (!EngineConfiguration.IsDebugEnabled) return;
            var d = _log.LogWarn($"[{_type}] " + data);
            //if(EngineConfiguration.IsClient)
            //    ExecuteEngineMethodServer(EngineConstraints.Log, 1, data.ToString());
            F8Console.WriteLine(d);
        }
        protected void LogError(object data)
        {
            if (!EngineConfiguration.IsDebugEnabled) return;
            var d = _log.LogError($"[{_type}] " + data);
            //if (EngineConfiguration.IsClient)
            //    ExecuteEngineMethodServer(EngineConstraints.Log, 2, data.ToString());
            F8Console.WriteLine(d);
        }

        public void DumpLog()
        {
            F8Console.WriteLine("--------------------------- Begin log dump ------------------------------------");
            foreach (var item in _log.GetLog())
            {
                F8Console.WriteLine(item);
            }
            F8Console.WriteLine("--------------------------- End log dump ------------------------------------");
        }


        protected async Task Delay(int ms)
        {
            if (EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
                await Client.CitizenFX.Core.BaseScript.Delay(ms);
            else if (!EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
                await Server.CitizenFX.Core.BaseScript.Delay(ms);
            else
                await Task.Delay(ms);
        }
    }

}
