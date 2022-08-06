using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MBrain;
using Proline.ClassicOnline.MissionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class TruckingOnDemand
    {
        private Vector3 _truckSpawnLoc;
        private Vector3 _trailerSpawnLoc;
        private Vehicle _truck;
        private Vehicle _trailer;
        private Vector3 _deliveryLoc;
        private int _payout;
        private Blip _deliveryBlip;
        private float _closestDistance;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("TruckingOnDemand") > 1)
                return;
            if (!MissionAPIs.BeginMission())
                return;

            _closestDistance = 99999f;

            _truckSpawnLoc = new Vector3(829.9249f, -2950.439f, 4.902536f);
            _trailerSpawnLoc = new Vector3(865.3315f, -2986.426f, 4.900764f);
            _truck = (Vehicle) Entity.FromHandle(int.Parse(args[0].ToString()));


            var handles = MBrainAPI.GetEntityHandlesByTypes(GScripting.EntityType.VEHICLE);

            foreach (var item in handles)
            {
                var entity = Entity.FromHandle(item);
                var distance = World.GetDistance(entity.Position, Game.PlayerPed.Position);
                if (distance < _closestDistance &&  IsValidModel(entity.Model))
                {
                    _trailer = (Vehicle) entity;
                    _closestDistance = distance;
                }
            }

            if (_trailer == null)
                return;

            //_trailer = await World.CreateVehicle(VehicleHash.Trailers2, _trailerSpawnLoc, 270);
            _deliveryLoc = new Vector3(-430.9589f, -2713.246f, 5.000218f);
            _payout = (int)(10.0f * World.GetDistance(_trailer.Position, _deliveryLoc));

            _truck.AttachBlip();
            _trailer.AttachBlip();
            _deliveryBlip = World.CreateBlip(_deliveryLoc);


            MissionAPIs.TrackPoolObjectForMission(_truck);
            MissionAPIs.TrackPoolObjectForMission(_trailer);
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

        private bool IsValidModel(Model model)
        { 
            return model == VehicleHash.Trailers || model == VehicleHash.Trailers2 || model == VehicleHash.Trailers3 || model == VehicleHash.Trailers4;
        }
    }
}
