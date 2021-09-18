using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Classic.Components;
using Proline.Engine.Scripting;

namespace Proline.Classic.LevelScripts
{
    public class DoSomething : LevelScript
    {
        public override async Task Execute(params object[] args)
        {
            ExampleAPI.AttachBlipsToGasStations();
            StartNewScript("MPStartup", null);
            //Globals.Set("EnableSomething", false);
            LogDebug("Testing on another script: " + "");//Globals.Get("EnableSomething"));
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
