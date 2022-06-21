using CitizenFX.Core;
using Proline.Modularization.Core;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MGame
{
    public class MGameContext : ModuleScript
    {
        public MGameContext(Assembly source) : base(source)
        {
        }

        public override async Task OnLoad()
        {
            //EventManager.InvokeEventV2("playerJoinedSessionHandler");
            //EventManager.InvokeEventV2("playerJoinedSession");

        }
    }
}
