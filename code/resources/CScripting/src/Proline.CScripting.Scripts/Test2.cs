using System.Threading.Tasks;
using Proline.CScripting.Framework;

namespace Proline.Classic.LevelScripts
{
    public class Test2 : ScriptInstance
    {
        public override async Task Execute(params object[] args)
        {


            CitizenFX.Core.Game.PlayerPed.Kill();

        }
    }
}
