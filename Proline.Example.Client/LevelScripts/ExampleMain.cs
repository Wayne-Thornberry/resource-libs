
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.LevelScripts
{
    [Client]
    public class ExampleMain : GameScript
    {
        private int tick;

        public override async Task Execute(params object[] args)
        {
            //NativeAPI.CallNativeAPI(231321313, null);
            var ti = DateTime.UtcNow;
            ExampleAPI.Example();
            Debugger.LogDebug((DateTime.UtcNow - ti).ToString("ss'.'ffff")); 
            while (Stage == 0)
            {
                var result = ExampleAPI.Example();
                //Debugger.LogDebug(result);
                await Delay(0);
                tick++;
                if (tick > 100)
                    Stage = -1;
            }
        }
    }
}
