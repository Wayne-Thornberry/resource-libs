
extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;

using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.LevelScripts
{
    public class ExampleTest : LevelScript
    {
        

        public override async Task Execute(params object[] args)
        {
            while (true)
            {
                testc();
                break;
                await BaseScript.Delay(0);
            }


        }

        private async Task testc()
        {
            Screen.ShowSubtitle("dasdsada");

        }
    }
}
