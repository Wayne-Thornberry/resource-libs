

using CPoolObjects;

namespace Proline.ClassicOnline.Engine.Parts
{

    public static partial class EngineAPI
    {

        public static int[] GetAllExistingPoolObjects()
        {
            var api = new CPoolObjectsAPI();
            return api.GetAllExistingPoolObjects();
        }
    }
}
