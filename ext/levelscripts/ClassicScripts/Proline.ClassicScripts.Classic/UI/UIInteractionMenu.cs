using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.ClassicOnline.MScreen;
using Proline.ClassicOnline.MScripting;
using Proline.ClassicOnline.MScreen.MenuItems;

namespace Proline.ClassicOnline.LevelScripts.UI
{
    public class UIInteractionMenu 
    {
        public UIInteractionMenu()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
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
                    MScriptingAPI.MarkScriptAsNoLongerNeeded();
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
