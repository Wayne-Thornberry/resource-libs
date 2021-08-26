
using CitizenFX.Core.Native;
using Proline.Freemode;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Core;

namespace Proline.Freemode.LevelScripts
{
    public class DoSomething : LevelScript
    {
        public override async Task Execute(params object[] args)
        {
            ExampleAPI.AttachBlipsToGasStations();
            StartNewScript("MPStartup", null);
            Persistence.Set("EnableSomething", false);
            LogDebug("Testing on another script: " + Persistence.Get("EnableSomething"));
            StartNewScript("PlayerDeath", null);
            while (true)
            {
                if(Game.IsControlJustPressed(0, Control.VehicleExit))
                {
                    //ExampleAPI.SetPlayerAsPartOfPoliceGroup();
                    ExampleAPI.UnlockNeareastVehicle();
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
