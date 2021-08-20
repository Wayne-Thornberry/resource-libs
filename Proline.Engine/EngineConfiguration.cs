
using Newtonsoft.Json;
using Proline.Engine.Data;
using System;

namespace Proline.Engine
{
    internal static class EngineConfiguration
    {
        private static EngineConfig _config;

        public static int EnvType { get; internal set; }

        public static ComponentDetails[] GetComponents()
        {
            LoadConfig();
            return _config.Components;
        }

        private static void LoadConfig()
        {
            if(_config == null)
            { 
                var configJson = CitizenAccess.GetInstance().LoadResourceFile(CitizenAccess.GetInstance().GetCurrentResourceName(), "Proline.Engine.Script.json");
                _config = JsonConvert.DeserializeObject<EngineConfig>(configJson);
                if (_config.Extensions == null)
                    _config.Extensions = new ExtensionDetails[0];
                EnvType = _config.EnvType;
            }
        }

        internal static LevelScriptAssembly[] GetScripts()
        {
            LoadConfig();
            return _config.Scripts;
        }

        internal static ExtensionDetails[] GetExtensions()
        { 
            LoadConfig();
            return _config.Extensions;
        }

        internal static string[] GetAssemblies()
        {
            LoadConfig();
            return _config.Assemblies;
        }

        internal static bool IsDebugEnabled()
        {
            return _config.EnableDebug;
        }

        internal static bool IsEngineConsoleApp()
        {
            return _config.ConsoleLaunch;
        }
    }
}