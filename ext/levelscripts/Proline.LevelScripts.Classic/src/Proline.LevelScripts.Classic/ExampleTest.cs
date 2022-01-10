using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.CScripting.Framework;
using Proline.Resource.Component;

namespace Proline.Classic.Scripts
{
    public class ExampleTest : ScriptInstance
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
