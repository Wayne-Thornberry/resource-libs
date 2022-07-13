using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MRendering;
using Proline.ClassicOnline.MScreen;

namespace Proline.ClassicOnline.SClassic
{
    public class DebugInterface
    {
        private List<int> _handles;

        public DebugInterface()
        {
            _handles = new List<int>();
        }


        public async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var t = Game.PlayerPed.Position.ToString() + "H:" + Game.PlayerPed.Heading + "\n"
                   + Game.PlayerPed.Model.Hash + "\n"
                   + Game.PlayerPed.Health + "\n"
                   + Game.PlayerPed.Handle + "\n" +
                   _handles.Count + " Entities in the world ";
                ScreenAPI.DrawDebugText2D(t, new PointF(0.01f, 0.05f), 0.3f, 0);
                //foreach (Entity entity in World.GetAllProps())
                //{ 
                //    //MDebugAPI.LogDebug(API.GetEntityType(handle).ToString());
                //    if (entity == null) continue;
                //    if (!API.IsEntityVisible(entity.Handle) || World.GetDistance(entity.Position, CitizenFX.Core.Game.PlayerPed.Position) > 10f) continue;
                //    // var pos = entity.Model.GetDimensions();
                //    var d = entity.Position + new Vector3(0, 0, (entity.Model.GetDimensions().Z * 0.8f));
                //    var x = $"{entity.Handle}\n" +
                //        $"{entity.Model.Hash}\n" +
                //        $"{Math.Round(entity.Heading, 2)}\n" +
                //        $"{entity.Health}\n";// +
                //                             //$"{ExampleAPI.IsEntityScripted(entity.Handle)}";
                //    //MRenderingAPI.DrawEntityBoundingBox(entity.Handle, 125, 125, 125, 100);
                //    //MRenderingAPI.DrawDebugText3D(x, d, 3f, 0);

                //}
                await BaseScript.Delay(0);
            }
        }
    }
}
