using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Buildings
{
    public class CharacterApts
    {
        private const string PropertyId = "apt_dpheights_he_01";
        private List<Vector3> _buildingEntrances;
        private List<Vector3> _interiorExits;
        private Blip _blip;
        private string _targetProperty;
        private string _targetArea;
        private Vector3 _lastPoint; 
        private string _t;
        private Vector3 _buildingVector;
        private string _interior;
        private string _enteredBuilding;
        private string _neariestEntrance;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("CharacterApts") > 1)
                return;

            _t = "apt";
            //RefreshExitPoints();
            RefreshEntryPoints();

            _blip = World.CreateBlip(new Vector3(-1443.171f, -544.501f, 34.74184f));
            _blip.Sprite = BlipSprite.GTAOPlayerSafehouse;
            var stage = 0;



            while (!token.IsCancellationRequested)
            {

                switch (stage)
                {
                    case 0:
                        foreach (var entrance in _buildingEntrances)
                        { 
                            World.DrawMarker(MarkerType.DebugSphere, entrance, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                System.Drawing.Color.FromArgb(150, 0, 0, 0));
                            if (World.GetDistance(Game.PlayerPed.Position, entrance) < 2f)
                            {
                                _enteredBuilding = WorldAPI.GetNearestBuilding();
                                _neariestEntrance = WorldAPI.GetNearestBuildingEntrance();
                                _buildingVector = WorldAPI.GetBuildingWorldPos(_enteredBuilding);  
                                var whereAreYouEntering = WorldAPI.EnterBuilding(_enteredBuilding, _neariestEntrance); 
                                switch (whereAreYouEntering)
                                {
                                    case "Garage": _targetProperty = WorldAPI.GetPropertyGarage(PropertyId);  break;
                                    case "Apartment": _targetProperty = WorldAPI.GetPropertyApartment(PropertyId); break;
                                }
                                _targetArea = whereAreYouEntering;
                                //WorldAPI.EnterProperty(PropertyId, _targetProperty, _neariestEntrance);
                                _interior = WorldAPI.GetPropertyInterior(_targetProperty, whereAreYouEntering + "s");
                                var spawnPoint = WorldAPI.GetPropertyEntry(_targetProperty, whereAreYouEntering + "s", _neariestEntrance);
                                _lastPoint = WorldAPI.EnterInterior(_interior, spawnPoint);
                                Game.PlayerPed.Position = _lastPoint; 
                                RefreshExitPoints();
                                stage = 1;
                                MDebug.MDebugAPI.LogDebug(stage);
                            }
                        } 
                        break;
                    case 1:
                        {
                            if (World.GetDistance(Game.PlayerPed.Position, _lastPoint) > 4f)
                            { 
                                stage = 2;
                                MDebug.MDebugAPI.LogDebug(stage);
                            }
                        }
                        break;
                    case 2:
                        foreach (var exit in _interiorExits)
                        { 
                            World.DrawMarker(MarkerType.DebugSphere, exit, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                System.Drawing.Color.FromArgb(150, 0, 0, 0));
                            if (World.GetDistance(Game.PlayerPed.Position, exit) < 2f)
                            { 
                                var neariestExit = WorldAPI.GetNearestInteriorExit(_interior);
                                var whereAreYouExiting = WorldAPI.ExitInterior(_interior, neariestExit); 
                                var newExit = WorldAPI.GetPropertyExit(_targetProperty, _targetArea + "s", neariestExit);
                                var placePosition = WorldAPI.ExitBuilding(_enteredBuilding, newExit, Game.PlayerPed.IsInVehicle() ? 1 : 0);
                                Game.PlayerPed.Position = placePosition;
                                _lastPoint = placePosition;
                                RefreshEntryPoints(); 
                                stage = 3;
                                MDebug.MDebugAPI.LogDebug(stage);
                            }
                        } 
                        break;
                    case 3:
                        {
                            if (World.GetDistance(Game.PlayerPed.Position, _lastPoint) > 4f)
                            {
                                stage = 0;
                                MDebug.MDebugAPI.LogDebug(stage);
                            }
                        }
                        break;
                }

                await BaseScript.Delay(0);
            }
        }

        private void RefreshEntryPoints()
        {
            _buildingEntrances = new List<Vector3>();
            for (int i = 0; i < MWorld.WorldAPI.GetNumOfBuldingEntrances(WorldAPI.GetPropertyBuilding(PropertyId)); i++)
            {
                _buildingEntrances.Add(MWorld.WorldAPI.GetBuildingEntrance(WorldAPI.GetPropertyBuilding(PropertyId), i));
            };
        }

        private void RefreshExitPoints()
        {
            var id = _t.Equals("gar") ? 0 : 1;
            _interiorExits = new List<Vector3>(); 
            for (int i = 0; i < MWorld.WorldAPI.GetNumOfInteriorExits(_interior); i++)
            {
                var x = MWorld.WorldAPI.GetInteriorExit(_interior, i);
                _interiorExits.Add(x);
                MDebug.MDebugAPI.LogDebug($"{i} {JsonConvert.SerializeObject(x)}");
            };
        }
    }
}
