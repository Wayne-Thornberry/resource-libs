using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore
{
    public abstract class ModuleContext
    {
        public virtual void OnStart() { }
        public virtual void OnLoad() { }
    }
}
