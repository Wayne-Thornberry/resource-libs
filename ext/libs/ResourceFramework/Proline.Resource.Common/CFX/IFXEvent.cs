namespace Proline.Resource.Common.CFX
{
    public interface IFXEvent : IFXWrapper
    {
        void TriggerEvent(string eventName, params object[] args);
        void TriggerLatentSidedEvent(string eventName, int bps, params object[] args);
        void TriggerSidedEvent(string eventName, params object[] args);
    }
}