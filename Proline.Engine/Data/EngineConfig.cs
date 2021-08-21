namespace Proline.Engine.Data
{
    internal class ExtensionDetails
    {
        public string Assembly { get; set; }
        public string[] ExtensionClasses { get; set; }
    }

    internal class ComponentDetails
    {
        public string Assembly { get; set; }
        public string ComponentName { get; set; }
        public string HandlerClass { get; set; }
        public string CommandClass { get; set; }
        public string ControllerClass { get; set; }
        public string APIClass { get; set; }
        public string SimpleComponentClass { get; set; }
        public string[] ScriptClasses { get; set; }
        public bool DebugOnly { get; set; }
    }

    internal class ScriptConfig
    {
        public string ScriptName { get; set; }
        public bool DebugOnly { get; set; } 
        public object[] AddionalArgs { get; set; }
    }

    internal class ScriptPackageConfig
    {
        public string Assembly { get; set; }
        public string[] StartScripts { get; set; }
        public bool DebugOnly { get; set; }
        public string[] ScriptClasses { get; set; }
        public ScriptConfig[] ScriptConfigs { get; set; }
    }

    internal class EngineConfig
    {
        public string GameName { get; set; }
        public bool ConsoleLaunch { get; set; }
        public ExtensionDetails[] Extensions { get; set; }
        public ComponentDetails[] Components { get; set; }
        public ScriptPackageConfig[] ScriptPackages { get; set; }
        public string CentralEndpoint { get; set; }
        public long MasterKey { get; set; }
        public bool EnableDebug { get; set; }
        public string Args { get; set; }
        public string[] Assemblies { get; set; }
        public int EnvType { get; set; }
    }
}
