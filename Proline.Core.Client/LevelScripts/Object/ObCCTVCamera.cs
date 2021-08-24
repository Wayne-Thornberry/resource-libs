using System.Threading.Tasks;
using Proline.Engine;

namespace Proline.Core.Client.LevelScripts.Object
{
    public class ObCCTVCamera : GameScript
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