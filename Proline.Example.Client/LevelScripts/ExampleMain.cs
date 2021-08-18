using Proline.Engine.Client;
using Proline.Engine.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Client.LevelScripts
{
    public class ExampleMain : LevelScript
    {
        public override async Task Execute(params object[] args)
        {
            Debugger.LogDebug(this, " Script executed");
        }
    }
}
