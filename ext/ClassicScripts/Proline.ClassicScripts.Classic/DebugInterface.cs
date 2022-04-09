﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.EngineFramework.Scripting;
using Proline.ExampleClient2.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ExampleClient2.Scripts
{
    public class DebugInterface : DemandScript
    { 
        private List<int> _handles;

        public DebugInterface(string name) :base(name)
        { 
            _handles = new List<int>(); 
        }
          

        public override async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            { 
                var t = CitizenFX.Core.Game.PlayerPed.Position.ToString() + "H:" + CitizenFX.Core.Game.PlayerPed.Heading + "\n"
                   + CitizenFX.Core.Game.PlayerPed.Model.Hash + "\n"
                   + CitizenFX.Core.Game.PlayerPed.Health + "\n"
                   + CitizenFX.Core.Game.PlayerPed.Handle + "\n" +
                   _handles.Count + " Entities in the world ";
                DebugUtil.DrawDebugText2D(t, new PointF(0.01f, 0.05f), 0.3f, 0);
                foreach (Entity entity in World.GetAllProps())
                { 
                    //_log.Debug(API.GetEntityType(handle).ToString());
                    if (entity == null) continue;
                    if (!API.IsEntityVisible(entity.Handle) || World.GetDistance(entity.Position, CitizenFX.Core.Game.PlayerPed.Position) > 10f) continue;
                    // var pos = entity.Model.GetDimensions();
                    var d = entity.Position + new Vector3(0, 0, (entity.Model.GetDimensions().Z * 0.8f));
                    var x = $"{entity.Handle}\n" +
                        $"{entity.Model.Hash}\n" +
                        $"{Math.Round(entity.Heading, 2)}\n" +
                        $"{entity.Health}\n";// +
                                             //$"{ExampleAPI.IsEntityScripted(entity.Handle)}";
                    DebugUtil.DrawEntityBoundingBox(entity.Handle, 125, 125, 125, 100);
                    DebugUtil.DrawDebugText3D(x, d, 3f, 0);

                }
                await Delay(0);
            }
        }
    }
}
