using CGameLogic.Data;

namespace CScriptObjs
{
    public interface ICScriptObjsAPI
    {
        int[] GetEntityHandlesByTypes(EntityType type);
        CitizenFX.Core.Entity GetNeariestEntity(EntityType type);
    }
}