using System;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.MScripting;

namespace Proline.ClassicOnline.SClassic.Event
{
    public class ReArmouredTruck
    {
        private readonly Vector3 _endLocal;
        private readonly int _maxReward;
        private readonly int _minReward;
        private readonly PlacePoint[] _placeLocals;
        private readonly Random _randomGenerator;
        private Vehicle _armouredTruck;
        private Ped _guardOne;
        private Ped _guardTwo;

        private bool _hasAttacked;
        private bool _hasDestroyed;
        private bool _hasLootSpawned;
        private bool _hasStolen;
        private int _rewardMoney;
        private int _rewardXp;
        private Blip _truckBlip;

        public ReArmouredTruck()
        {
            _randomGenerator = new Random();
            _minReward = 1000;
            _maxReward = 5000;
            _endLocal = new Vector3(-29.5f, -689.73f, 32.33f);
            _placeLocals = new[]
            {
                new PlacePoint(new Vector3(-1192.67f, -322.55f, 37.02f), new Vector3(0.70f, 0.08f, 26.08f)),
                new PlacePoint(new Vector3(260.52f, 188.27f, 104.12f), new Vector3(0.90f, 0.48f, 66.88f)),
                new PlacePoint(new Vector3(-346.03f, -29.47f, 47.31f), new Vector3(1.47f, 2.18f, -111.96f))
            };
        }

        private async Task NotifyPlayer()
        {
            _truckBlip = _armouredTruck.AttachBlip();
            API.SetBlipSprite(_truckBlip.Handle, 67);
            _truckBlip.IsFlashing = true;
            Screen.ShowNotification("An armoured truck has spawned in the world");
            Game.PlaySound("Friend_Deliver", "HUD_FRONTEND_MP_COLLECTABLE_SOUNDS");
            await BaseScript.Delay(3000);
            _truckBlip.IsFlashing = false;
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            SetArmouredTruckReward(GenerateArmouredTruckReward(), 100);
            var spawn = GetRandomSpawnLocation();
            await SpawnArmouredTruck(spawn);
            await SpawnArmouredTruckSecurity();
            await NotifyPlayer();
            _guardOne.Task.DriveTo(_armouredTruck, _endLocal, 20, 100, (int)DrivingStyle.Normal);

            while (!token.IsCancellationRequested)
            {
                if (_armouredTruck.Exists())
                {
                    if (World.GetDistance(_armouredTruck.Position, _endLocal) < 50)
                    {
                        MarkTruckForDeletion();
                        Screen.ShowNotification("The truck has made it to the UD safely");
                        // TerminateScript();
                    }
                    else if (_armouredTruck.IsDead)
                    {
                        Screen.ShowNotification("The truck has made it to the UD safely");
                    }

                    if (Game.PlayerPed.IsInVehicle())
                    {
                        if (Game.PlayerPed.CurrentVehicle == _armouredTruck)
                        {
                            if (_truckBlip.Alpha == 255)
                                _truckBlip.Alpha = 0;

                            Screen.DisplayHelpTextThisFrame("Stop the truck to spawn the loot");
                            if (Game.PlayerPed.CurrentVehicle.IsStopped)
                            {
                                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to open the back of the truck");
                                if (Game.IsControlPressed(0, Control.Context))
                                {
                                    LootTruck();
                                    MarkTruckForDeletion();
                                    // TerminateScript();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!_hasLootSpawned && _truckBlip.Alpha == 0) _truckBlip.Alpha = 255;
                    }
                }
                else
                {
                    MScriptingAPI.MarkScriptAsNoLongerNeeded(this);
                    MScriptingAPI.TerminateScriptInstance(this);
                }
                await BaseScript.Delay(0);
            }

        }


        private void SetArmouredTruckReward(int rewardMoney, int rewardXp)
        {
            _rewardMoney = rewardMoney;
            _rewardXp = rewardXp;
        }

        private async Task SpawnArmouredTruckSecurity()
        {
            _guardOne = await _armouredTruck.CreatePedOnSeat(VehicleSeat.Driver, PedHash.Security01SMM);
            _guardTwo = await _armouredTruck.CreatePedOnSeat(VehicleSeat.Passenger, PedHash.Security01SMM);
            while (!API.NetworkDoesNetworkIdExist(_guardOne.NetworkId) ||
                   !API.NetworkDoesNetworkIdExist(_guardTwo.NetworkId)) await BaseScript.Delay(0);
        }


        private async Task SpawnArmouredTruck(PlacePoint place)
        {
            _armouredTruck = await World.CreateVehicle(VehicleHash.Stockade, place.Position,
                GameMath.DirectionToHeading(place.Rotation));
            while (!API.NetworkDoesNetworkIdExist(_armouredTruck.NetworkId)) await BaseScript.Delay(0);
        }

        private void LootTruck()
        {
            _armouredTruck.Doors[VehicleDoorIndex.BackLeftDoor].Open();
            _armouredTruck.Doors[VehicleDoorIndex.BackRightDoor].Open();
            SpawnReward();
        }

        private int GenerateArmouredTruckReward()
        {
            return _randomGenerator.Next(_minReward, _maxReward);
        }

        private void ArmouredTruckStolen(int obj)
        {
            var veh = Entity.FromHandle(obj);
            if (veh != _armouredTruck || _hasStolen) return;
            _hasStolen = true;
        }

        private void ArmouredTruckDestroyed(int arg1, int arg2, uint arg3, bool arg4, int arg5)
        {
            var veh = Entity.FromHandle(arg1);
            if (veh != _armouredTruck || Game.PlayerPed.Handle != arg2 || _hasLootSpawned || _hasDestroyed) return;
            _hasDestroyed = true;
            Screen.DisplayHelpTextThisFrame("The armoured truck has been destroyed, and so has the money! " +
                                            "\n Try breaking open the back or getting into the vehicle to steal the money");
            TriggerConsequence(4);
        }

        private void ArmouredTruckAttacked(int arg1, int arg2, uint arg3, bool arg4, int arg5)
        {
            var veh = Entity.FromHandle(arg1);
            if (veh != _armouredTruck || Game.PlayerPed.Handle != arg2 || _hasAttacked) return;
            _hasAttacked = true;
            TriggerConsequence(3);
        }

        private void DisplayNetworkNotification(string player, int type)
        {
            switch (type)
            {
                case 1:
                    Screen.ShowNotification("~r~" + player + "~w~ attacked a armoured truck");
                    break;
                case 2:
                    Screen.ShowNotification("~r~" + player + "~w~ stole a armoured truck");
                    break;
                case 3:
                    Screen.ShowNotification("~r~" + player + "~w~ destroyed a armoured truck");
                    break;
            }
        }


        private PlacePoint GetRandomSpawnLocation()
        {
            return _placeLocals[_randomGenerator.Next(0, 2)];
        }

        private void MarkTruckForDeletion()
        {
            _truckBlip.Delete();
            _armouredTruck.Delete();
            _guardOne.Delete();
            _guardTwo.Delete();
        }

        private void TriggerConsequence(int wantedLevel)
        {
            if (Game.Player.WantedLevel < wantedLevel)
                Game.Player.WantedLevel = wantedLevel;
        }

        private void SpawnReward()
        {
            var cords = _armouredTruck.Position - _armouredTruck.ForwardVector * 4;
            API.CreateMoneyPickups(cords.X, cords.Y, cords.Z, _rewardMoney, 3, 289396019);
            _hasLootSpawned = true;
        }
    }

    internal class PlacePoint : ISpatial
    {
        public PlacePoint(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
    }
}