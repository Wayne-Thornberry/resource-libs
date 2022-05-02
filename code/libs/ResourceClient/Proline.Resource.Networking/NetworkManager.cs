

namespace Proline.Resource.Networking
{
    internal class NetworkManager
    {
        internal static ITaskMethods TaskMethods { get; private set; }
        internal static IEventMethods EventMethods { get; private set; }

        internal static void Initialize(ITaskMethods taskMethods, IEventMethods eventMethods)
        {
            TaskMethods = taskMethods;
            EventMethods = eventMethods;
        }
    }
}
