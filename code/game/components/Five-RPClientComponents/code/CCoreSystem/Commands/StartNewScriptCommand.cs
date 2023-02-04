using Proline.Resource.Framework;
using System.Linq;

namespace CCoreSystem.Commands
{
    public class StartNewScriptCommand : ResourceCommand
    {
        public StartNewScriptCommand() : base("StartNewScript")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Count() == 0)
            {
                return;
            }
            var scriptName = args[0].ToString();
            var coreAPI = new CCoreSystemAPI();
            coreAPI.StartNewScript(scriptName);
        }
    }
}
