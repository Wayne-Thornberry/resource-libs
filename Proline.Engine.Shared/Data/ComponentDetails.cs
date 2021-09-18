namespace Proline.Engine.Data
{
    public class ComponentDetails
    {
        public int EnvType { get; set; }
        public string Assembly { get; set; }
        public string ComponentName { get; set; }  
        public string APIClass { get; set; }
        public string CommanderClass { get; set; }
        public string HandlerClass { get; set; }
        public string ComponentClass { get; set; }
        public string[] ScriptClasses { get; set; }
        public bool DebugOnly { get; set; }
    }
}
