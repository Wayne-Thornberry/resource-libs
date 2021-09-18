


using Newtonsoft.Json;
using Proline.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Proline.Engine.Networking;
using CitizenFX.Core.Native;
using Proline.Engine.Internals;

namespace Proline.Engine
{
    public class ServerEngineService : EngineService
    {
        public ServerEngineService() 
        {
            //Debug = new DebugObject(new ServerConsole());
        }
        public static void Main(string[] args)
        {
            var service = new ServerEngineService();
            var resourceName = args[0];
            var sourceHandle = int.Parse(args[1]);
            var isDebug = bool.Parse(args[2]);
            var isIsolated = resourceName.Equals("ConsoleApp");
            EngineConfiguration.IsClient = sourceHandle != -1;
            EngineConfiguration.IsIsolated = isIsolated;
            EngineConfiguration.IsDebugEnabled = isDebug;
            EngineConfiguration.OwnerHandle = sourceHandle;
            EngineConfiguration.EnvTypeName = (EngineConfiguration.IsClient ? "Client" : "Server");
            service.Initialize();
        }

        protected override void LoadConfig(bool isDebugEnabled)
        {
            var resourceName = API.GetCurrentResourceName();
            var configJson = API.LoadResourceFile(resourceName, "Engine.json");
            EngineConfiguration.LoadConfig(configJson);
            EngineConfiguration.IsDebugEnabled = isDebugEnabled;
        }
    }
}
