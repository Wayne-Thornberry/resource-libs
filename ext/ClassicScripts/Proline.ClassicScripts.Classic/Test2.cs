using System.Threading;
using System.Threading.Tasks;
using Proline.EngineFramework.Scripting;

namespace Proline.ExampleClient.Scripts
{
    public class Test2 : DemandScript
    {
        public Test2(string name) : base(name)
        {
        }

        public override async Task Execute(object[] args, CancellationToken token)
        {


            CitizenFX.Core.Game.PlayerPed.Kill();

        }
    }
}
