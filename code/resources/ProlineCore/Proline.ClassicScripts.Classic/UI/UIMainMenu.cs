using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core.Scaleforms;
using Proline.ClassicOnline.MScripting;

namespace Proline.ClassicOnline.SClassic.UI
{
    public class UIMainMenu
    {
        public UIMainMenu()
        {
            MaxItems = 6;
            Title = "Proline";
            Subtitle = "A better online";
            Items = new[] { "Play", "Character Creator", "Character Select", "Options", "Credits", "Quit" };
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
        public int Stage { get; private set; }

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

        public async Task Execute(object[] args, CancellationToken token)
        {
            Scaleform = new CustomWarningScreen();
            await Scaleform.Load();
            Show();
            while (!token.IsCancellationRequested && Stage != -1)
            {
                API.DrawRect(0, 0, 2, 2, 0, 0, 0, 100);

                if (Game.IsControlJustPressed(0, Control.FrontendUp))
                {
                    SelectedIndex--;
                    if (SelectedIndex < 1) SelectedIndex = ActiveItems;
                    SelectedItem = Items[SelectedIndex - 1];
                    Game.PlaySound("NAV_UP_DOWN", "HUD_FREEMODE_SOUNDSET");
                    Scaleform.SetSelectedIndex(SelectedIndex);
                    MDebug.MDebugAPI.LogDebug(SelectedIndex);
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendDown))
                {
                    SelectedIndex++;
                    if (SelectedIndex > ActiveItems) SelectedIndex = 1;
                    SelectedItem = Items[SelectedIndex - 1];
                    Game.PlaySound("NAV_UP_DOWN", "HUD_FREEMODE_SOUNDSET");
                    Scaleform.SetSelectedIndex(SelectedIndex);
                    MDebug.MDebugAPI.LogDebug(SelectedIndex);
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendAccept))
                {
                    Game.PlaySound("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                    switch (SelectedIndex)
                    {
                        case 1:
                            { 
                                MScriptingAPI.StartNewScript("PlayerLoading");
                                Stage = -1;
                                Hide();
                            }
                            break;
                       case 2:
                            {
                                API.DoScreenFadeOut(100);
                                Screen.LoadingPrompt.Show("Loading Character Creator...");
                                API.SwitchInPlayer(Game.PlayerPed.Handle);
                                while (API.IsPlayerSwitchInProgress())
                                {
                                    await BaseScript.Delay(0);
                                }
                                MScriptingAPI.StartNewScript("CharacterCreator");
                                Stage = -1;
                                Hide();
                                Screen.LoadingPrompt.Hide();
                                API.DoScreenFadeIn(500);
                            }
                            break;
                    }
                    MDebug.MDebugAPI.LogDebug(SelectedIndex);
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                {
                    switch (Stage)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            Stage = 1;
                            break;
                    }
                    //Game.PlaySound("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                    //Stage = -1;
                    //Hide();
                }
                Scaleform.Render2D();
                await BaseScript.Delay(0);
            }
        }
    }
}