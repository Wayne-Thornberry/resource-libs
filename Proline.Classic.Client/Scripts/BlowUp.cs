using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Engine.Scripting;

namespace Proline.Classic.LevelScripts
{
    public class BlowUp : LevelScript
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
