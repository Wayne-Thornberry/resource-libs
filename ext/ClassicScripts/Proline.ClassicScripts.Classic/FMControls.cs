using CitizenFX.Core;
using Proline.EngineFramework.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ExampleClient
{
    public class FMControls : DemandScript
    {
        public FMControls(string name) : base(name)
        {
        }

        public override async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if(Game.IsControlJustReleased(0, Control.InteractionMenu))
                {
                    StartNewScript("UIInteractionMenu");
                }
                await Delay(0);
            }
        }
    }
}
