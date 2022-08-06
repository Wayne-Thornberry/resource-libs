using CitizenFX.Core;
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
        private List<Vector3> _aptEntrances;
        private List<Vector3> _aptExits; 
        private Blip _blip;
        private Vector3 _lastExit;
        private Vector3 _lastEntrance;
        private List<Vector3> _gargEntrances;
        private List<Vector3> _gargExits;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("CharacterApts") > 1)
                return;

            _aptEntrances = new List<Vector3>();
            for (int i = 0; i < MWorld.WorldAPI.GetAptNumOfEntrances("apt_dpheights"); i++)
            {
                _aptEntrances.Add(MWorld.WorldAPI.GetAptEntrance("apt_dpheights", i));
            };
            _gargEntrances = new List<Vector3>();
            for (int i = 0; i < MWorld.WorldAPI.GetNumOfEntrances("gar_dpheights"); i++)
            {
                _aptEntrances.Add(MWorld.WorldAPI.GetEntrance("gar_dpheights", i));
            };
            _aptExits = new List<Vector3>();
            for (int i = 0; i < MWorld.WorldAPI.GetAptNumOfExits("apt_dpheights", "apt_dpheights_01"); i++)
            {
                _aptExits.Add(MWorld.WorldAPI.GetAptExit("apt_dpheights", "apt_dpheights_01", i));
            };
            _gargExits = new List<Vector3>();
            for (int i = 0; i < MWorld.WorldAPI.GetNumOfExits("gar_dpheights", "gar_dpheights_01"); i++)
            {
                _gargExits.Add(MWorld.WorldAPI.GetExit("gar_dpheights", "gar_dpheights_01", i));
            };
            _blip = World.CreateBlip(new Vector3(-1443.171f, -544.501f, 34.74184f));
            _blip.Sprite = BlipSprite.GTAOPlayerSafehouse;
            var stage = 0;

            while (!token.IsCancellationRequested)
            {
                switch (stage)
                {
                    case 0:
                        foreach (var entrance in _aptEntrances)
                        { 
                            World.DrawMarker(MarkerType.DebugSphere, entrance, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                System.Drawing.Color.FromArgb(150, 0, 0, 0));
                            if (World.GetDistance(Game.PlayerPed.Position, entrance) < 2f)
                            {
                                MWorld.WorldAPI.EnterApartmentProperty("apt_dpheights", "apt_dpheights_01");
                                _aptExits = new List<Vector3>();
                                for (int i = 0; i < MWorld.WorldAPI.GetAptNumOfExits("apt_dpheights", "apt_dpheights_01"); i++)
                                {
                                    _aptExits.Add(MWorld.WorldAPI.GetAptExit("apt_dpheights", "apt_dpheights_01", i));
                                };
                                _lastExit = _aptExits[0];
                                stage = 1;
                            }
                        }

                        foreach (var entrance in _gargEntrances)
                        {
                            World.DrawMarker(MarkerType.DebugSphere, entrance, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                System.Drawing.Color.FromArgb(150, 0, 0, 0));
                            if (World.GetDistance(Game.PlayerPed.Position, entrance) < 2f)
                            {
                                MWorld.WorldAPI.EnterProperty("gar_dpheights", "gar_dpheights_01");
                                _gargExits = new List<Vector3>();
                                for (int i = 0; i < MWorld.WorldAPI.GetNumOfExits("gar_dpheights", "gar_dpheights_01"); i++)
                                {
                                    _gargExits.Add(MWorld.WorldAPI.GetExit("gar_dpheights", "gar_dpheights_01", i));
                                };
                                _lastExit = _gargExits[0];
                                stage = 1;
                            }
                        }
                        break;
                    case 1:
                        {
                            if (World.GetDistance(Game.PlayerPed.Position, _lastExit) > 4f)
                            { 
                                stage = 2;
                            }
                        }
                        break;
                    case 2:
                        foreach (var exit in _aptExits)
                        { 
                            World.DrawMarker(MarkerType.DebugSphere, exit, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                System.Drawing.Color.FromArgb(150, 0, 0, 0));
                            if (World.GetDistance(Game.PlayerPed.Position, exit) < 2f)
                            {
                                MWorld.WorldAPI.ExitApartmentProperty("apt_dpheights", "apt_dpheights_01");
                                _aptEntrances = new List<Vector3>();
                                for (int i = 0; i < MWorld.WorldAPI.GetAptNumOfEntrances("apt_dpheights"); i++)
                                {
                                    _aptEntrances.Add(MWorld.WorldAPI.GetAptEntrance("apt_dpheights", i));
                                };
                                _lastEntrance = _aptEntrances[0];
                                stage = 3;
                            }
                        }


                        foreach (var exit in _gargExits)
                        {
                            World.DrawMarker(MarkerType.DebugSphere, exit, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                System.Drawing.Color.FromArgb(150, 0, 0, 0));
                            if (World.GetDistance(Game.PlayerPed.Position, exit) < 2f)
                            {
                                MWorld.WorldAPI.ExitProperty("gar_dpheights", "gar_dpheights_01");
                                _gargEntrances = new List<Vector3>();
                                for (int i = 0; i < MWorld.WorldAPI.GetNumOfEntrances("gar_dpheights"); i++)
                                {
                                    _gargEntrances.Add(MWorld.WorldAPI.GetEntrance("gar_dpheights", i));
                                };
                                _lastEntrance = _gargEntrances[0];
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
    }
}
