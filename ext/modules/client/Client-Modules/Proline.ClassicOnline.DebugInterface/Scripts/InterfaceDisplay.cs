using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MRendering;
using Proline.ClassicOnline.MScreen;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MRendering.Scripts
{
    internal class InterfaceDisplay : ModuleScript
    {
        public override async Task OnUpdate()
        {
            var t = Game.PlayerPed.Position.ToString() + "H:" + Game.PlayerPed.Heading + "\n"
               + Game.PlayerPed.Model.Hash + "\n"
               + Game.PlayerPed.Health + "\n"
               + Game.PlayerPed.Handle + "\n";
            //_handles.Count + " Entities in the world ";
            ScreenAPI.DrawDebugText2D(t, new PointF(0.1f, 0.05f), 0.3f, 0);
            //foreach (var handle in _handles)
            //{
            //    var entity = Entity.FromHandle(handle);
            //    //_log.Debug(API.GetEntityType(handle).ToString());
            //    if (entity == null) continue;
            //    if (!API.IsEntityVisible(entity.Handle) || World.GetDistance(entity.Position, Game.PlayerPed.Position) > 10f) continue;
            //    // var pos = entity.Model.GetDimensions();
            //    var d = entity.Position + new Vector3(0, 0, entity.Model.GetDimensions().Z * 0.8f);
            //    var x = $"{entity.Handle}\n" +
            //        $"{entity.Model.Hash}\n" +
            //        $"{Math.Round(entity.Heading, 2)}\n" +
            //        $"{entity.Health}\n";// +
            //                             //$"{ExampleAPI.IsEntityScripted(entity.Handle)}";
            //                             //DebugUtil.DrawEntityBoundingBox(entity.Handle, 125, 125, 125, 100);
            //    DebugUtil.DrawDebugText3D(x, d, 3f, 0);
            //}
        }
    }
}
