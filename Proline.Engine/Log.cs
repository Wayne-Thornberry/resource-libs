using System;


namespace Proline.Engine
{
    internal class Log
    {

        public string LogError(object data)
        {
            var type = "Error";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            return format;
        }

        public string LogWarn(object data)
        {
            var type = "Warn";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            return format;
        }

        public string LogDebug(object data)
        {
            var type = "Debug";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            return format;
        }
    }
}
