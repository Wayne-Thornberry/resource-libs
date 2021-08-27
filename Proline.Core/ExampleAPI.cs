extern alias Client;
extern alias Server;

using Client.CitizenFX.Core.Native; 
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.CitizenFX.Core;

namespace Proline.Core
{
    public static class ExampleAPI
    {
        public static void PlayerAPI(string userName)
        {
            //var args = new object[] { userName };
            //APICaller.CallAPIAsync((int)EngineAPI.PlayerAPI, args);
        }

        public static int Example()
        {
            return -1;
        }

        public static void DrawDebug2DBox(PointF start, PointF end, Color color)
        {
            var args = new object[] { start, end, color };
            ComponentAPI.CallAPI((int)ExampleHash.DrawDebug2DBox, args);
        }

        public static Vector3 ScreenRelToWorld(Vector3 camPos, Vector3 camRot, Vector2 coord, out Vector3 forwardDirection)
        {
            var args = new object[] { camPos, camRot, coord, null };
            var result = ComponentAPI.CallAPI((int)ExampleHash.ScreenRelToWorld, args);
            forwardDirection = (Vector3)args[3];
            return (Vector3)result;
        }
        public static void StartNewEntityScript(string scriptName, int handle, params object[] param)
        {
            var args = new object[] { scriptName, handle, param };
            ComponentAPI.CallAPI((int)ExampleHash.StartNewEntityScript, args);
        }

        public static async Task<int> TestNetworkAPI(int x, int y, int z)
        {
            return 1;
            //var args = new object[] { x, y, z };
            //return await APICaller.CallAPIAsync<int>(EngineAPI.TestN, args);
        }

        public static void DrawEntityBoundingBox(int ent, int r, int g, int b, int a)
        {
            var args = new object[] { ent, r, g, b, a };
            ComponentAPI.CallAPI((int)ExampleHash.DrawEntityBoundingBox, args);
        }

        public static void DrawBoundingBoxFromPoints(Vector3[] points, int r, int g, int b, int a)
        {
            var args = new object[] { points, r, g, b, a };
            ComponentAPI.CallAPI((int)ExampleHash.DrawBoundingBoxFromPoints, args);
        }

        public static void DrawBoundingBox(Vector3 start, Vector3 end, int r, int g, int b, int a)
        {
            var args = new object[] { start, end, r, g, b, a };
            ComponentAPI.CallAPI((int)ExampleHash.DrawBoundingBox, args);
        }

        public static void GetNearbyEntities(out int[] entities)
        {
            var args = new object[1] { null };
            ComponentAPI.CallAPI(1841346673, args);
            entities = (int[])args[0];
        }

        public static void DrawDebugText3D(string text, Vector3 vector3, float scale, int font)
        {
            var args = new object[4] { text, vector3, scale, font };
            ComponentAPI.CallAPI((int)ExampleHash.DrawDebugText3D, args);
        }
        public static void DrawDebugText2D(string text, PointF vector3, float scale, int font)
        {
            var args = new object[4] { text, vector3, scale, font };
            ComponentAPI.CallAPI((int)ExampleHash.DrawDebugText2D, args);
        }

        public static bool IsEntityInActivationRange(int entHandle)
        {
            var args = new object[1] { entHandle };
            return (bool)ComponentAPI.CallAPI((int)ExampleHash.IsEntityInActivationRange, args);
        }
        public static bool IsInActivationRange(Vector3 vector3)
        {
            var args = new object[1] { vector3 };
            return (bool)ComponentAPI.CallAPI((int)ExampleHash.IsInActivationRange, args);
        }

        public static void UnlockNeareastVehicle()
        {
            var args = new object[0];
            ComponentAPI.CallAPI((int)ExampleHash.UnlockNeareastVehicle_2, args);
        }

        public static void SetPlayerAsPartOfPoliceGroup()
        {
            var args = new object[0];
            ComponentAPI.CallAPI((int)ExampleHash.SetPlayerAsPartOfPoliceGroup, args);
        }

        public static void AttachBlipsToGasStations()
        {
            var args = new object[0];
            ComponentAPI.CallAPI((int)ExampleHash.AttachBlipsToGasStations, args);
        }

        public static bool IsEntityScripted(int entityHandle)
        {
            var args = new object[1]{ entityHandle};
            return (bool) ComponentAPI.CallAPI(1187952361, args);
        }
    }
}
