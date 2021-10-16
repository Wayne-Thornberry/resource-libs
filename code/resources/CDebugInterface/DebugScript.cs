using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Game.Component;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CDebugInterface
{
    internal class DebugScript : ComponentScript
    {
        private int[] _handles;

        protected override void OnUpdate()
        {
            var t = CitizenFX.Core.Game.PlayerPed.Position.ToString() + "H:" + CitizenFX.Core.Game.PlayerPed.Heading + "\n"
               + CitizenFX.Core.Game.PlayerPed.Model.Hash + "\n"
               + CitizenFX.Core.Game.PlayerPed.Health + "\n"
               + CitizenFX.Core.Game.PlayerPed.Handle + "\n";
            ComponentAPI.DrawDebugText2D(t, new PointF(0.005f, 0.05f), 0.2f, 1);
            foreach (var handle in _handles)
            {
                var entity = Entity.FromHandle(handle);
                if (entity == null) continue;
                if (!API.IsEntityVisible(entity.Handle) || World.GetDistance(entity.Position, CitizenFX.Core.Game.PlayerPed.Position) > 10f) continue;
                var pos = entity.Model.GetDimensions();
                var d = entity.Position + new Vector3(0, 0, (entity.Model.GetDimensions().Z * 0.8f));
                var x = $"{entity.Handle}\n" +
                    $"{entity.Model.Hash}\n" +
                    $"{Math.Round(entity.Heading, 2)}\n" +
                    $"{entity.Health}\n";// +
                                         //$"{ExampleAPI.IsEntityScripted(entity.Handle)}";
                ComponentAPI.DrawEntityBoundingBox(entity.Handle, 125, 125, 125, 100);
                ComponentAPI.DrawDebugText3D(x, d, 3f, 0);
            }
        }
        protected override void OnStart()
        {
            _handles = new int[0];
        }

    }
}
