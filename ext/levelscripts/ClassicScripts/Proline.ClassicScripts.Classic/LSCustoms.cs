using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.MScripting;

namespace Proline.ClassicOnline.LevelScripts
{
    public class LSCustoms 
    {
        public LSCustoms()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            var position = (Vector3)args[0];
            var blip = World.CreateBlip(position);
            while (!token.IsCancellationRequested)
            {
                if (IsPointWithinActivationRange(position))
                {
                    if (Game.PlayerPed.IsInVehicle())
                    {
                        Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to repair the car");
                        if (Game.IsControlJustPressed(0, Control.Context))
                        {
                            Game.PlayerPed.CurrentVehicle.Repair();
                        }
                    }
                }
                else
                {
                    MScriptingAPI.MarkScriptAsNoLongerNeeded();
                } 
                await BaseScript.Delay(0);
            }
            blip.Delete();
        }

        private bool IsPointWithinActivationRange(Vector3 position)
        {
            return World.GetDistance(Game.PlayerPed.Position, position) < 50f;
        }
    }
}
