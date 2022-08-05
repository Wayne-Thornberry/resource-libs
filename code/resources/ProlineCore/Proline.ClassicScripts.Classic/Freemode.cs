using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.MissionManager;
using Proline.ClassicOnline.MScripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class Freemode
    {
        private Vector3 _missionTruckingLocationStart;
        private Blip _missionTruckingBlip;
        private bool _hasHelpTextDisplayed;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("Freemode") > 1)
                return;


            _missionTruckingLocationStart = new Vector3(798.6685f, -2977.404f, 5.020939f);
            _missionTruckingBlip = World.CreateBlip(_missionTruckingLocationStart);
            _missionTruckingBlip.Sprite = BlipSprite.TowTruck;

            while (!token.IsCancellationRequested)
            {

                if (!MissionAPIs.GetMissionFlag())
                {
                    if (Game.PlayerPed.CurrentVehicle != null)
                    {
                        var currentVehicle = Game.PlayerPed.CurrentVehicle;
                        if (currentVehicle.Model == VehicleHash.Phantom || currentVehicle.Model == VehicleHash.Hauler)
                        {
                            if (!_hasHelpTextDisplayed)
                            { 
                                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ To Start Trucking");
                                API.PlaySoundFrontend(-1, "INFO", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                                _hasHelpTextDisplayed = true;
                            }
                            if (Game.IsControlJustPressed(0, Control.Context) &&
                                MScriptingAPI.GetInstanceCountOfScript("TruckingOnDemand") == 0)
                            {
                                MScriptingAPI.StartNewScript("TruckingOnDemand", currentVehicle.Handle);
                            }
                        }
                        else if (currentVehicle.Model == VehicleHash.Police || currentVehicle.Model == VehicleHash.Police2)
                        {
                            if (!_hasHelpTextDisplayed)
                            {
                                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ To Start Vigilante");
                                API.PlaySoundFrontend(-1, "INFO", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                                _hasHelpTextDisplayed = true;
                            }
                            if (Game.IsControlJustPressed(0, Control.Context) &&
                                MScriptingAPI.GetInstanceCountOfScript("VigilanteOnDemand") == 0)
                            {
                                MScriptingAPI.StartNewScript("VigilanteOnDemand", currentVehicle.Handle);
                            }
                        }
                    }
                    else
                    {
                        if(_hasHelpTextDisplayed)
                            _hasHelpTextDisplayed = false;
                    }


                    if (_missionTruckingBlip != null)
                    {
                        World.DrawMarker(MarkerType.VerticalCylinder, _missionTruckingLocationStart, new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0), new Vector3(1, 1, 1), System.Drawing.Color.FromArgb(150, 145, 0, 0));

                        if (World.GetDistance(Game.PlayerPed.Position, _missionTruckingLocationStart) <= 2f && MScriptingAPI.GetInstanceCountOfScript("Trucking") == 0)
                        {
                            MScriptingAPI.StartNewScript("Trucking");
                        }
                    }
                }

                await BaseScript.Delay(0);
            }
        }
    }
}
