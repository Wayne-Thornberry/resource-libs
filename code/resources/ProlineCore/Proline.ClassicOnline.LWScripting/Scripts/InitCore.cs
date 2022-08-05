using CitizenFX.Core;
using Proline.ClassicOnline.MScripting.Events;
using Proline.ClassicOnline.MScripting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Client.Scripts
{
    public class InitCore 
    {
        public async Task Execute()
        { 
            //PlayerJoinedEvent.SubscribeEvent();
            PlayerReadyEvent.SubscribeEvent();

            var lsAssembly = ScriptingConfigSection.ModuleConfig;
            Console.WriteLine("Retrived config section");
            var _lwScriptManager = ScriptTypeLibrary.GetInstance();

            if (lsAssembly != null)
            {
                Console.WriteLine($"Loading level scripts. from {lsAssembly.LevelScriptAssemblies.Count()} assemblies");
                foreach (var item in lsAssembly.LevelScriptAssemblies)
                {
                    _lwScriptManager.ProcessAssembly(item);
                }
                ScriptTypeLibrary.HasLoadedScripts = true;
            }

            var gc = new GarbageCleaner();
            while (true)
            {
                var task = Task.Factory.StartNew(gc.Execute); 
                await BaseScript.Delay(1000);
            }

        }
    }
}
