using System.ComponentModel;
using System.Threading.Tasks;

namespace Proline.Resource.Component.Framework
{
    public interface IComponentHandler : IComponentObject
    {
        Task OnInitialized();
        Task OnLoad();
        Task OnTick();
    }
}