using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;

namespace Proline.ClassicOnline.LevelScripts
{
    public class ExampleTest 
    {
        public ExampleTest()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
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
