using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.ClassicOnline.MDebug;

namespace Proline.ClassicOnline.LevelScripts
{
    public class DoSomething 
    {
        public DoSomething()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            //ComponentAPI.AttachBlipsToGasStations();
           // EngineAPI.Script.StartNewScript("MPStartup", null);
            //Globals.Set("EnableSomething", false);
            DebugConsole.LogDebug("Testing on another script: " + "");//Globals.Get("EnableSomething"));
           // EngineAPI.Script.StartNewScript("PlayerDeath", null);
            while (true)
            {
                if(Game.IsControlJustPressed(0, Control.VehicleExit))
                {
                    //ExampleAPI.SetPlayerAsPartOfPoliceGroup();
                    //ComponentAPI.UnlockNeareastVehicle();
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
