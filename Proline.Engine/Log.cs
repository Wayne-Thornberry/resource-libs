using System;


namespace Proline.Engine
{
    internal class Log
    {
        private const string Format = "yyyy-MM-ddThh:mm:ss:fffZ";

        internal string LogError(object data)
        {
            var type = "Error";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString(Format)} [{type}] {data}";
            return format;
        }

        internal string LogWarn(object data)
        {
            var type = "Warn";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            return format;
        }

        internal string LogDebug(object data)
        {
            var type = "Debug";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            return format;
        }
    }
}
