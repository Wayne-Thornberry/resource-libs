using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.MBrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class VigilanteOnDemand
    {
        private Vector3 _truckSpawnLoc;
        private Vector3 _trailerSpawnLoc;
        private Vehicle _policeVehicle;
        private Vehicle _target;
        private Vector3 _deliveryLoc;
        private int _payout;
        private Blip _deliveryBlip;
        private float _closestDistance;
        private Ped[] _targetPeds;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("VigilanteOnDemand") > 1)
                return;

            _closestDistance = 99999f;

            //_truckSpawnLoc = new Vector3(829.9249f, -2950.439f, 4.902536f);
            //_trailerSpawnLoc = new Vector3(865.3315f, -2986.426f, 4.900764f);
            _policeVehicle = (Vehicle) Entity.FromHandle(int.Parse(args[0].ToString()));

            var handles = MBrainAPI.GetEntityHandlesByTypes(GScripting.EntityType.VEHICLE);

            foreach (var item in handles)
            {
                var entity = (Vehicle) Entity.FromHandle(item);
                var distance = World.GetDistance(entity.Position, Game.PlayerPed.Position);
                if (distance < _closestDistance &&  IsValidModel(entity.Model) && entity != _policeVehicle)
                {
                    MDebug.MDebugAPI.LogDebug("Found a vehicle");
                    _target = (Vehicle) entity;
                    _closestDistance = distance;
                }
            }

            if (_target == null)
                return;

            //_trailer = await World.CreateVehicle(VehicleHash.Trailers2, _trailerSpawnLoc, 270);
            //_deliveryLoc = new Vector3(-430.9589f, -2713.246f, 5.000218f);
            _policeVehicle.AttachBlip();
            _target.AttachBlip();
            var targets = new List<Ped>(_target.Passengers);
            targets.Add(_target.Driver);
            _targetPeds = targets.ToArray(); 
            _payout = 1000 * _targetPeds.Length;//(int)(10.0f * World.GetDistance(_target.Position, _deliveryLoc));


            Screen.ShowNotification("Vigilante Started");

            foreach (var item in _targetPeds)
            {
                item.AttachBlip();
                item.Task.ShootAt(Game.PlayerPed);
            } 

            while (!token.IsCancellationRequested)
            {
                Game.Player.WantedLevel = 0;
                if (Game.PlayerPed.IsDead || _policeVehicle.IsDead || IsAllTargetsDead())
                    break;

                foreach (var item in _targetPeds)
                { 
                    if(item.CurrentVehicle != null)
                    {
                        item.AttachedBlip.Alpha = 0;
                        if (item.CurrentVehicle.AttachedBlip == null)
                            item.AttachBlip();
                        item.CurrentVehicle.AttachedBlip.Alpha = 255;
                    }
                    else
                    { 
                        item.AttachedBlip.Alpha = 255;
                    }
                } 

                if (Game.PlayerPed.IsInVehicle())
                {
                    var vehicle = Game.PlayerPed.CurrentVehicle;
                    if(vehicle != _policeVehicle)
                    { 
                        _policeVehicle.AttachedBlip.Alpha = 255; 
                    }
                }  
                await BaseScript.Delay(0);
            }

            _policeVehicle.AttachedBlip.Delete();

            if (IsAllTargetsDead())
            { 
                MGame.MGameAPI.AddValueToBankBalance(_payout);
            }
             
        }

        private bool IsAllTargetsDead()
        {
            foreach (var item in _targetPeds)
            {
                if (!item.IsDead) return false;
            }
            return true;
        }

        private bool IsValidModel(Model model)
        { 
            return model.IsCar;
        }
    }
}
