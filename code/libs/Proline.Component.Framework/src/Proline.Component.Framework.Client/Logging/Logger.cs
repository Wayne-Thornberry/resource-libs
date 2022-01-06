using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Logging
{
    public class Logger
    {
        private static Logger _instance2;
        private Log _instance; 

        public static Logger GetInstance()
        {
            if (_instance2 == null)
                _instance2 = new Logger();
            return _instance2;
        } 

        public Log GetLog()
        {
            if (_instance == null)
                _instance = new Log();
            return _instance;
        }
    }
}
