namespace Proline.Resource.Common.CFX
{
    public interface IFXExtendedEvent : IFXWrapper
    {
        void TriggerSidededEvent(int playerId, string eventName, params object[] args);
        void TriggerLatentSidededEvent(int playerId, string eventName, int bps, params object[] args);
    }
}