using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.MDebug;

namespace Proline.ClassicOnline.SClassic.Mission
{
    public class HePacStandard
    {
        private readonly List<Entity> _bankDoors;
        private readonly string _cameraNameHash;
        private readonly List<Entity> _cameras;
        private readonly Ped[] _civilians;
        private readonly string[] _doorNameHash;
        private readonly List<Ped> _peds;
        private readonly Ped[] _securityGuards;
        private readonly string _securityPanelNameHash;
        private readonly List<Entity> _securityPanels;
        private readonly Ped[] _tellers;
        private readonly string _vaultNameHash;
        private int _interiorHandle;
        private int _moneh;
        private Entity _vaultDoor;
        private Blip _worldBlip;

        public Vector3 LocalPosition { get; private set; }
        public int Stage { get; private set; }

        public HePacStandard()
        {
            _securityGuards = new Ped[5];
            _civilians = new Ped[6];
            _tellers = new Ped[3];
            _securityPanels = new List<Entity>();
            _cameras = new List<Entity>();
            _bankDoors = new List<Entity>();
            _peds = new List<Ped>();
            _moneh = 3472364;
            _cameraNameHash = "hei_prop_bank_cctv_01";
            _securityPanelNameHash = "hei_prop_hei_securitypanel";
            _vaultNameHash = "v_ilev_bk_vaultdoor";
            _doorNameHash = new[]
            {
                "hei_prop_hei_bankdoor_new",
                "prop_ld_bankdoors_01",
                "prop_ld_bankdoors_02",
                "hei_v_ilev_bk_gate2_pris",
                "hei_v_ilev_bk_safegate_pris",
                "hei_v_ilev_bk_gate_pris",
                "v_ilev_bk_vaultdoor"
            };
        }

        private async Task SpawnPeds()
        {
            await SpawnCivilians();
            await SpawnGuards();
            await SpawnTellers();
            _peds.AddRange(_civilians);
            _peds.AddRange(_securityGuards);
            _peds.AddRange(_tellers);
        }

        private async Task NotifyPlayer()
        {
            Screen.ShowNotification("Oh look a bank");
            _worldBlip.IsFlashing = true;
            await BaseScript.Delay(1000);
            _worldBlip.IsFlashing = false;
        }

        private void CreateBlip()
        {
            _worldBlip = World.CreateBlip(LocalPosition);
        }

        private int GetInteriorAtPosition(Vector3 position)
        {
            return API.GetInteriorAtCoords(position.X, position.Y, position.Z);
        }

        private void GetNearbyEntities()
        {
            var entities = World.GetAllProps();
            foreach (var entity in entities)
            {
                if (entity.Model == API.GetHashKey(_cameraNameHash))
                {
                    _cameras.Add(entity);
                    MDebugAPI.LogDebug("Found the camera");
                    continue;
                }

                if (entity.Model == API.GetHashKey(_securityPanelNameHash))
                {
                    _securityPanels.Add(entity);
                    MDebugAPI.LogDebug("Found the security panel");
                    continue;
                }

                if (entity.Model == API.GetHashKey(_vaultNameHash))
                {
                    _vaultDoor = entity;
                    MDebugAPI.LogDebug("Found the vault door");
                }

                foreach (var doorNameHash in _doorNameHash)
                    if (entity.Model == API.GetHashKey(doorNameHash))
                    {
                        _bankDoors.Add(entity);
                        MDebugAPI.LogDebug("Found a door");
                    }
            }
        }

        private async Task SpawnTellers()
        {
            for (var i = 0; i < _tellers.Length; i++)
            {
                _tellers[i] = await World.CreatePed(new Model("cs_bankman"), LocalPosition);
                _civilians[i].Task.ClearAllImmediately();
                _civilians[i].Task.StandStill(-1);
            }
        }

        private async Task SpawnGuards()
        {
            for (var i = 0; i < _tellers.Length; i++)
            {
                _securityGuards[i] = await World.CreatePed(new Model("mp_m_securoguard_01"), LocalPosition);
                _civilians[i].Task.ClearAllImmediately();
                _civilians[i].Task.StandStill(-1);
            }
        }

        private async Task SpawnCivilians()
        {
            for (var i = 0; i < _tellers.Length; i++)
            {
                _civilians[i] = await World.CreatePed(new Model("ig_bankman"), LocalPosition);
                _civilians[i].Task.ClearAllImmediately();
                _civilians[i].Task.StandStill(-1);
            }
        }

        private void LockAllDoors()
        {
            API.DoorControl((uint)_vaultDoor.Model.Hash, _vaultDoor.Position.X, _vaultDoor.Position.Y,
                _vaultDoor.Position.Z, true, 1f, 1f, 1f);
            _vaultDoor.IsPositionFrozen = true;
            foreach (var door in _bankDoors)
            {
                MDebugAPI.LogDebug("Looping through detected door...");
                door.IsPositionFrozen = true;
                API.DoorControl((uint)door.Model.Hash, door.Position.X, door.Position.Y, door.Position.Z, true, 1f, 1f,
                    1f);
            }
        }

        public async Task Execute(object[] args, CancellationToken token)
        {


            _interiorHandle = -1;//GetInteriorAtPosition(LocalPosition);
            GetNearbyEntities();
            LockAllDoors();
            CreateBlip();
            await SpawnPeds();
            NotifyPlayer();

            while (Stage != -1)
            {

                if (false)//IsPointWithinActivationRange())
                {
                    foreach (var door in _bankDoors)
                        if (door.HasBeenDamagedBy(WeaponHash.StickyBomb) && door.IsPositionFrozen)
                            door.IsPositionFrozen = false;

                    if (API.GetInteriorFromEntity(Game.PlayerPed.Handle) != 0)
                    {
                        foreach (var securityPanel in _securityPanels)
                        {
                            if (Game.PlayerPed.IsNearEntity(securityPanel, new Vector3(1, 1, 1)))
                            {
                                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to delete");
                                if (Game.IsControlJustPressed(0, Control.Context)) _vaultDoor.Delete();
                            }
                        }

                        foreach (var securityPanel in _securityPanels)
                            if (Game.PlayerPed.IsNearEntity(securityPanel, new Vector3(1f, 1f, 1f)))
                            {
                                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to delete");
                                if (Game.IsControlJustPressed(0, Control.Context))
                                {
                                    var clos = World.GetClosest(securityPanel.Position, _bankDoors.ToArray());
                                    clos.Delete();
                                    API.PlaySoundFrontend(-1, "alarm_loop", "dlc_xm_submarine_sounds", false);
                                }
                            }
                    }
                }
                else
                {
                    _worldBlip.Delete();
                    DeletePeds();
                    //TerminateScript();
                }
                await BaseScript.Delay(0);
            }

        }

        private void DeletePeds()
        {
            foreach (var ped in _peds) ped.Delete();
        }
    }
}