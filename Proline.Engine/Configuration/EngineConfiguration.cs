extern alias Server;
extern alias Client;

using Newtonsoft.Json;
using Proline.Engine.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace Proline.Engine
{
    internal static class EngineConfiguration
    {
        private const string FileName = "Engine.json";
        private static EngineConfig _config;
        public static bool IsClient { get; internal set; }
        public static bool IsDebugEnabled { get; internal set; }
        public static ScriptPackageConfig[] ScriptPackages => _config.ScriptPackages;
        public static int EnvType => _config.EnvType;
        public static AssemblyDetails[] Assemblies => _config.Assemblies;
        public static ComponentDetails[] Components => _config.Components;
        public static ExtensionDetails[] Extensions => _config.Extensions;
        public static bool IsIsolated { get; internal set; }
        public static string[] StartupScripts => _config.StartScripts;

        public static int OwnerHandle { get; internal set; }
        public static object EnvTypeName { get; internal set; }

        internal static void LoadConfig()
        {
            var resourceName = "";
            var configJson = "";
            if (EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
            {
                resourceName = Client.CitizenFX.Core.Native.API.GetCurrentResourceName();
                configJson = Client.CitizenFX.Core.Native.API.LoadResourceFile(resourceName, FileName);
            }
            else if(!EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
            { 
                resourceName = Server.CitizenFX.Core.Native.API.GetCurrentResourceName();
                configJson = Server.CitizenFX.Core.Native.API.LoadResourceFile(resourceName, FileName);
            }
            else
            {
                configJson = File.ReadAllText(FileName);
            }

            _config = JsonConvert.DeserializeObject<EngineConfig>(configJson);
        }   
    }
}