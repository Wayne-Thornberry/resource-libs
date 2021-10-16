﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core.Scaleforms;
using Proline.CScripting.Framework;

namespace Proline.Classic.LevelScripts.UI
{
    public class UIPlayerSwitch : ScriptInstance
    {
        private PlayerSwitch plySwitch;
        private int sel;
        private int oldSel;

        public UIPlayerSwitch()
        {

        }

        public override async Task Execute(params object[] args)
        {
            while (true)
            {
                if (CitizenFX.Core.Game.IsControlJustPressed(0, Control.CharacterWheel))
                {
                    LogDebug("BUTTON PRESSED");
                    plySwitch = new PlayerSwitch();
                    await plySwitch.Load();
                    LogDebug("LOAD DONE");
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
                    LogDebug("EFFECY");
                    Screen.Effects.Start(ScreenEffect.SwitchHudFranklinOut, 0, true);
                    CitizenFX.Core.Game.PlaySound("CHARACTER_SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                }
                else if (CitizenFX.Core.Game.IsControlJustReleased(0, Control.CharacterWheel))
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
                    //if (playId == oldSel) continue;
                    API.SwitchOutPlayer(CitizenFX.Core.Game.PlayerPed.Handle, 1, 1);
                    await BaseScript.Delay(3000);
                    CitizenFX.Core.Game.PlayerPed.Position = new Vector3(new Random().Next(-4500, 4500), new Random().Next(-7500, 5500), World.GetGroundHeight(new Vector2(CitizenFX.Core.Game.PlayerPed.Position.X,
                         CitizenFX.Core.Game.PlayerPed.Position.Y)));
                    await BaseScript.Delay(3000);
                    //Game.PlayerPed.Position = World.GetNextPositionOnSidewalk(new Vector3(Game.PlayerPed.Position.X + 20f,
                    //    Game.PlayerPed.Position.Y + 20f,
                    //   World.GetGroundHeight(new Vector2(Game.PlayerPed.Position.X + 20f,
                    //      Game.PlayerPed.Position.Y + 20f))));
                    await BaseScript.Delay(3000);
                    switch (playId)
                    {
                        case 0:
                            await CitizenFX.Core.Game.Player.ChangeModel(new Model(225514697));
                            break;
                        case 1:
                            await CitizenFX.Core.Game.Player.ChangeModel(new Model(-1686040670));
                            break;
                        case 2:
                            await CitizenFX.Core.Game.Player.ChangeModel(new Model(-1692214353));
                            break;
                        case 3:
                            await CitizenFX.Core.Game.Player.ChangeModel(new Model(1885233650));
                            break;
                    }
                    await BaseScript.Delay(3000);
                    API.SwitchInPlayer(CitizenFX.Core.Game.PlayerPed.Handle);
                    plySwitch = null;
                }

                if (CitizenFX.Core.Game.IsControlPressed(0, Control.CharacterWheel))
                {
                    CitizenFX.Core.Game.DisableControlThisFrame(0, Control.LookLeftRight);
                    CitizenFX.Core.Game.DisableControlThisFrame(0, Control.LookUpDown);
                    API.HideHudAndRadarThisFrame();
                    if (CitizenFX.Core.Game.GetControlNormal(0, Control.WeaponWheelLeftRight) > 0.5f)
                    {
                        sel = 1;
                    }
                    else if (CitizenFX.Core.Game.GetControlNormal(0, Control.WeaponWheelLeftRight) < -0.5f)
                    {
                        sel = 0;
                    }
                    else if (CitizenFX.Core.Game.GetControlNormal(0, Control.WeaponWheelUpDown) > 0.5f)
                    {
                        sel = 3;
                    }
                    else if (CitizenFX.Core.Game.GetControlNormal(0, Control.WeaponWheelUpDown) < -0.5f)
                    {
                        sel = 2;
                    }

                    if (sel != oldSel)
                    {
                        oldSel = sel;
                        plySwitch.SetPlayerSelected(sel);
                        CitizenFX.Core.Game.PlaySound("Apt_Style_Purchase", "DLC_APT_Apartment_SoundSet");
                    }
                    if(plySwitch.IsLoaded)
                        plySwitch.Render2DScreenSpace(new PointF(70f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f, (Screen.Height - 150f) - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f), new PointF(150f, 150f));
                    //new PointF(50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 78f,
                    //50f - (API.GetSafeZoneSize() - 0.89f) / 0.11f * 50f)
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
