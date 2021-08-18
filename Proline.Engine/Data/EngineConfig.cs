namespace Proline.Engine.Client.Data
{
    public class ExtensionDetails
    {
        public string Assembly { get; set; }
        public string[] ExtensionClasses { get; set; }
    }

    public class ComponentDetails
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

    public class LevelScriptAssembly
    {
        public string Assembly { get; set; }
        public string StartupScript { get; set; }
        public bool DebugOnly { get; set; }
        public string[] ScriptClasses { get; set; }
    } 

    public class EngineConfig
    {
        public string GameName { get; set; }
        public bool ConsoleLaunch { get; set; }
        public ExtensionDetails[] Extensions { get; set; }
        public ComponentDetails[] Components { get; set; }
        public LevelScriptAssembly[] Scripts { get; set; }
        public string CentralEndpoint { get; set; }
        public long MasterKey { get; set; }
        public bool EnableDebug { get; set; }
        public string Args { get; set; }
        public string[] Assemblies { get; set; }
    }
}
