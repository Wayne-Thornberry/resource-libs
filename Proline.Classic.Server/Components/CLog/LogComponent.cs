using System;
using System.IO;
using Proline.Engine.Componentry;
using Proline.Engine.Data;
using Proline.Engine.Eventing;

namespace Proline.Classic.Components.CLog
{
    public class LogComponent : ServerComponent
    {
        private const string _path = @"logs\";
        private const string _name = "_log";
        private const string _dtFormat = "yyyy-MM-dd";
        private const string _extension = ".txt";
        private FileStream _fileStream;
        private string _path2;

        protected override void OnInitialized()
        {
            if (!EngineConfiguration.IsClient)
            {
                var filename = DateTime.UtcNow.ToString(_dtFormat) + _name + _extension;
                _path2 = Path.Combine(_path, filename);
                if (!File.Exists(_path2))
                    File.Create(_path2);
            }
        }

        protected override void OnStart()
        {
        }

        [ComponentEvent("logDebug")]
        public void LogDebugHandler(params object[] args)
        {
            var data = args[0];
            Wrtie(data);
        }

        [ComponentEvent("logError")]
        public void LogErrorHandler(params object[] args)
        {
            var data = args[0];
            Wrtie(data);
        }

        [ComponentEvent("logWarn")]
        public void LogWarnHandler(params object[] args)
        {
            var data = args[0];
            Wrtie(data); 
        }


        private void Wrtie(object data)
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
