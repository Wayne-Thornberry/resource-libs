using CitizenFX.Core;
using Proline.ClassicOnline.MBrain.Tasks;
using Proline.ClassicOnline.MScripting.Events;
using Proline.ClassicOnline.MScripting.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.S
{
    public class InitSession
    {
        public async Task Execute()
        { 
            var gc = new ProcessScriptingObjectsAndPositions();
            while (true)
            {
                var task = Task.Factory.StartNew(gc.Execute);
                await BaseScript.Delay(1000);
            }
        }
    }
}
