﻿using CitizenFX.Core;
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
        private List<Vector3> _buildingEntrances;
        private List<Vector3> _interiorExits;
        private Blip _blip;
        private Vector3 _lastPoint;
        private Vector3 _lastEntrance;
        private string _t;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("CharacterApts") > 1)
                return;

            _t = "apt";
            RefreshExitPoints();
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
                                var neariestBulding = WorldAPI.GetNearestBuilding();
                                var neariestEntrance = WorldAPI.GetNearestBuildingEntrance();
                                MDebug.MDebugAPI.LogDebug(neariestBulding);
                                MDebug.MDebugAPI.LogDebug(neariestEntrance);
                                
                                var thing = MWorld.WorldAPI.EnterBuilding(neariestBulding, neariestEntrance);
                                if (string.IsNullOrEmpty(thing))
                                    thing = "All";
                                MDebug.MDebugAPI.LogDebug(thing);

                                _t = thing.Equals("Garage") ? "gar" : "apt";
                                MDebug.MDebugAPI.LogDebug(_t);
                                _lastPoint = MWorld.WorldAPI.EnterProperty("apt_dpheights_he_01", $"{_t}_dpheights_01", neariestEntrance);
                                RefreshExitPoints();
                                stage = 1;
                            }
                        } 
                        break;
                    case 1:
                        {
                            if (World.GetDistance(Game.PlayerPed.Position, _lastPoint) > 4f)
                            { 
                                stage = 2;
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
                                _lastPoint = MWorld.WorldAPI.ExitProperty("apt_dpheights_he_01", "apt_dpheights_01", WorldAPI.GetInteriorExitString(WorldAPI.GetPropertyInterior("apt_dpheights_he_01",1)));
                                RefreshEntryPoints(); 
                                stage = 3;
                            }
                        } 
                        break;
                    case 3:
                        {
                            if (World.GetDistance(Game.PlayerPed.Position, _lastEntrance) > 4f)
                            {
                                stage = 0;
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
            for (int i = 0; i < MWorld.WorldAPI.GetNumOfBuldingEntrances(WorldAPI.GetPropertyBuilding("apt_dpheights_he_01")); i++)
            {
                _buildingEntrances.Add(MWorld.WorldAPI.GetBuildingEntrance(WorldAPI.GetPropertyBuilding("apt_dpheights_he_01"), i));
            };
        }

        private void RefreshExitPoints()
        {
            var id = _t.Equals("gar") ? 0 : 1;
            _interiorExits = new List<Vector3>();
            for (int i = 0; i < MWorld.WorldAPI.GetNumOfInteriorExits(WorldAPI.GetPropertyInterior("apt_dpheights_he_01", id)); i++)
            {
                _interiorExits.Add(MWorld.WorldAPI.GetInteriorExit(WorldAPI.GetPropertyInterior("apt_dpheights_he_01", id), i));
            };
        }
    }
}