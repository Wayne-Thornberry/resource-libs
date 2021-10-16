using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Common.Logging
{
    public class Log : ILogMethods
    {
        private List<string> _entries;
        private IFXConsole _output;

        internal Log(IFXConsole output)
        {
            _entries = new List<string>();
            _output = output;
        } 

        public void LogDebug(string data)
        {
            var type = "Debug";
            LogEntry(type, data);
        }

        private void LogEntry(string type, string data)
        {
            var entry = string.Format("[{0}] [{1}] {2}", DateTime.UtcNow.ToString(), type, data); 
            AddEntry(data, entry);
        }

        public void LogWarn(string data)
        { 
            var type = "Warn";
            LogEntry(type, data);
        }

        public void LogInfo(string data)
        { 
            var type = "Info";
            LogEntry(type, data);
        }

        public void LogError(string data)
        { 
            var type = "Error";
            LogEntry(type, data);
        }

        private void AddEntry(string original, string entry)
        {
            _entries.Add(entry);
            _output?.WriteLine(entry);
            //original
        }
    }
}
