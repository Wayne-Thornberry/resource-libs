using System;


namespace Proline.Engine
{
    public class Log
    {

        public void LogError(object data, bool replicate = true)
        {
            var type = "Error";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            F8Console.WriteLine(format);
            if (replicate)
                //ExecuteServerMethod("LogError", data);
            return;
        }

        public void LogWarn(object data, bool replicate = false)
        {
            var type = "Warn";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            F8Console.WriteLine(format);
            if (replicate)
                //ExecuteServerMethod("LogWarn", data);
            return;
        }

        public void LogDebug(object data, bool replicate = false)
        {
            var type = "Debug";
            data = data == null ? "null object" : data;
            var format = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss:fffZ")} [{type}] {data}";
            F8Console.WriteLine(format);
            // Write to local data file
            if (replicate)
                //ExecuteServerMethod("LogDebug", data);
            return;
        }
    }
}
