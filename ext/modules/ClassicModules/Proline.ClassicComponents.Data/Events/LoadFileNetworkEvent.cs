using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;

using Console = Proline.Resource.Console; 

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {
        public LoadFileNetworkEvent() : base(EventHandlerNames.LOADFILEHANDLER)
        {
        }   
    }
}
