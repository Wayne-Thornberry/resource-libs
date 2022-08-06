using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MissionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class Trucking
    {
        private Vector3 _truckSpawnLoc;
        private Vector3 _trailerSpawnLoc;
        private Vehicle _truck;
        private Vehicle _trailer;
        private Vector3 _deliveryLoc;
        private Blip _deliveryBlip;
        private int _payout;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("Trucking") > 1)
                return;

            if (!MissionAPIs.BeginMission())
                return;

            _truckSpawnLoc = new Vector3(829.9249f, -2950.439f, 4.902536f);
            _trailerSpawnLoc = new Vector3(865.3315f, -2986.426f, 4.900764f);
            _truck = await World.CreateVehicle(VehicleHash.Phantom, _truckSpawnLoc, 180);
            _trailer = await World.CreateVehicle(VehicleHash.Trailers2, _trailerSpawnLoc, 270);

            MissionAPIs.TrackPoolObjectForMission(_truck);
            MissionAPIs.TrackPoolObjectForMission(_trailer);

            _deliveryLoc = new Vector3(-430.9589f, -2713.246f, 5.000218f);
            _payout = (int) (10.0f * World.GetDistance(_trailer.Position, _deliveryLoc));

            _truck.AttachBlip();
            _trailer.AttachBlip();
            _deliveryBlip = World.CreateBlip(_deliveryLoc);
            MissionAPIs.TrackPoolObjectForMission(_deliveryBlip);

            while (!token.IsCancellationRequested)
            {
                if (_truck.IsDead || _trailer.IsDead)
                    break;

                if (_truck.IsAttachedTo(_trailer))
                {
                    _truck.AttachedBlip.Alpha = 0;
                    if (Game.PlayerPed.CurrentVehicle == _truck)
                    {
                        _truck.AttachedBlip.Alpha = 0;
                        _deliveryBlip.Alpha = 255;
                    }
                }
                else
                {
                    if(World.GetDistance(_trailer.Position, _deliveryLoc) < 10f)
                    {
                        _trailer.Delete();
                        MGame.MGameAPI.AddValueToBankBalance(_payout);
                        break;
                    }
                }

                await BaseScript.Delay(0);
            }
            MissionAPIs.EndMission();
        }
    }
}
