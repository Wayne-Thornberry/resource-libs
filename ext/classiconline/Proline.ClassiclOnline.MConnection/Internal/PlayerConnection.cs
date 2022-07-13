using CitizenFX.Core;

namespace Proline.ClassicOnline.MConnection.Internal
{
    internal class PlayerConnection
    {
        public Player Player { get; set; }
        public string PlayerName { get; set; }
        public object KickReason { get; set; }
        public dynamic Defferal { get; set; }
    }

    internal class PlayerDisconnection
    {
        public Player Player { get; set; }
        public string Reason { get; internal set; }
    }
}
