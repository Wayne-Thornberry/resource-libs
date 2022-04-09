using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.EngineFramework.Scripting; 

namespace Proline.ExampleClient.Scripts
{
    public class ExampleTest : DemandScript
    {
        public ExampleTest(string name) : base(name)
        {
        }

        public override async Task Execute(object[] args, CancellationToken token)
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
