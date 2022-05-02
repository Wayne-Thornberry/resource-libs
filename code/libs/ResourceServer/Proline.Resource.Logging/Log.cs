using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Logging
{
    public class Log
    {
        private List<string> _entries;

        public Log()
        {
            _entries = new List<string>();
        }

        public string Debug(string data, bool broadcast = false)
        {
            var type = "Debug";
            return LogEntry(type, data);
        }

        private string LogEntry(string type, string data, bool broadcast = false)
        {
            var entry = string.Format("[{0}] [{1}] {2}", DateTime.UtcNow.ToString(), type, data);
            AddEntry(data, entry);
            //CitizenFX.Core.Debug.WriteLine(entry);
            int x = 0;
            switch (entry)
            {
                case "Debug": x = 0; break;
                case "Info": x = 1; break;
                case "Warn": x = 2; break;
                case "Error": x = 3; break;
            }
            return entry;
            //if(broadcast)
            //    CNetwork.InvokeNetworkEvent("LogHandler", data, x);
        }

        public string Warn(string data)
        {
            var type = "Warn";
            return LogEntry(type, data);
        }

        public string Info(string data)
        {
            var type = "Info";
            return LogEntry(type, data);
        }

        public string Error(string data)
        {
            var type = "Error";
            return LogEntry(type, data);
        }

        private void AddEntry(string original, string entry)
        {
            _entries.Add(entry);
            //_output?.WriteLine(entry);
            //original
        }
    }
}
