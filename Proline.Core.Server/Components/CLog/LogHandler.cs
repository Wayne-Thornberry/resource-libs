using Proline.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Server.Components.CLog
{
    public class LogHandler : ComponentHandler
    {
        private const string _path = @"logs\";
        private const string _name = "_log";
        private const string _dtFormat = "yyyy_MM_dd";
        private const string _extension = ".txt";
        private FileStream _fileStream;
        private string _path2;

        public override void OnComponentInitialized()
        {
            var filename = DateTime.UtcNow.ToString(_dtFormat) + _name + _extension;
            _path2 = Path.Combine(_path, filename);
            if (!File.Exists(_path2))
                File.Create(_path2);
        }

        public override void OnComponentStart()
        {
        }

        public override void OnComponentEvent(string eventName, params object[] args)
        {
            var data = args[0];
            if (eventName.Equals("logDebug") || eventName.Equals("logError") || eventName.Equals("logWarn"))
            {
                _fileStream = File.Open(_path2, FileMode.Append);
                using (FileStream fs = _fileStream)
                {
                    StreamWriter sw = new StreamWriter(fs);
                    long endPoint = fs.Length;
                    // Set the stream position to the end of the file.        
                    fs.Seek(endPoint, SeekOrigin.Begin);
                    sw.WriteLine(data);
                    sw.Flush();
                }
                _fileStream.Close();
            } 
        }
    }
}
