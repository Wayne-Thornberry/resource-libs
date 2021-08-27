extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;

using System.Linq;
using System.Threading.Tasks;
using Proline.Engine;
using Proline.Engine.Core.Scaleforms;

namespace Proline.Freemode.LevelScripts.UI
{
    public class UIMainMenu : LevelScript
    {
        public UIMainMenu()
        {
            MaxItems = 6;
            Title = "Proline";
            Subtitle = "A better online";
            Items = new[] { "Play", "Something", "Character Select", "Options", "Credits", "Quit" };
        }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Items { get; set; }
        public string SelectedItem { get; set; }
        public int MaxItems { get; }
        public int ActiveItems => Items.Count(item => !string.IsNullOrEmpty(item));
        public int SelectedIndex { get; set; }

        public CustomWarningScreen Scaleform { get; private set; }
        public bool IsVisible { get; set; }

        private void Show()
        {
            Scaleform.ShowCustomScreen(Title, Subtitle, Items);
            SelectedIndex = 1;
            SelectedItem = "";
        }

        private void Hide()
        {
            Title = "";
            Subtitle = "";
            Items = new[] { "", "", "", "", "", "" };
            Scaleform.HideCustomWarningScreen();
        }

        public override async Task Execute(params object[] args)
        {
            Scaleform = new CustomWarningScreen();
            await Scaleform.Load();
            Show();
            while (Stage != -1)
            {
                API.DrawRect(0, 0, 2, 2, 0, 0, 0, 100);

                if (Game.IsControlJustPressed(0, Control.FrontendUp))
                {
                    SelectedIndex--;
                    if (SelectedIndex < 1) SelectedIndex = ActiveItems;
                    SelectedItem = Items[SelectedIndex - 1];
                    Game.PlaySound("NAV_UP_DOWN", "HUD_FREEMODE_SOUNDSET");
                    Scaleform.SetSelectedIndex(SelectedIndex);
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendDown))
                {
                    SelectedIndex++;
                    if (SelectedIndex > ActiveItems) SelectedIndex = 1;
                    SelectedItem = Items[SelectedIndex - 1];
                    Game.PlaySound("NAV_UP_DOWN", "HUD_FREEMODE_SOUNDSET");
                    Scaleform.SetSelectedIndex(SelectedIndex);
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendAccept))
                {
                    Game.PlaySound("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                {
                    Game.PlaySound("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                    Stage = -1;
                    Hide();
                }
                Scaleform.Render2D();
                await BaseScript.Delay(0);
            }
        }
    }
}