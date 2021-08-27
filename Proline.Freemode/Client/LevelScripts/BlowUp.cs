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
