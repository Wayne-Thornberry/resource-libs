using Newtonsoft.Json;

namespace Proline.Engine.Data
{
    public static class EngineConfiguration
    {
        private const string FileName = "Engine.json";
        private static EngineConfig _config;
        public static bool IsClient { get; set; }
        public static bool IsDebugEnabled { get; set; }
        public static ScriptPackageConfig[] ScriptPackages => _config.ScriptPackages;
        public static int EnvType => _config.EnvType;
        public static AssemblyDetails[] Assemblies => _config.Assemblies;
        public static ComponentDetails[] Components => _config.Components;
        public static ExtensionDetails[] Extensions => _config.Extensions;
        public static bool IsIsolated { get; set; }
        public static string[] StartupScripts => _config.StartScripts;

        public static int OwnerHandle { get; set; }
        public static object EnvTypeName { get; set; }

        public static void LoadConfig(string configJson)
        { 
            _config = JsonConvert.DeserializeObject<EngineConfig>(configJson);
        }   
    }
}