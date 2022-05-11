using System.Threading.Tasks;

namespace Proline.ClassicOnline.ModuleCore
{
    public abstract class ModuleContext
    {
        public virtual void OnStart() { }
        public virtual void OnLoad() { }
        public virtual async Task OnTick() { }
    }
}
