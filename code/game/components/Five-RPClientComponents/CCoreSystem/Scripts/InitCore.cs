using CCoreSystem.Events;
using CCoreSystem.Internal;
using CCoreSystem.Internal.Events;
using CitizenFX.Core;
using Proline.ClassicOnline.EventQueue;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CCoreSystem.Scripts
{
    public class InitCore
    {
        public async Task Execute()
        {
            //PlayerJoinedEvent.SubscribeEvent();
            PlayerReadyEvent.SubscribeEvent();

            ComponentEvent.RegisterEvent(typeof(CTestEventRandomInput));

            TaskManager.StartNew(async () =>
            {
                var eventProcessor = new ComponentEventProcessor();
                while (true)
                {
                    //ScriptManager.CleanUpCompletedScripts();
                    await eventProcessor.ProcessQueue();
                    await BaseScript.Delay(1000);
                }
            });


        }
    }
}
