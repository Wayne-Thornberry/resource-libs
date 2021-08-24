using CitizenFX.Core;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.LevelScripts
{
    public class ExampleTest : GameScript
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
            CitizenFX.Core.UI.Screen.ShowSubtitle("dasdsada");

        }
    }
}
