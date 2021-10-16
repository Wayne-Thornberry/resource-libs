using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.CScripting.Framework;

namespace Proline.Classic.LevelScripts
{
    public class LSCustoms : ScriptInstance
    {
        public override async Task Execute(params object[] args)
        {
            var position = (Vector3)args[0];
            var blip = World.CreateBlip(position);
            while (true)
            {
                if (CitizenFX.Core.Game.PlayerPed.IsInVehicle())
                {
                    Screen.DisplayHelpTextThisFrame("Press ~CONTEXT_E~ to repair the car");
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
