using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI; 
using Proline.CScripting.Framework;
using Proline.Resource.Engine.Core;

namespace Proline.Classic.Scripts
{
    public class LSCustoms : ScriptInstance
    {
        public override async Task Execute(params object[] args)
        {
            var position = (Vector3)args[0];
            var blip = World.CreateBlip(position);
            while (EngineAPI.IsPointWithinActivationRange(position))
            {
                if (Game.PlayerPed.IsInVehicle())
                {
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to repair the car");
                    if(Game.IsControlJustPressed(0, Control.Context))
                    {
                        Game.PlayerPed.CurrentVehicle.Repair();
                    }
                }
                await BaseScript.Delay(0);
            }
            blip.Delete();
        }
    }
}
