using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.MissionManager;
using Proline.ClassicOnline.MScreen.Minimap;

namespace Proline.ClassicOnline.SClassic.Mission
{
    public class FMVechicleExporter
    {
        private Vector3 _deliveryPoint;
        private Blip _blip;
        private Vehicle _targetVehicle;
        private bool _oneTime;
        private int _ticks;

        public FMVechicleExporter()
        {
            _deliveryPoint = new Vector3(1204.67f, -3116.19f, 5.12f);
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            Vector3 dir = new Vector3(0, 0, 0);
            Vector3 rot = new Vector3(0, 0, 0);
            Vector3 scale = new Vector3(5, 5, 5);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(125, 255, 0, 0);
            while (!token.IsCancellationRequested)
            {

                if (Game.PlayerPed.IsInVehicle() && !MissionAPIs.GetMissionFlag() && !MGame.MGameAPI.IsInPersonalVehicle() && !MissionAPIs.IsInMissionVehicle())
                {
                    _targetVehicle = Game.PlayerPed.CurrentVehicle;
                    if (!_oneTime)
                    {
                        _blip = World.CreateBlip(_deliveryPoint);
                        _blip.IsShortRange = false;
                        _blip.Sprite = BlipSprite.PersonalVehicleCar;
                        _blip.IsFlashing = true;
                        Screen.DisplayHelpTextThisFrame("Deliver this vehicle to the docks to earn some money");
                        API.PlaySoundFrontend(-1, "INFO", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                        MinimapAPI.FlashBlip(_blip);
                        _oneTime = true;
                    }

                    World.DrawMarker(MarkerType.VerticalCylinder, _deliveryPoint, dir, rot, scale, color);

                    if (World.GetDistance(Game.PlayerPed.Position, _deliveryPoint) < 2f && _targetVehicle.IsStopped)
                    {
                        if (Game.PlayerPed.IsInVehicle())
                        {
                            Game.PlayerPed.Task.LeaveVehicle();
                            _targetVehicle.LockStatus = VehicleLockStatus.Locked;
                        }
                    }
                }
                else
                {
                    if (_targetVehicle != null)
                    {
                        if (World.GetDistance(Game.PlayerPed.Position, _deliveryPoint) < 25f)
                        {
                            Screen.DisplayHelpTextThisFrame("Please leave the area");
                        }
                        else
                        {
                            if (World.GetDistance(_targetVehicle.Position, _deliveryPoint) < 2f)
                            {
                                _targetVehicle.Delete();
                                _targetVehicle = null;
                                var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                                var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                                stat.SetValue(stat.GetValue() + 1500);
                            }
                            else
                            {
                                if (_blip != null)
                                {
                                    _blip.Delete();
                                    _blip = null;
                                }
                                _targetVehicle = null;
                                _oneTime = false;
                            }
                            //stat2.SetValue(stat2.GetValue() + 1500);
                        }
                    }
                    else
                    {
                        if (_blip != null)
                        {
                            _blip.Delete();
                            _blip = null;
                        }
                        _targetVehicle = null;
                        _oneTime = false;
                    }
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
