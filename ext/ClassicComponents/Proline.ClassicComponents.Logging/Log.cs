using Proline.EngineFramework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.EngineFramework.Logging
{
    public class Log
    {
        private List<string> _entries;

        public Log()
        {
            _entries = new List<string>(); 
        } 

        public void Debug(string data, bool broadcast = false)
        {
            var type = "Debug";
            LogEntry(type, data); 
        }

        private void LogEntry(string type, string data, bool broadcast = false)
        {
            var entry = string.Format("[{0}] [{1}] {2}", DateTime.UtcNow.ToString(), type, data); 
            AddEntry(data, entry); 
            CitizenFX.Core.Debug.WriteLine(entry);
            int x = 0;
            switch (entry)
            {
                case "Debug": x = 0; break;
                case "Info": x = 1; break;
                case "Warn": x = 2; break;
                case "Error": x = 3; break;
            }
            //if(broadcast)
            //    CNetwork.InvokeNetworkEvent("LogHandler", data, x);
        }

        public void Warn(string data, bool broadcast = false)
        { 
            var type = "Warn";
            LogEntry(type, data);
        }

        public void Info(string data, bool broadcast = false)
        { 
            var type = "Info";
            LogEntry(type, data);
        }

        public void Error(string data, bool broadcast = false)
        { 
            var type = "Error";
            LogEntry(type, data);
        }

        private void AddEntry(string original, string entry)
        {
            _entries.Add(entry);
            //_output?.WriteLine(entry);
            //original
        }
    }
}
