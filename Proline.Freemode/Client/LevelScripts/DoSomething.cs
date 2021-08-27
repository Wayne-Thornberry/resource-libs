extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;

using Proline.Engine;
using System.Threading.Tasks;
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
