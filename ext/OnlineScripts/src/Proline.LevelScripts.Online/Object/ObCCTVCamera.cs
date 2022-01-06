using System.Threading.Tasks;
using Proline.CScripting.Framework;

namespace Proline.Classic.Scripts.Object
{
    public class ObCCTVCamera : ScriptInstance
    {
        public ObCCTVCamera()
        {

        }

        public override async Task Execute(params object[] args)
        {
            //if (LocalEntity.Exists())
            //{
            //    //World.DrawLine(_object.Position, Game.PlayerPed.Position,Color.FromArgb(255,255,255));
            //    if (LocalEntity.HasBeenDamagedByAnyWeapon())
            //    {
            //        Screen.DisplayHelpTextThisFrame("Camera has been destroyed");
            //        LocalEntity.AttachedBlip.Delete();
            //        TerminateScript();
            //    }
            //}
            //else
            //{
            //    TerminateScript();
            //}
        }
    }
}