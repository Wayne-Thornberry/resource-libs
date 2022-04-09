using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.EngineFramework.Scripting;

namespace Proline.ExampleClient.Scripts
{
    public class BlowUp : DemandScript
    {
        public BlowUp(string name) : base(name)
        {
        }

        public override async Task Execute(object[] args, CancellationToken token)
        {
            if(args.Length > 0)
            {
                var handle = (int)args[0];
                var entity = Entity.FromHandle(handle);
                World.AddExplosion(entity.Position, ExplosionType.Car, 2f, 0f);
            }
        }
    }
}
