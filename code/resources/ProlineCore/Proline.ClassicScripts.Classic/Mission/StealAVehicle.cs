using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.MBrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class StealAVehicle
    {
        private float _closestDistance;
        private int _payout;
        private Entity _targetEntity;
        private Vector3 _targetDeliveryPos;
        private Blip _targetBlip;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("StealAVehicle") > 1)
                return;
            _closestDistance = 99999.0f;
            _payout = 1000;

            var handles = MBrainAPI.GetEntityHandlesByTypes(GScripting.EntityType.VEHICLE);

            foreach (var item in handles)
            {
                var entity = Entity.FromHandle(item);
                var distance = World.GetDistance(entity.Position, Game.PlayerPed.Position);
                if (distance < _closestDistance)
                {
                    _targetEntity = entity;
                    _closestDistance = distance;
                }
            }

            _targetEntity.AttachBlip();
            _targetDeliveryPos = new Vector3(-347.4664f, 368.7049f, 109.0104f);
            _targetBlip = World.CreateBlip(_targetDeliveryPos);

            Screen.ShowNotification("Go steal that vehicle");


            while (!token.IsCancellationRequested)
            {
                _targetBlip.Alpha = 0;
                if (World.GetDistance(_targetEntity.Position, _targetDeliveryPos) < 10f)
                { 
                    if (!Game.PlayerPed.IsInVehicle())
                    {
                        var character = CharacterGlobals.Character;
                        if(character != null)
                        {
                            MGame.MGameAPI.SetCharacterBankBalance(MGame.MGameAPI.GetCharacterBankBalance() + _payout);
                        } 
                        _targetEntity.Delete();
                        break;
                    }
                }


                if (Game.PlayerPed.IsInVehicle())
                {
                    if (Game.PlayerPed.CurrentVehicle == _targetEntity)
                    {
                        _targetBlip.Alpha = 255;
                    }
                } 
                await BaseScript.Delay(0);
            }
        }
    }
}
