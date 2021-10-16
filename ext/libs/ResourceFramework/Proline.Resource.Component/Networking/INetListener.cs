using System.Threading.Tasks;

namespace Proline.Resource.Component.Networking
{
    public interface INetListener
    {
        int AliveFor { get; set; }

        Task Listen(long ms);
    }
}