using CitizenFX.Core;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.LevelScripts
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
