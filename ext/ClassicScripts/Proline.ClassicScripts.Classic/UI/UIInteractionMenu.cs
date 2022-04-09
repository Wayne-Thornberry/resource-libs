using Proline.EngineFramework.Scripting;
using Proline.ExampleClient.ComponentUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ExampleClient.UI
{
    public class UIInteractionMenu : DemandScript
    {
        public UIInteractionMenu(string name) : base(name)
        {
        }

        public override async Task Execute(object[] args, CancellationToken token)
        {
            var menu = new Menu("Test");
            MenuController.AddMenu(menu);
            var controller = new MenuController();
            menu.OpenMenu();
            while (!token.IsCancellationRequested)
            {
                await controller.Process();
                if (!menu.Visible)
                {
                    MarkScriptAsNoLongerNeeded();
                }
                await Delay(0);
            }
        }
    }
}
