using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Common.Logging
{
    public class Logger
    {
        private static Logger _instance2;
        private Log _instance;
        private IFXConsole _output;

        public static Logger GetInstance()
        {
            if (_instance2 == null)
                _instance2 = new Logger();
            return _instance2;
        }

        public void SetOutput(IFXConsole output)
        {
            _output = output;
        }

        public Log GetLog()
        {
            if (_instance == null)
                _instance = new Log(_output);
            return _instance;
        }
    }
}
