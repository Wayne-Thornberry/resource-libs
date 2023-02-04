using CitizenFX.Core;

namespace CGameRendering
{
    public interface ICGameRenderingAPI
    {
        Vector3 ConvertLocalToWorld(Vector3 origin, float originRotation, Vector3 localPosition);
        Vector3 ConvertWorldToLocal(Vector3 origin, float originRotation, Vector3 Worldposition);
        void DrawBoundingBox(Vector3 start, Vector3 end, int r, int g, int b, int a);
        void DrawBoundingBoxFromPoints(Vector3[] points, int r, int g, int b, int a);
        void DrawBoundingPlaneFromPoints(Vector3[] points, int r, int g, int b, int a);
        void DrawDebugText3D(string text, Vector3 vector3, float scale2, int font);
        void DrawEntityBoundingBox(int ent, int r, int g, int b, int a);
        Vector3[] GetBoundingBox(Vector3 start, Vector3 end, float heading = 0);
        Vector3[] GetEntityBoundingBox(int entity);
        void DoTest();
    }
}