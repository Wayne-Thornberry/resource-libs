namespace Proline.CScripting
{
    internal class StartScriptRequest
    {
        public StartScriptRequest(string scriptName, object[] args)
        {
            ScriptName = scriptName;
            Args = args;
        }

        internal string ScriptName { get; set; }
        internal object[] Args { get; set; }
    }
}
