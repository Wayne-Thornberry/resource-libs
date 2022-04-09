using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.EngineFramework.Scripting;


namespace Proline.ExampleClient.Scripts
{
    public class DoSomething : DemandScript
    {
        public DoSomething(string name) : base(name)
        {
        }

        public override async Task Execute(object[] args, CancellationToken token)
        {
            //ComponentAPI.AttachBlipsToGasStations();
           // EngineAPI.StartNewScript("MPStartup", null);
            //Globals.Set("EnableSomething", false);
            LogDebug("Testing on another script: " + "");//Globals.Get("EnableSomething"));
           // EngineAPI.StartNewScript("PlayerDeath", null);
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
