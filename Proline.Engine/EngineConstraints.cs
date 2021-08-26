using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class EngineConstraints
    {
        public const string NetworkRequestListenerHandler = "networkRequestListener";
        public const string NetworkResponseListenerHandler = "networkResponseListener";
        public const string PushHandler = "networkPushListener";

        public const string ExecuteComponentAPI = "ExecuteComponentControl";
        public const string CreateAndInsertResponse = "CreateAndInsertResponse";
        public const string HealthCheck = "Healthcheck";
        public const string LogWarn = "LogWarn";
        public const string LogError = "LogError";
        public const string LogDebug = "LogDebug";
    }
}
