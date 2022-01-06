using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.CScripting.Framework;

namespace Proline.Classic.Scripts
{
    public class BlowUp : ScriptInstance
    {
        public override async Task Execute(params object[] args)
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
