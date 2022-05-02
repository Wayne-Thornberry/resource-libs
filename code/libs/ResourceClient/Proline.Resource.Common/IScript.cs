using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Common
{
    public interface IScript
    {
        bool EnableFrameSync { get; set; }
        bool HasStarted { get; }
        bool IsPaused { get; set; } 
        int State { get; set; }
        Task OnStart(); 
        Task OnUpdate();
        Task OnTick();

    }
}
