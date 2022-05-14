using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore
{
    public interface IModuleScript
    {
        bool EnableFrameSync { get; set; }
        bool HasStarted { get; }
        bool IsPaused { get; set; }
        int State { get; set; }

        Task OnStart();
        Task OnTick();
        Task OnUpdate();
    }
}