using CGameLogic.Data;
using CitizenFX.Core;
using CScriptObjs;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static int[] GetEntityHandlesByTypes(int type)
        {
            var api = new CCScriptObjsAPI();
            return api.GetEntityHandlesByTypes((EntityType) type);
        }

        public static Entity GetNeariestEntity(int type)
        {
            var api = new CCScriptObjsAPI();
            return api.GetNeariestEntity((EntityType) type);
        }
    }
}
