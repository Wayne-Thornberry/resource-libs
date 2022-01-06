using Proline.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Logging
{
    public class Log : ILogMethods
    {
        private List<string> _entries; 

        internal Log()
        {
            _entries = new List<string>(); 
        } 

        public void Debug(string data)
        {
            var type = "Debug";
            LogEntry(type, data);
        }

        private void LogEntry(string type, string data)
        {
            var entry = string.Format("[{0}] [{1}] {2}", DateTime.UtcNow.ToString(), type, data); 
            AddEntry(data, entry);
            CitizenFX.Core.Debug.WriteLine(entry);
        }

        public void Warn(string data)
        { 
            var type = "Warn";
            LogEntry(type, data);
        }

        public void Info(string data)
        { 
            var type = "Info";
            LogEntry(type, data);
        }

        public void Error(string data)
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
