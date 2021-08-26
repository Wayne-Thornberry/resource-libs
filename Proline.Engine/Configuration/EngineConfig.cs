namespace Proline.Engine.Data
{ 
    internal class EngineConfig
    {
        public string GameName { get; set; }
        public bool ConsoleLaunch { get; set; }
        public ExtensionDetails[] Extensions { get; set; }
        public ComponentDetails[] Components { get; set; }
        public ScriptPackageConfig[] ScriptPackages { get; set; }
        public string[] StartScripts { get; set; }
        public string CentralEndpoint { get; set; }
        public long MasterKey { get; set; }
        public bool EnableDebug { get; set; }
        public string Args { get; set; }
        public AssemblyDetails[] Assemblies { get; set; }
        public int EnvType { get; set; }
    }
}
