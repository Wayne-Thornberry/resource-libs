using CitizenFX.Core;
using CWorldObjects;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static int CreateMarker(Vector3 position, float activationRange = 2f)
        {

            var api = new CWorldObjectsAPI();
            return api.CreateMarker(position, activationRange);
        }

        public static void DrawMarker(int handle)
        {

            var api = new CWorldObjectsAPI();
            api.DrawMarker(handle);
        }

        public static bool IsInMarker(int handle, int obj)
        {
            var api = new CWorldObjectsAPI();
            return api.IsInMarker(handle, obj);
        }

        public static void DeleteMarker(int handle)
        {
            var api = new CWorldObjectsAPI();
            api.DeleteMarker(handle);

        }
    }
}
