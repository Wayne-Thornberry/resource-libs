using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting.Client.Commands
{
    public class ListScriptTasksCommand : ModuleCommand
    {
        public ListScriptTasksCommand() : base("ListScriptTasks")
        {
        }

        protected override Delegate GetCommandHandler()
        {
            return new Action<string>(OnCommandExecute);
        }

        private void OnCommandExecute(string obj)
        {

            var sm = LWScriptManager.GetInstance();
            var scripts = sm.GetRunningScripts();
            foreach (var item in scripts)
            {
                var count = sm.GetScriptInstanceCount(item);
                Console.WriteLine(String.Format("{0} Instances: {1}", item, count)); 
            } 
        }
    }
}
