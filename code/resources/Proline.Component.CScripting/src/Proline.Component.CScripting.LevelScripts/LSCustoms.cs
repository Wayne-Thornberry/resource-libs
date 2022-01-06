using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.Component.CScriptAreas.Core;
using Proline.CScripting.Framework;

namespace Proline.Classic.Scripts
{
    public class LSCustoms : ScriptInstance
    {
        public override async Task Execute(params object[] args)
        {
            var position = (Vector3)args[0];
            var blip = World.CreateBlip(position);
            while (ScriptAreaAPI.IsPointWithinActivationRange(position))
            {
                if (CitizenFX.Core.Game.PlayerPed.IsInVehicle())
                {
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to repair the car");
                    if(CitizenFX.Core.Game.IsControlJustPressed(0, Control.Context))
                    {
                        CitizenFX.Core.Game.PlayerPed.CurrentVehicle.Repair();
                    }
                }
                await BaseScript.Delay(0);
            }
            blip.Delete();
        }
    }
}
