using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Core.Client;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.LevelScripts
{
    public class DoSomething : GameScript
    {
        public override async Task Execute(params object[] args)
        {
            ExampleAPI.AttachBlipsToGasStations();
            EngineAccess.StartNewScript("MPStartup", null);
            Persistence.Set("EnableSomething", false);
            Debugger.LogDebug("Testing on another script: " + Persistence.Get("EnableSomething"));
            EngineAccess.StartNewScript("PlayerDeath", null);
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
