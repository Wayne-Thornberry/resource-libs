namespace Proline.CScripting
{
    internal class ScriptPackageConfig
    {
        public string Assembly { get; set; }
        public int EnvType { get; set; }
        public bool DebugOnly { get; set; }
        public string[] ScriptClasses { get; set; }
        public ScriptConfig[] ScriptConfigs { get; set; }
    }
}
