using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MScripting.Internal;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting.Commands
{
    public class ListScriptsCommand : ModuleCommand
    {
        public ListScriptsCommand() : base("ListScripts")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var sm = ScriptTypeLibrary.GetInstance();
            foreach (var item in sm.Keys)
            {
                Console.WriteLine(item);
            } 
        } 
    }
}
