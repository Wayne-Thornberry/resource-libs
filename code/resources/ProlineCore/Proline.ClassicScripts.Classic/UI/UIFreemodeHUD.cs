using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core.Scaleforms;

namespace Proline.ClassicOnline.SClassic.UI
{
    public class MultiplayerMenuSlot
    {
        public string Username;
        public string CrewTag;
        public int Rank;
        public int Points;

        public MultiplayerMenuSlot(string name, string tag, int rank, int points)
        {
            Username = name;
            CrewTag = tag;
            Rank = rank;
            Points = points;
        }
    } 

    public class UIFreemodeHUD
    {
        public UIFreemodeHUD()
        {
            RankBar = new ScaleformHud(19);
            DisplayTime = 5000;
            Scaleform = new MPMMFreemodeMenu();
            Position = new PointF(50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f, 50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 50f);
            Size = new PointF(360, 434);
            DisplayScaleform = true;
            EnableControls = true;
        }

        public float TimeLeft { get; set; }
        public int DisplayTime { get; set; }
        public int ScriptState { get; set; }
        public ScaleformHud RankBar { get; set; }
         

        public MPMMFreemodeMenu Scaleform { get; }
        public bool DisplayScaleform { get; set; }
        public PointF Position { get; set; }
        public PointF Size { get; }
        public PlayerList Players { get; set; }
        public int MaxPage => (int)Math.Ceiling(3 / 16.0);
        public int CurrentPage { get; private set; }
        public string Title => $"PH:RP (Public, {3})";
        public string Subtitle => $"({CurrentPage}/{MaxPage})";
        public bool EnableControls { get; set; } 


        private void PopulateCurrentPage()
        {
            for (var i = 0; i < 16; i++)
            {
                try
                {
                    var player = Players[i];
                    Scaleform.SetDataSlot(i, 0, player.Name, 111, "65", "", 0, $"..+STAFF", 5, "", "", 'F');
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void RefreshDisplay()
        {
            Position = new PointF(50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f, 50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 50f);
            EmptyCurrentPage();
            PopulateCurrentPage();
            DisplayCurrentPage();
        }

        private void EmptyCurrentPage()
        {
            for (var i = 0; i < 16; i++) Scaleform.SetDataSlot(i);
        }

        private void DisplayCurrentPage()
        {
            Scaleform.SetTitle(Title, Subtitle, 0);
            Scaleform.DisplayView();
        }

        private void NextPage()
        {
            CurrentPage++;
            RefreshDisplay(); 
        }

        public void Show()
        { 
            CurrentPage = 1;
            DisplayScaleform = true;
            RefreshDisplay(); 
        }

        public void Hide()
        {
            CurrentPage = 0;
            DisplayScaleform = false;
            EmptyCurrentPage(); 
        }

        public Vector3 ConvertLocalToWorld(Vector3 origin, Vector3 rotation, Vector3 position)
        {
            position = new Vector3(position.X + origin.X, position.Y + origin.Y, position.Z);
            var radi = Math.PI / 180 * rotation.Z;
            var x = (float)(Math.Cos(radi) * (position.X - origin.X) - Math.Sin(radi) * (position.Y - origin.Y) +
                             origin.X);
            var y = (float)(Math.Sin(radi) * (position.X - origin.X) + Math.Cos(radi) * (position.Y - origin.Y) +
                             origin.Y);
            return new Vector3(x, y, origin.Z);
        }

        public float ConvertMsToFloat(int time)
        {
            return (float)(time * 0.001);
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (ScriptState > 0)
                    HudTimer();
                Scaleform.Render2DScreenSpace(Position, Size);
                if (Game.IsControlJustPressed(0, Control.MultiplayerInfo))
                {   
                    switch (ScriptState)
                    {
                        case 0:
                            TimeLeft = ConvertMsToFloat(DisplayTime);
                            if (CurrentPage == 0)
                            {
                                ShowHud();
                                ScriptState = 1;
                            }

                            break;
                        case 1:
                            ShowHud();
                            TimeLeft = ConvertMsToFloat(DisplayTime);
                            if (CurrentPage >
                                MaxPage)
                            {
                                Hide();
                                API.SetRadarBigmapEnabled(true, false);
                                ScriptState = 2;
                            }
                            else
                            {
                                Show();
                            }

                            break;
                        case 2:
                            TimeLeft = ConvertMsToFloat(DisplayTime);
                            API.SetRadarBigmapEnabled(false, false);
                            ScriptState = 0;
                            break;
                    }
                }

                await BaseScript.Delay(0);
            }
        }


        private void HudTimer()
        {
            if (TimeLeft > 0)
                TimeLeft -= Game.LastFrameTime;
            else
                HideHud();
        }

        private async void ShowHud()
        {
            if (!API.IsScriptedHudComponentActive(19))
            {
                await RankBar.Load();
                RankBar.CallFunction("SET_COLOUR", 111);
                RankBar.CallFunction("SET_RANK_SCORES", 111, 111, 111, 111, 1);
            }

            RankBar.CallFunction("STAY_ON_SCREEN");
            RankBar.CallFunction("OVERRIDE_ONSCREEN_DURATION", DisplayTime);
            Screen.Hud.ShowComponentThisFrame(HudComponent.Cash);
            Screen.Hud.ShowComponentThisFrame(HudComponent.MpCash);
        }

        private async void HideHud()
        {
            ScriptState = 0;
            Hide();
            API.SetRadarBigmapEnabled(false, false);
            if (API.IsScriptedHudComponentActive(19)) RankBar.CallFunction("HIDE");
        }
    }
}