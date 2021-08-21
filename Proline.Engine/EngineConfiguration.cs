
using Newtonsoft.Json;
using Proline.Engine.Data;
using System;
using System.Collections.Generic;

namespace Proline.Engine
{
    internal static class EngineConfiguration
    {
        private static EngineConfig _config;

        public static bool IsDebugEnabled { get; internal set; }
        public static ScriptPackageConfig[] ScriptPackages => _config.ScriptPackages;
        public static int EnvType => _config.EnvType;
        public static string[] Assemblies => _config.Assemblies;
        public static ComponentDetails[] Components => _config.Components;
        public static ExtensionDetails[] Extensions => _config.Extensions;
        public static bool IsIsolated => _config.ConsoleLaunch;
         

        internal static void LoadConfig()
        {
            var configJson = CitizenAccess.GetInstance().LoadResourceFile(CitizenAccess.GetInstance().GetCurrentResourceName(), "Proline.Engine.Script.json");
            _config = JsonConvert.DeserializeObject<EngineConfig>(configJson);
        }   
    }
}