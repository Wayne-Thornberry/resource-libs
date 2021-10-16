namespace Proline.Resource.Common.CFX
{
    public interface IFXEventTrigger : IFXWrapper
    {
        void TriggerEvent(string eventName, params object[] args);
        void TriggerLatentSidedEvent(string eventName, int bps, params object[] args);
        void TriggerSidedEvent(string eventName, params object[] args);
    }
}