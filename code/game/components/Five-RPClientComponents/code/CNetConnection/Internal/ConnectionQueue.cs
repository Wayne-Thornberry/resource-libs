using System.Collections.Generic;

namespace CNetConnection.Internal
{
    internal class ConnectionQueue : Queue<PlayerConnection>
    {
        private static ConnectionQueue _instance;

        public static ConnectionQueue GetInstance()
        {
            if (_instance == null)
                _instance = new ConnectionQueue();
            return _instance;
        }
    }

    internal class DisconnectionQueue : Queue<PlayerDisconnection>
    {
        private static DisconnectionQueue _instance;

        public static DisconnectionQueue GetInstance()
        {
            if (_instance == null)
                _instance = new DisconnectionQueue();
            return _instance;
        }
    }
}
