using CitizenFX.Core;
using Proline.ClassicOnline.MScripting.Scripts;
using Proline.Resource.Console;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting
{
    public class ScriptingContext : ModuleContext
    { 
        public override void OnLoad()
        {
            BaseScript.RegisterScript(new ScriptingScript());
            EConsole.WriteLine("Tesdsadat???");
            base.OnLoad();
        }

        public override void OnStart()
        {
            BaseScript.RegisterScript(new ScriptingScript());
            EConsole.WriteLine("Test???");
            base.OnStart();
        }
    }
}
