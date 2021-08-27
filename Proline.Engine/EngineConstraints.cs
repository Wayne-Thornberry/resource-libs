using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class EngineConstraints
    {
        public const string ExecuteEngineMethodHandler = "ExecuteEngineMethodHandler";
        public const string PushHandler = "NetworkPushHandler";
        public const string PullHandler = "NetworkPullHandler";

        public const string ExecuteAPI = "ExecuteAPI";
        public const string CreateAndInsertResponse = "CreateAndInsertResponse";
        public const string HealthCheck = "Healthcheck";
        public const string Log = "Log";

        public const string PlayerConnectingHandler = "playerConnecting";
        public const string PlayerDroppedHandler = "playerDropped";
    }
}
