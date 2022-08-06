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
        private Vector3 _entrance;
        private List<Vector3> _exits; 
        private Blip _blip;
        private Vector3 _lastExit;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (MScripting.MScriptingAPI.GetInstanceCountOfScript("CharacterApts") > 1)
                return;

            _entrance = new Vector3(-1443.171f, -544.501f, 34.74184f);
            _exits = new List<Vector3>();
            _blip = World.CreateBlip(_entrance);
            _blip.Sprite = BlipSprite.GTAOPlayerSafehouse;
            var stage = 0;

            while (!token.IsCancellationRequested)
            {
                switch (stage)
                {
                    case 0:

                        World.DrawMarker(MarkerType.DebugSphere, _entrance, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                            System.Drawing.Color.FromArgb(150, 0, 0, 0));
                        if (World.GetDistance(Game.PlayerPed.Position, _entrance) < 2f)
                        {
                            MWorld.WorldAPI.TeleportToAptInterior("apt_dpheights_01");
                            for (int i = 0; i < MWorld.WorldAPI.GetAptNumOfExits(); i++)
                            {
                                _exits.Add(MWorld.WorldAPI.GetAptExit(i));
                            } ;
                            _lastExit = _exits[0];
                            stage = 1;
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
                        foreach (var exit in _exits)
                        { 
                            World.DrawMarker(MarkerType.DebugSphere, exit, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                System.Drawing.Color.FromArgb(150, 0, 0, 0));
                            if (World.GetDistance(Game.PlayerPed.Position, exit) < 2f)
                            {
                                MWorld.WorldAPI.TeleportToAptExterior("apt_dpheights_01");
                                stage = 3;
                            }
                        }
                        break;
                    case 3:
                        {
                            if (World.GetDistance(Game.PlayerPed.Position, _entrance) > 4f)
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
