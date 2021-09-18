using System;
using System.Drawing;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine.AAPI;
using Proline.Engine.Componentry;
using Proline.Engine.Data;
using Proline.Engine.Five;

namespace Proline.Classic.Components
{
    public class CDebugInterface : ClientComponent
    {
        private int[] _handles;

        //protected override void OnUpdate()
        //{
        //    if (EngineConfiguration.IsClient)
        //    {  
        //        var t = Game.PlayerPed.Position.ToString() + "H:" + Game.PlayerPed.Heading + "\n"
        //           + Game.PlayerPed.Model.Hash + "\n"
        //           + Game.PlayerPed.Health + "\n"
        //           + Game.PlayerPed.Handle + "\n";
        //        ExampleAPI.DrawDebugText2D(t, new PointF(0.005f, 0.05f), 0.2f, 1);
        //        foreach (var handle in _handles)
        //        {
        //            var entity = Entity.FromHandle(handle);
        //            if (entity == null) continue;
        //            if (!API.IsEntityVisible(entity.Handle) || World.GetDistance(entity.Position, Game.PlayerPed.Position) > 10f) continue;
        //            var pos = entity.Model.GetDimensions();
        //            var d = entity.Position + new Vector3(0, 0, (entity.Model.GetDimensions().Z * 0.8f));
        //            var x = $"{entity.Handle}\n" +
        //                $"{entity.Model.Hash}\n" +
        //                $"{Math.Round(entity.Heading, 2)}\n" +
        //                $"{entity.Health}\n";// +
        //                                     //$"{ExampleAPI.IsEntityScripted(entity.Handle)}";
        //            ExampleAPI.DrawEntityBoundingBox(entity.Handle, 125, 125, 125, 100);
        //            ExampleAPI.DrawDebugText3D(x, d, 3f, 0);
        //        }
        //    }
        //}
        protected override void OnStart()
        {
            if (EngineConfiguration.IsClient)
            {
                _handles = new int[0];
            }
        }

    }
}
