using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Internal
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
