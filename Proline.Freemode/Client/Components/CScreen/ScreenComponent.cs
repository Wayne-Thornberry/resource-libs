extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CScreen
{
    public class ScreenComponent : EngineComponent
    {
        [Client]
        [ComponentAPI]
        public static Vector3 ScreenRelToWorld(Vector3 camPos, Vector3 camRot, Vector2 coord, out Vector3 forwardDirection)
        {
            var camForward = RotationToDirection(camRot);
            var rotUp = camRot + new Vector3(1, 0, 0);
            var rotDown = camRot + new Vector3(-1, 0, 0);
            var rotLeft = camRot + new Vector3(0, 0, -1);
            var rotRight = camRot + new Vector3(0, 0, 1);

            var camRight = RotationToDirection(rotRight) - RotationToDirection(rotLeft);
            var camUp = RotationToDirection(rotUp) - RotationToDirection(rotDown);

            var rollRad = -DegToRad(camRot.Y);

            var camRightRoll = camRight * (float)Math.Cos(rollRad) - camUp * (float)Math.Sin(rollRad);
            var camUpRoll = camRight * (float)Math.Sin(rollRad) + camUp * (float)Math.Cos(rollRad);

            var point3D = camPos + camForward * 1.0f + camRightRoll + camUpRoll;
            Vector2 point2D;
            if (!World3DToScreen2D(point3D, out point2D))
            {
                forwardDirection = camForward;
                return camPos + camForward * 1.0f;
            }
            var point3DZero = camPos + camForward * 1.0f;
            Vector2 point2DZero;
            if (!World3DToScreen2D(point3DZero, out point2DZero))
            {
                forwardDirection = camForward;
                return camPos + camForward * 1.0f;
            }

            const double eps = 0.001;
            if (Math.Abs(point2D.X - point2DZero.X) < eps || Math.Abs(point2D.Y - point2DZero.Y) < eps)
            {
                forwardDirection = camForward;
                return camPos + camForward * 1.0f;
            }
            var scaleX = (coord.X - point2DZero.X) / (point2D.X - point2DZero.X);
            var scaleY = (coord.Y - point2DZero.Y) / (point2D.Y - point2DZero.Y);
            var point3Dret = camPos + camForward * 1.0f + camRightRoll * scaleX + camUpRoll * scaleY;
            forwardDirection = camForward + camRightRoll * scaleX + camUpRoll * scaleY;
            return point3Dret;
        }

        private static bool World3DToScreen2D(Vector3 pos, out Vector2 result)
        {
            var x2dp = 0f;
            var y2dp = 0f;

            bool success =  API.World3dToScreen2d(pos.X, pos.Y, pos.Z, ref x2dp, ref y2dp); //  GET_SCREEN_COORD_FROM_WORLD_COORD and previously, _WORLD3D_TO_SCREEN2D
            result = new Vector2(x2dp, y2dp);
            return success;
        }

        private static float DegToRad(float _deg)
        {
            double Radian = (Math.PI / 180) * _deg;
            return (float)Radian;
        }

        private static Vector3 RotationToDirection(Vector3 rotation)
        {
            var z = DegToRad(rotation.Z);
            var x = DegToRad(rotation.X);
            var num = Math.Abs(Math.Cos(x));
            return new Vector3
            {
                X = (float)(-Math.Sin(z) * num),
                Y = (float)(Math.Cos(z) * num),
                Z = (float)Math.Sin(x)
            };
        }
    }
}
