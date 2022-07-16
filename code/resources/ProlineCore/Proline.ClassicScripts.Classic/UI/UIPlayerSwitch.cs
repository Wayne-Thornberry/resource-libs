using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core.Scaleforms;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MScripting;

namespace Proline.ClassicOnline.SClassic.UI
{
    public class UIPlayerSwitch
    {
        private PlayerSwitch plySwitch;
        private int sel;
        private int oldSel;

        public UIPlayerSwitch()
        {

        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (Game.IsControlJustPressed(0, Control.CharacterWheel))
                {
                    MDebugAPI.LogDebug("BUTTON PRESSED");
                    plySwitch = new PlayerSwitch();
                    await plySwitch.Load();
                    MDebugAPI.LogDebug("LOAD DONE");
                    if (plySwitch.IsLoaded)
                    {
                        plySwitch.SetSwitchVisible(true);
                        for (int i = 0; i < 4; i++)
                        {
                            // var txt = new PedHeadshot(Game.PlayerPed.Handle);
                            // await txt.LoadHeadShot();
                            plySwitch.SetSwitchSlot(i, 1, i, i == sel, "");
                        }
                    }
                    MDebugAPI.LogDebug("EFFECY");
                    Screen.Effects.Start(ScreenEffect.SwitchHudFranklinOut, 0, true);
                    Game.PlaySound("CHARACTER_SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                }
                else if (Game.IsControlJustReleased(0, Control.CharacterWheel))
                {
                    Screen.Effects.Stop(ScreenEffect.SwitchHudFranklinOut);
                    if (plySwitch != null)
                    {
                        if (plySwitch.IsLoaded)
                        {
                            plySwitch.SetSwitchVisible(false);
                        }
                    }

                    var playId = await plySwitch.GetSwitchSelected();
                    var position = Game.PlayerPed.Position;
                    //if (playId == oldSel) continue;
                    var rng = new Random();
                    var R = 1000;
                    var r = R * Math.Sqrt(rng.NextDouble());
                    var theta = rng.NextDouble() * 2 * Math.PI;
                    var x = (float)(position.X + r * Math.Cos(theta));
                    var y = (float)(position.Y + r * Math.Sin(theta));
                    MDebugAPI.LogDebug($"R: {R}, r {r}, Theta {theta}, CurrentPos {position} X {x}, Y {y}");

                    API.SwitchOutPlayer(Game.PlayerPed.Handle, 1, 1);
                    await BaseScript.Delay(3000);
                    Game.PlayerPed.Position = new Vector3(x, y, World.GetGroundHeight(new Vector2(x, y)));
                    //await BaseScript.Delay(3000); 
                    switch (playId)
                    {
                        case 0:
                            await Game.Player.ChangeModel(new Model(225514697));
                            break;
                        case 1:
                            await Game.Player.ChangeModel(new Model(-1686040670));
                            break;
                        case 2:
                            await Game.Player.ChangeModel(new Model(-1692214353));
                            break;
                        case 3: 
                            MScriptingAPI.StartNewScript("PlayerLoading");
                            break;
                    }
                    await BaseScript.Delay(3000);
                    API.SwitchInPlayer(Game.PlayerPed.Handle);
                    plySwitch = null;
                }

                if (Game.IsControlPressed(0, Control.CharacterWheel))
                {
                    Game.DisableControlThisFrame(0, Control.LookLeftRight);
                    Game.DisableControlThisFrame(0, Control.LookUpDown);
                    API.HideHudAndRadarThisFrame();
                    if (Game.GetControlNormal(0, Control.WeaponWheelLeftRight) > 0.5f)
                    {
                        sel = 1;
                    }
                    else if (Game.GetControlNormal(0, Control.WeaponWheelLeftRight) < -0.5f)
                    {
                        sel = 0;
                    }
                    else if (Game.GetControlNormal(0, Control.WeaponWheelUpDown) > 0.5f)
                    {
                        sel = 3;
                    }
                    else if (Game.GetControlNormal(0, Control.WeaponWheelUpDown) < -0.5f)
                    {
                        sel = 2;
                    }

                    if (sel != oldSel)
                    {
                        oldSel = sel;
                        plySwitch.SetPlayerSelected(sel);
                        Game.PlaySound("Apt_Style_Purchase", "DLC_APT_Apartment_SoundSet");
                    }
                    if (plySwitch.IsLoaded)
                        plySwitch.Render2DScreenSpace(new PointF(70f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f, Screen.Height - 150f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f), new PointF(150f, 150f));
                    //new PointF(50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f,
                    //50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 50f)
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
