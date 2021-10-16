using System.Threading.Tasks;

namespace Proline.Resource.Component.Framework
{
    public interface IComponentSource
    { 
        void TriggerEvent(bool isServerCall, string eventName, params object[] args);
        Task Wait(int ms);
    }
}