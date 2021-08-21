
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Client.LevelScripts
{
    public class ExampleMain : GameScript
    {
        public override async Task Execute(params object[] args)
        {
            LogDebug(" Script executed" + args[0]);


            var result = ExampleAPI.UnlockNeareastVehicle(3, out var x);
            Debugger.LogDebug(result);
            Debugger.LogDebug(x);
        }
    }
}
