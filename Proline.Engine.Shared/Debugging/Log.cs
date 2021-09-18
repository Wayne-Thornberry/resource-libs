using System;
using System.Collections.Generic;
using System.Linq;
using Proline.Engine.Data;

namespace Proline.Engine.Debugging
{
    internal class LogEntry
    {
        public DateTime TimeStamp { get; set; }
        public string Entry { get; set; }
    }

    public class Log
    {
        private const string Format = "yyyy-MM-ddThh:mm:ss:fffZ";
        private List<string> _messages;

        public Log()
        {
            _messages = new List<string>();
        }

        public string LogError(object data)
        {
            var type = "Error";
            data = data == null ? "null object" : data;
            var format = $"[{EngineConfiguration.EnvTypeName}] {DateTime.UtcNow.ToString(Format)} [{type}] {data}";
            _messages.Add(format);
            return format;
        }

        public string LogWarn(object data)
        {
            var type = "Warn";
            data = data == null ? "null object" : data;
            var format = $"[{EngineConfiguration.EnvTypeName}] {DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            _messages.Add(format);
            return format;
        }

        public string LogDebug(object data)
        {
            var type = "Debug";
            data = data == null ? "null object" : data;
            var format = $"[{EngineConfiguration.EnvTypeName}] {DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            _messages.Add(format);
            return format;
        }

        public string[] GetLog(string type = "All")
        {
            if (type.Equals("Debug") || type.Equals("Error") || type.Equals("Warn"))
            {
                return _messages.Where(e => e.Contains($"[{type}]")).ToArray();
            }
            else
            {
                return _messages.ToArray();
            }
        }
    }
}
