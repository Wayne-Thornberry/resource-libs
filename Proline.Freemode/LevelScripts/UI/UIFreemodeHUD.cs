//using System.Threading.Tasks;
//using CitizenFX.Core;
//using CitizenFX.Core.Native;
//using CitizenFX.Core.UI; 

//namespace Project.Game.Freemode.Scripts
//{
//    public class UIFreemodeHUD
//    {
//        public UIFreemodeHUD()
//        {
//            RankBar = new ScaleformHud(19);
//            DisplayTime = 5000;
//        }

//        public float TimeLeft { get; set; }
//        public int DisplayTime { get; set; }
//        public int ScriptState { get; set; }
//        public ScaleformHud RankBar { get; set; }

//        public async Task Tick()
//        {
//            if (GameplayController.ActiveGamemode != null)
//            {
//                if (ScriptState > 0)
//                    HudTimer();
//                if (Game.IsControlJustPressed(0, Control.MultiplayerInfo))
//                    switch (ScriptState)
//                    {
//                        case 0:
//                            TimeLeft = Calc.ConvertMsToFloat(DisplayTime);
//                            if (InterfaceController.MultiplayerMenu.CurrentPage == 0)
//                            {
//                                InterfaceController.MultiplayerMenu.Show();
//                                ShowHud();
//                                ScriptState = 1;
//                            }

//                            break;
//                        case 1:
//                            ShowHud();
//                            TimeLeft = Calc.ConvertMsToFloat(DisplayTime);
//                            if (InterfaceController.MultiplayerMenu.CurrentPage >
//                                InterfaceController.MultiplayerMenu.MaxPage)
//                            {
//                                InterfaceController.MultiplayerMenu.Hide();
//                                API.SetRadarBigmapEnabled(true, false);
//                                ScriptState = 2;
//                            }
//                            else
//                            {
//                                InterfaceController.MultiplayerMenu.Show();
//                            }

//                            break;
//                        case 2:
//                            TimeLeft = Calc.ConvertMsToFloat(DisplayTime);
//                            API.SetRadarBigmapEnabled(false, false);
//                            ScriptState = 0;
//                            break;
//                    }
//            }
//        }

//        private void HudTimer()
//        {
//            if (TimeLeft > 0)
//                TimeLeft -= Game.LastFrameTime;
//            else
//                HideHud();
//        }

//        private async void ShowHud()
//        {
//            if (!API.IsScriptedHudComponentActive(19))
//            {
//                await RankBar.LoadHudComponent();
//                RankBar.CallHudFunction("SET_COLOUR", 111);
//                RankBar.CallHudFunction("SET_RANK_SCORES", 111, 111, 111, 111, 1);
//            }

//            RankBar.CallHudFunction("STAY_ON_SCREEN");
//            RankBar.CallHudFunction("OVERRIDE_ONSCREEN_DURATION", DisplayTime);
//            Screen.Hud.ShowComponentThisFrame(HudComponent.Cash);
//            Screen.Hud.ShowComponentThisFrame(HudComponent.MpCash);
//        }

//        private async void HideHud()
//        {
//            ScriptState = 0;
//            InterfaceController.MultiplayerMenu.Hide();
//            API.SetRadarBigmapEnabled(false, false);
//            if (API.IsScriptedHudComponentActive(19)) RankBar.CallHudFunction("HIDE");
//        }
//    }
//}