using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Engine.Scripting;

namespace Proline.Classic.LevelScripts
{
    public class Test2 : LevelScript
    {
        public override async Task Execute(params object[] args)
        {


            Game.PlayerPed.Kill();

        }
    }
}
