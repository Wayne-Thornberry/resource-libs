using CPoolObjects.Internal;
using Console = Proline.Resource.Console;

namespace CPoolObjects
{
    public class CPoolObjectsAPI : ICPoolObjectsAPI
    {
        public int[] GetAllExistingPoolObjects()
        {
            return PoolObjectManager.TrackedHandles;
        }
    }
}
