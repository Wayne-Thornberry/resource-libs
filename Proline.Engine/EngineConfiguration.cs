
using Newtonsoft.Json;
using Proline.Engine.Data;
using System;
using System.Collections.Generic;

namespace Proline.Engine
{
    internal static class EngineConfiguration
    {
        private static EngineConfig _config;
        public static bool IsClient { get; internal set; }
        public static bool IsDebugEnabled { get; internal set; }
        public static ScriptPackageConfig[] ScriptPackages => _config.ScriptPackages;
        public static int EnvType => _config.EnvType;
        public static AssemblyDetails[] Assemblies => _config.Assemblies;
        public static ComponentDetails[] Components => _config.Components;
        public static ExtensionDetails[] Extensions => _config.Extensions;
        public static bool IsIsolated => _config.ConsoleLaunch; 
        public static string[] StartupScripts => _config.StartScripts;

        internal static void LoadConfig()
        {
            var configJson = EngineService.GetInstance().LoadResourceFile(EngineService.GetInstance().GetCurrentResourceName(), "Engine.json");
            _config = JsonConvert.DeserializeObject<EngineConfig>(configJson);
        }   
    }
}