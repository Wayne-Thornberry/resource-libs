using System.Threading;
using System.Threading.Tasks;
using Proline.EngineFramework.Scripting;

namespace Proline.ExampleClient.Scripts.Object
{
    public class ObCCTVCamera : DemandScript
    {
        public ObCCTVCamera(string name) : base(name)
        {

        }

        public override async Task Execute(object[] args, CancellationToken token)
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