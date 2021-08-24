using CitizenFX.Core;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.LevelScripts
{
    public class Test2 : GameScript
    {
        public override async Task Execute(params object[] args)
        {


            Game.PlayerPed.Kill();

        }
    }
}
