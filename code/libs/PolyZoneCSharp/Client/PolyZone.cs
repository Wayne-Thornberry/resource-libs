using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyZoneCSharp
{
    internal class PolyZone : PolyZoneShared
    {
        static float? minZ = null;
        static float? maxZ = null;
        private static object createdZone;

        public float handleInput(float center)
        {
            var rot = API.GetGameplayCamRot(2);
            center = handleArrowInput(center, rot.Z);
            return center;
        }

        public static void polyStart(string name) {
            var coords = API.GetEntityCoords(API.PlayerPedId(), true);
            createdZone = PolyZoneShared.Create(new Vector2(coords.X, coords.Y), new { name = name, useGrid = false});
            Task.Factory.StartNew(() =>
            {
                while (createdZone != null)
                {
                    //Have to convert the point to a vector3 prior to calling handleInput,
                    //then convert it back to vector2 afterwards
                    var lastPoint = createdZone.points[createdZone.points.Length - 1];
                    lastPoint = new Vector3(lastPoint.x, lastPoint.y, 0.0);
                    lastPoint = handleInput(lastPoint);
                    createdZone.points[createdZone.points.Length - 1] = new Vector2(lastPoint.x, lastPoint.y);
                    BaseScript.Delay(0);
                }
            });

            minZ = coords.Z; 
            maxZ= coords.Z;
        }
        

        public static void polyFinish()
        {
            BaseScript.TriggerServerEvent("polyzone:printPoly", new { name = createdZone.name, points = createdZone.points, minZ = minZ, maxZ = maxZ });
        }
    }
}
