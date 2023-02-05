using CGameRendering;
using CitizenFX.Core; 

namespace Proline.ClassicOnline.Engine.Parts
{

    public static partial class EngineAPI
    {
        public static Vector3 ConvertLocalToWorld(Vector3 origin, float originRotation, Vector3 localPosition)
        {
            var api = new CGameRenderingAPI();
            return api.ConvertLocalToWorld(origin, originRotation, localPosition);
        }
        public static Vector3 ConvertWorldToLocal(Vector3 origin, float originRotation, Vector3 Worldposition)
        {
            var api = new CGameRenderingAPI();
            return api.ConvertWorldToLocal(origin, originRotation, Worldposition);
        }
        public static void DrawBoundingBox(Vector3 start, Vector3 end, int r, int g, int b, int a)
        {
            var api = new CGameRenderingAPI();
            api.DrawBoundingBox(start, end, r, g, b, a);
        }
        public static void DrawBoundingBoxFromPoints(Vector3[] points, int r, int g, int b, int a)
        {
            var api = new CGameRenderingAPI();
            api.DrawBoundingBoxFromPoints(points, r, g, b, a);
        }
        public static void DrawBoundingPlaneFromPoints(Vector3[] points, int r, int g, int b, int a)
        {
            var api = new CGameRenderingAPI();
            api.DrawBoundingPlaneFromPoints(points, r, g, b, a);
        }
        public static void DrawDebugText3D(string text, Vector3 vector3, float scale2, int font)
        {
            var api = new CGameRenderingAPI();
            api.DrawDebugText3D(text, vector3, scale2, font);
        }
        public static void DrawEntityBoundingBox(int ent, int r, int g, int b, int a)
        {
            var api = new CGameRenderingAPI();
            api.DrawEntityBoundingBox(ent, r, g, b, a);
        }
        public static Vector3[] GetBoundingBox(Vector3 start, Vector3 end, float heading = 0)
        {
            var api = new CGameRenderingAPI();
            return api.GetBoundingBox(start, end, heading);
        }
        public static Vector3[] GetEntityBoundingBox(int entity)
        {
            var api = new CGameRenderingAPI();
            return api.GetEntityBoundingBox(entity);
        }


    }
}
