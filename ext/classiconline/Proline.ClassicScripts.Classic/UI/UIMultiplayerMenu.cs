using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.CFXExtended.Core.Scaleforms;

namespace Proline.ClassicOnline.SClassic.UI
{
    //public struct MultiplayerMenuSlot
    //{
    //    public string Username;
    //    public string CrewTag;
    //    public int Rank;
    //    public int Points;

    //    public MultiplayerMenuSlot(string name, string tag, int rank, int points)
    //    {
    //        Username = name;
    //        CrewTag = tag;
    //        Rank = rank;
    //        Points = points;
    //    }
    //}

    public class UIMultiplayerMenu
    {
        private int tick;
        private MPMMFreemodeMenu freemodeMenu;

        public PointF Position { get; set; }
        public PointF Size { get; set; }
        public Player[] Players = new Player[32];
        public int MaxPage => (int)Math.Ceiling(Players.Count() / 16.0);
        public int CurrentPage { get; private set; }
        public string Title => $"PH:RP (Public, {Players.Count()})";
        public string Subtitle => $"({CurrentPage}/{MaxPage})";

        public Scaleform Scaleform { get; }
        public bool DisplayScaleform { get; set; }

        public async Task Execute(object[] args, CancellationToken token)
        {

            Position = new PointF(50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f,
                50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 50f);
            Size = new PointF(360, 434);

            freemodeMenu = new MPMMFreemodeMenu();
            await freemodeMenu.Load();
            tick = 0;
            Show();
            while (CurrentPage < MaxPage && tick < 5000)
            {
                if (Game.IsControlJustPressed(0, Control.MultiplayerInfo) && CurrentPage > 0)
                {
                    tick = 0;
                    NextPage();
                }
                tick++;
                freemodeMenu.Render2DScreenSpace(Position, Size);
                await BaseScript.Delay(0);
            }
            Hide();
        }

        public void Show()
        {
            CurrentPage = 1;
            RefreshDisplay();
        }

        public void Hide()
        {
            EmptyCurrentPage();
            CurrentPage = 0;
        }


        private void PopulateCurrentPage()
        {
            for (var i = 0; i < 16; i++)
            {
                try
                {
                    var player = Players[i];
                    freemodeMenu.SetDataSlot(i, 0, player.Name, 111, "65", "", 0, "..+STAFF", 5, "", "",
                        'F');
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void RefreshDisplay()
        {
            Position = new PointF(50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f,
                50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 50f);
            //Players = ();
            EmptyCurrentPage();
            PopulateCurrentPage();
            DisplayCurrentPage();
        }

        private void EmptyCurrentPage()
        {
            for (var i = 0; i < 16; i++) freemodeMenu.SetDataSlotEmpty(i, 0);
        }

        private void DisplayCurrentPage()
        {
            freemodeMenu.SetTitle(Title, Subtitle, 0);
            freemodeMenu.DisplayView();
        }

        private void NextPage()
        {
            CurrentPage++;
            RefreshDisplay();
        }
    }
}