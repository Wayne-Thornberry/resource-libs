using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public interface IResourceScript
    {
        bool HasStarted { get; }
        bool IsPaused { get; }
        int State { get; set; }
        Task OnStart();
        Task OnUpdate();
    }
}
