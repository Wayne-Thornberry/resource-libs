extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;
using Proline.Core;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.LevelScripts
{
    public class LSCustoms : LevelScript
    {
        public override async Task Execute(params object[] args)
        {
            var position = (Vector3)args[0];
            var blip = World.CreateBlip(position);
            while (ExampleAPI.IsInActivationRange(position))
            {
                if (Game.PlayerPed.IsInVehicle())
                {
                    Screen.DisplayHelpTextThisFrame("Press ~CONTEXT_E~ to repair the car");
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
