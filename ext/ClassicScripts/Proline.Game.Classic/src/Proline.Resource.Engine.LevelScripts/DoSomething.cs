using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.CScripting.Framework;
using Proline.Resource.Engine.Core;

namespace Proline.Classic.Scripts
{
    public class DoSomething : ScriptInstance
    {
        public override async Task Execute(params object[] args)
        {
            //ComponentAPI.AttachBlipsToGasStations();
            EngineAPI.StartNewScript("MPStartup", null);
            //Globals.Set("EnableSomething", false);
            LogDebug("Testing on another script: " + "");//Globals.Get("EnableSomething"));
            EngineAPI.StartNewScript("PlayerDeath", null);
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
